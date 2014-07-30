#region BSD 2-Clause License
//
// Copyright (C) 2014, Atif Aziz
// Copyright (C) 2013, Sebastien Ros
// Copyright (C) 2013, Thaddee Tyl <thaddee.tyl@gmail.com>
// Copyright (C) 2012, Mathias Bynens <mathias@qiwi.be>
// Copyright (C) 2012, Joost-Wim Boekesteijn <joost-wim@boekesteijn.nl>
// Copyright (C) 2012, Kris Kowal <kris.kowal@cixar.com>
// Copyright (C) 2012, Yusuke Suzuki <utatane.tea@gmail.com>
// Copyright (C) 2012, Arpad Borsos <arpad.borsos@googlemail.com>
// Copyright (C) 2011, Ariya Hidayat <ariya.hidayat@gmail.com>
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
// 
//   * Redistributions of source code must retain the above copyright
//     notice, this list of conditions and the following disclaimer.
//
//   * Redistributions in binary form must reproduce the above copyright
//     notice, this list of conditions and the following disclaimer in the
//     documentation and/or other materials provided with the distribution.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
// ARE DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
#endregion

namespace Escape.Tests
{
    #region Imports

    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Ast;
    using Esparse;
    using Mannex.Diagnostics;
    using Xunit;
    using Xunit.Extensions;

    #endregion

    public class ParserTests
    {
        readonly JavaScriptParser _parser = new JavaScriptParser();

        [Theory]
        [InlineData("jQuery.js"        , "1.9.1" , true)]
        [InlineData("underscore.js"    , "1.5.2" , true )]
        [InlineData("backbone.js"      , "1.1.0" , true )]
        [InlineData("mootools.js"      , "1.4.5" , true )]
        [InlineData("angular.js"       , "1.2.5" , true )]
        [InlineData("JSXTransformer.js", "0.10.0", false)]
        public void ShouldParseScriptFile(string file, string version, bool compareTrees)
        {
            const string prefix = "Escape.Tests.Scripts.";

            var assembly = Assembly.GetExecutingAssembly();
            var scriptPath = prefix + file;
            var sw = new Stopwatch();
            var tempFilePath = NodePath != null 
                             ? Path.GetTempFileName() : null;

            try
            {
                using (var stream = assembly.GetManifestResourceStream(scriptPath))
                {
                    Assert.True(stream != null, string.Format("Script resource '{0}' not found", scriptPath));
                    using (var sr = new StreamReader(stream))
                    {
                        var source = sr.ReadToEnd();
                        sw.Reset();
                        sw.Start();
                        var parser = new JavaScriptParser();
                        var program = parser.Parse(source);
                        var bsfp = new ByteSizeFormatProvider();
                        Console.WriteLine("Parsed {0} {1} ({3}) in {2} ms", file, version, sw.ElapsedMilliseconds, bsfp.Format("SZ1", source.Length, null));
                        Assert.NotNull(program);

                        if (!compareTrees)
                        {
                            Console.WriteLine("WARNING! AST compairson skipped for {0} {1}.", file, version);
                            return;
                        }

                        if (tempFilePath == null)
                        {
                            Console.WriteLine(
                                "If Node.js is installed in the system path " + 
                                "then a more comprehensive test can be carried " + 
                                "out comparing Esprima's and Escape's AST.");
                            return;
                        }

                        File.WriteAllText(tempFilePath, source);
                        sw.Restart();
                        var esparseOutput = Esparse(tempFilePath);

                        Console.WriteLine(
                            "Esparse.js for {0} {1} took {2} ms, producing {3} of pretty-printed AST JSON", 
                            file, version, sw.ElapsedMilliseconds, 
                            bsfp.Format("SZ1", esparseOutput.Length, null));

                        var sb = new StringBuilder(esparseOutput.Length * 110 / 100);
                        var esw = new StringWriter(sb) { NewLine = "\r\n" };
                        JsonEncoder.Encode(program, false, esw);
                        esw.WriteLine();
                        Assert.Equal(esparseOutput.Length, sb.Length);
                        Assert.True(esparseOutput == sb.ToString(), "AST JSON mismatch");
                    }
                }
            }
            finally
            {
                if (tempFilePath != null)
                    File.Delete(tempFilePath);
            }
        }

        static string _nodePath;

        public static string NodePath
        {
            get { return _nodePath ?? (_nodePath = FindNode()); }
        }

        static string FindNode()
        {
            var path = Environment.GetEnvironmentVariable("PATH") ?? string.Empty;
            return path.Split(Path.PathSeparator)
                       .Select(p => Path.Combine(p, "node.exe"))
                       .FirstOrDefault(File.Exists);
        }

        static string Esparse(string path, string options = null)
        {
            using (var node = Process.Start(new ProcessStartInfo
            {
                UseShellExecute = false,
                FileName = NodePath,
                Arguments = string.Format("esparse.js {0} \"{1}\"", options, path),
                CreateNoWindow = true,
                RedirectStandardOutput = true, 
                RedirectStandardError = true,
                StandardOutputEncoding = Encoding.UTF8,
            }))
            {
                var stdout = new StringWriter();
                var stderr = new StringWriter();
                var drain = node.BeginReadLine(stdout.WriteLine, stderr.WriteLine);

                node.WaitForExit(null); // ReSharper disable once PossibleNullReferenceException
                if (node.ExitCode != 0)
                {
                    Console.Error.Write(stderr);
                    throw new Exception(string.Format("Node program terminated with a non-zero exit code of {0}.", node.ExitCode));
                }
                
                if (!drain(TimeSpan.FromSeconds(5)))
                    throw new TimeoutException();
                
                return stdout.ToString();
            }
        }
        
        [Fact]
        public void ShouldParseThis()
        {
            var program = _parser.Parse("this");
            var body = program.Body;

            Assert.NotNull(body);
            Assert.Equal(1, body.Count());
            Assert.Equal(SyntaxNodes.ThisExpression, body.First().As<ExpressionStatement>().Expression.Type);
        }

        [Fact]
        public void ShouldParseNull()
        {
            var program = _parser.Parse("null");
            var body = program.Body;

            Assert.NotNull(body);
            Assert.Equal(1, body.Count());
            Assert.Equal(SyntaxNodes.Literal, body.First().As<ExpressionStatement>().Expression.Type);
            Assert.Equal(null, body.First().As<ExpressionStatement>().Expression.As<Literal>().Value);
            Assert.Equal("null", body.First().As<ExpressionStatement>().Expression.As<Literal>().Raw);
        }

        [Fact]
        public void ShouldParseNumeric()
        {
            var program = _parser.Parse(
                @"
                42
            ");
            var body = program.Body;

            Assert.NotNull(body);
            Assert.Equal(1, body.Count());
            Assert.Equal(SyntaxNodes.Literal, body.First().As<ExpressionStatement>().Expression.Type);
            Assert.Equal(42d, body.First().As<ExpressionStatement>().Expression.As<Literal>().Value);
            Assert.Equal("42", body.First().As<ExpressionStatement>().Expression.As<Literal>().Raw);
        }

        [Fact]
        public void ShouldParseBinaryExpression()
        {
            BinaryExpression binary;

            var program = _parser.Parse("(1 + 2 ) * 3");
            var body = program.Body;

            Assert.NotNull(body);
            Assert.Equal(1, body.Count());
            Assert.NotNull(binary = body.First().As<ExpressionStatement>().Expression.As<BinaryExpression>());
            Assert.Equal(3d, binary.Right.As<Literal>().Value);
            Assert.Equal(BinaryOperator.Times, binary.Operator);
            Assert.Equal(1d, binary.Left.As<BinaryExpression>().Left.As<Literal>().Value);
            Assert.Equal(2d, binary.Left.As<BinaryExpression>().Right.As<Literal>().Value);
            Assert.Equal(BinaryOperator.Plus, binary.Left.As<BinaryExpression>().Operator);
        }

        [Theory]
        [InlineData(0, "0")]
        [InlineData(42, "42")]
        [InlineData(0.14, "0.14")]
        [InlineData(3.14159, "3.14159")]
        [InlineData(6.02214179e+23, "6.02214179e+23")]
        [InlineData(1.492417830e-10, "1.492417830e-10")]
        [InlineData(0, "0x0")]
        [InlineData(0, "0x0;")]
        [InlineData(0xabc, "0xabc")]
        [InlineData(0xdef, "0xdef")]
        [InlineData(0X1A, "0X1A")]
        [InlineData(0x10, "0x10")]
        [InlineData(0x100, "0x100")]
        [InlineData(0X04, "0X04")]
        [InlineData(02, "02")]
        [InlineData(10, "012")]
        [InlineData(10, "0012")]
        public void ShouldParseNumericLiterals(object expected, string source)
        {
            Literal literal;

            var program = _parser.Parse(source);
            var body = program.Body;

            Assert.NotNull(body);
            Assert.Equal(1, body.Count());
            Assert.NotNull(literal = body.First().As<ExpressionStatement>().Expression.As<Literal>());
            Assert.Equal(Convert.ToDouble(expected), Convert.ToDouble(literal.Value));
        }

        [Theory]
        [InlineData("Hello", @"'Hello'")]
        [InlineData("\n\r\t\v\b\f\\\'\"\0", @"'\n\r\t\v\b\f\\\'\""\0'")]
        [InlineData("\u0061", @"'\u0061'")]
        [InlineData("\x61", @"'\x61'")]
        [InlineData("Hello\nworld", @"'Hello\nworld'")]
        [InlineData("Hello\\\nworld", @"'Hello\\\nworld'")]
        public void ShouldParseStringLiterals(string expected, string source)
        {
            Literal literal;

            var program = _parser.Parse(source);
            var body = program.Body;

            Assert.NotNull(body);
            Assert.Equal(1, body.Count());
            Assert.NotNull(literal = body.First().As<ExpressionStatement>().Expression.As<Literal>());
            Assert.Equal(expected, literal.Value);
        }

        [Theory]
        [InlineData(@"{ x
                      ++y }")]
        [InlineData(@"{ x
                      --y }")]
        [InlineData(@"var x /* comment */;
                      { var x = 14, y = 3
                      z; }")]
        [InlineData(@"while (true) { continue
                      there; }")]
        [InlineData(@"while (true) { continue // Comment
                      there; }")]
        [InlineData(@"while (true) { continue /* Multiline
                      Comment */there; }")]
        [InlineData(@"while (true) { break
                      there; }")]
        [InlineData(@"while (true) { break // Comment
                      there; }")]
        [InlineData(@"while (true) { break /* Multiline
                      Comment */there; }")]
        [InlineData(@"(function(){ return
                      x; })")]
        [InlineData(@"(function(){ return // Comment
                      x; })")]
        [InlineData(@"(function(){ return/* Multiline
                      Comment */x; })")]
        [InlineData(@"{ throw error
                      error; }")]
        [InlineData(@"{ throw error// Comment
                      error; }")]
        [InlineData(@"{ throw error/* Multiline
                      Comment */error; }")]

        public void ShouldInsertSemicolons(string source)
        {
            var program = _parser.Parse(source);
            var body = program.Body;

            Assert.NotNull(body);
        }

    }
}
