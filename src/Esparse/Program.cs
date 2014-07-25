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

namespace Esparse
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using Escape;

    #endregion

    static class Program
    {
        static int Main(string[] args)
        {
            try
            {
                Run(args);
                return 0;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.GetBaseException().Message);
                Trace.TraceError(e.ToString());
                return 0xbad;
            }
        }

        static void Run(IEnumerable<string> args)
        {
            var options = new ParserOptions();
            var includeLocation = false;
            var outputEncoding = (Encoding) null;
            var inputEncoding = (Encoding) null;
            var stats = false;
            var gc = false;

            using (var arg = args.GetEnumerator())
            {
                bool popped;
                for (popped = arg.MoveNext(); popped; popped = arg.MoveNext())
                {
                    if (arg.Current == "--comment") { options.Comment = true; }
                    else if (arg.Current == "--tokens") { options.Tokens = true; }
                    else if (arg.Current == "--tolerant") { options.Tolerant = true; }
                    else if (arg.Current == "--loc") { includeLocation = true; }
                    else if (arg.Current == "--stats") { stats = true; }
                    else if (arg.Current == "--gc") { gc = true; }
                    else if (arg.Current == "--output-encoding")
                    {
                        if (!arg.MoveNext()) throw new Exception("Missing output encoding value specification.");
                        outputEncoding = Encoding.GetEncoding(arg.Current);
                    }
                    else if (arg.Current == "--input-encoding")
                    {
                        if (!arg.MoveNext()) throw new Exception("Missing input encoding value specification.");
                        inputEncoding = Encoding.GetEncoding(arg.Current);
                    }
                    else
                    {
                        if (arg.Current == "--")
                        {
                            while (arg.MoveNext()) { /* NOP */ }                        
                            popped = false;
                        }
                        break;
                    }
                }

                var sourceFilePath = arg.Current;

                var source = popped && sourceFilePath != "-"
                           ? inputEncoding != null 
                             ? File.ReadAllText(sourceFilePath, inputEncoding) 
                             : File.ReadAllText(sourceFilePath)
                           : Console.In.ReadToEnd();

                Escape.Ast.Program program = null;
                
                var durations = new TimeSpan[stats ? 4 : 1];
                var allocations = gc && stats ? new long[durations.Length] : null;
                var nullHeapSize = (long?) null;

                for (var i = 0; i < durations.Length; i++)
                {
                    var heapStartSize = allocations != null 
                                      ? GC.GetTotalMemory(true) 
                                      : nullHeapSize;

                    var sw = Stopwatch.StartNew();
                    program = new JavaScriptParser().Parse(source, options);
                    durations[i] = sw.Elapsed;

                    if (heapStartSize != nullHeapSize)
                    {
                        allocations[i] = GC.GetTotalMemory(true) - heapStartSize.Value;
                        if (i < durations.Length - 1)
                            program = null; // so it can be collected
                    }
                }

                Debug.Assert(program != null);

                using (var stdout = OpenStdOut(outputEncoding))
                {
                    JsonEncoder.Encode(program, includeLocation, stdout);
                    stdout.Flush();
                }

                if (stats)
                {
                    var ms = from d in durations.Skip(1 /* cold */) 
                             select d.TotalMilliseconds;
                    var duration = TimeSpan.FromMilliseconds(ms.Average());
                    var format = duration.Days == 0 && duration.Hours == 0
                               ? "mm':'ss'.'ffff"
                               : null;
                    Console.Error.WriteLine("Time: {0}; [{1}]", 
                        duration.ToString(format), 
                        string.Join(", ", from d in durations select d.ToString(format)));
                }

                if (allocations != null)
                {
                    var bsfp = new ByteSizeFormatProvider();
                    Console.Error.WriteLine("Heap: {0}; [{1}]",
                        bsfp.Format("SZ2", allocations.Select(a => (double)a).Average(), null),
                        string.Join(", ", allocations));
                }
            }
        }

        static TextWriter OpenStdOut(Encoding encoding, int bufferSize = 4096)
        {
            return
                #if DEBUG
                encoding == null 
                ? Console.Out // unbuffered!
                : new StreamWriter(Console.OpenStandardOutput(), encoding, bufferSize)
                #else
                new StreamWriter(Console.OpenStandardOutput(), encoding ?? Console.OutputEncoding, bufferSize)
                #endif
                ;
        }
    }
}
