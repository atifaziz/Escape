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
    #region Region

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Escape;
    using Escape.Ast;

    #endregion

    sealed class JsonEncoder : SyntaxNodeVisitor
    {
        readonly bool _includeLocation;
        readonly JsonTextWriter _writer;

        public JsonEncoder(TextWriter writer, bool includeLocation)
        {
            _writer = new JsonTextWriter(writer);
            _includeLocation = includeLocation;
        }

        public static void Encode(Escape.Ast.Program program, bool includeLocation, TextWriter stdout)
        {
            new JsonEncoder(stdout, includeLocation).Visit(program);
        }

        void Encode(SyntaxNode node, params KeyValuePair<string, object>[] members)
        {
            var ms = new[] { Member("type", node.Type.ToString()) }.Concat(members);
            if (_includeLocation)
                ms = ms.Concat(new[] { Member("loc", node.Location) });
            Object(ms.ToArray());
        }

        void Object(params KeyValuePair<string, object>[] members)
        {
            _writer.Object();
            foreach (var m in members)
            {
                _writer.Member(m.Key);
                Encode(m.Value);
            }
            _writer.EndObject();
        }

        void Encode(object value)
        {
            IEnumerable items;
            SyntaxNode node;
            Location location;
            string str;

            if (value == null) { _writer.Null(); }
            else if ((node = value as SyntaxNode) != null) { Visit(node); }
            else if ((location = value as Location) != null) { Encode(location); }
            else if (value is Position) { Encode((Position) value); }
            else if ((str = value as string) != null) { _writer.String(str); }
            else if (value is bool) { _writer.Boolean((bool) value); }
            else if (value is int) { _writer.Number((int) value); }
            else if (value is long) { _writer.Number((long) value); }
            else if (value is double) { _writer.Number((double) value); }
            else if (value is BinaryOperator) { _writer.String(((BinaryOperator) value).JsText()); }
            else if (value is LogicalOperator) { _writer.String(((LogicalOperator) value).JsText()); }
            else if (value is UnaryOperator) { _writer.String(((UnaryOperator) value).JsText()); }
            else if (value is AssignmentOperator) { _writer.String(((AssignmentOperator) value).JsText()); }
            else if (value is PropertyKind) 
            { 
                switch ((PropertyKind) value) 
                { 
                    case PropertyKind.Data: _writer.String("init"); break;
                    case PropertyKind.Get : _writer.String("get" ); break;
                    case PropertyKind.Set : _writer.String("set" ); break;
                    default: throw new NotSupportedException(); 
                } 
            }
            else if ((items = value as IEnumerable) != null)
            {
                _writer.Array();
                foreach (var item in items)
                    Encode(item);
                _writer.EndArray();
            }
            else { throw new NotSupportedException(string.Format("Don't know how to encode {0} as JSON.", value.GetType().FullName)); }
        }

        public static KeyValuePair<string, object> Member(string key, object value)
        {
            return new KeyValuePair<string, object>(key, value);
        }

        void Encode(Location location)
        {
            Object(Member("start", location.Start), Member("end", location.End));
        }

        void Encode(Position position)
        {
            Object(Member("line", position.Line), Member("column", position.Column));
        }

        protected override void Visit(ArrayExpression node)
        {
            Encode(node, Member("elements", node.Elements));
        }

        protected override void Visit(AssignmentExpression node)
        {
            Encode(node, Member("operator", node.Operator), 
                Member("left", node.Left), Member("right", node.Right));
        }

        protected override void Visit(BinaryExpression node)
        {
            Encode(node, Member("operator", node.Operator), 
                Member("left", node.Left), Member("right", node.Right));
        }

        protected override void Visit(BlockStatement node)
        {
            Encode(node, Member("body", node.Body));
        }

        protected override void Visit(BreakStatement node)
        {
            Encode(node, Member("label", node.Label));
        }

        protected override void Visit(CallExpression node)
        {
            Encode(node, Member("callee", node.Callee), Member("arguments", node.Arguments));
        }

        protected override void Visit(CatchClause node)
        {
            Encode(node, Member("param", node.Param), Member("body", node.Body));
        }

        protected override void Visit(ConditionalExpression node)
        {
            Encode(node, Member("test", node.Test), Member("consequent", node.Consequent), Member("alternate", node.Alternate));
        }

        protected override void Visit(ContinueStatement node)
        {
            Encode(node, Member("label", node.Label));
        }

        protected override void Visit(DebuggerStatement node)
        {
            Encode(node);
        }

        protected override void Visit(DoWhileStatement node)
        {
            Encode(node, Member("body", node.Body), Member("test", node.Test));
        }

        protected override void Visit(EmptyStatement node)
        {
            Encode(node);
        }

        protected override void Visit(ExpressionStatement node)
        {
            Encode(node, Member("expression", node.Expression));
        }

        protected override void Visit(ForInStatement node)
        {
            Encode(node, Member("left", node.Left), Member("right", node.Right), 
                Member("body", node.Body), Member("each", node.Each));
        }

        protected override void Visit(ForStatement node)
        {
            Encode(node, Member("init", node.Init), Member("test", node.Test), 
                Member("update", node.Update), Member("body", node.Body));
        }

        protected override void Visit(FunctionDeclaration node)
        {
            Encode(node, Member("id", node.Id), Member("params", node.Parameters), 
                Member("defaults", node.Defaults), Member("body", node.Body), 
                Member("rest", node.Rest), Member("generator", node.Generator), 
                Member("expression", node.Expression));
        }

        protected override void Visit(FunctionExpression node)
        {
            Encode(node, Member("id", node.Id), Member("params", node.Parameters), 
                Member("defaults", node.Defaults), Member("body", node.Body), 
                Member("rest", node.Rest), Member("generator", node.Generator), 
                Member("expression", node.Expression));
        }

        protected override void Visit(Identifier node)
        {
            Encode(node, Member("name", node.Name));
        }

        protected override void Visit(IfStatement node)
        {
            Encode(node, Member("test", node.Test), Member("consequent", node.Consequent), Member("alternate", node.Alternate));
        }

        protected override void Visit(LabelledStatement node)
        {
            Encode(node, Member("label", node.Label), Member("body", node.Body));
        }

        protected override void Visit(Literal node)
        {
            if (node.Type == SyntaxNodes.RegularExpressionLiteral)
            {
                // http://www.ecma-international.org/ecma-262/5.1/#sec-15.10.6.4

                var value = node.Raw;
                var closingSlashIndex = value.LastIndexOf('/');
                if (closingSlashIndex + 2 < value.Length)
                {
                    var flags = value.Substring(closingSlashIndex + 1);
                    var sb = new StringBuilder(value, 0, closingSlashIndex + 1, value.Length);
                    if (flags.IndexOf('g') >= 0) sb.Append('g');
                    if (flags.IndexOf('i') >= 0) sb.Append('i');
                    if (flags.IndexOf('m') >= 0) sb.Append('m');
                    value = sb.ToString();
                }

                node = new Literal
                {
                    Type = SyntaxNodes.Literal,
                    Location = node.Location,
                    Range = node.Range,
                    Raw = node.Raw,
                    Value = value
                };
            }

            Encode(node, Member("value", node.Value), Member("raw", node.Raw));
        }

        protected override void Visit(LogicalExpression node)
        {
            Encode(node, Member("operator", node.Operator), 
                Member("left", node.Left), Member("right", node.Right));
        }

        protected override void Visit(MemberExpression node)
        {
            Encode(node, Member("computed", node.Computed), 
                Member("object", node.Object), Member("property", node.Property));
        }

        protected override void Visit(NewExpression node)
        {
            Encode(node, Member("callee", node.Callee), Member("arguments", node.Arguments));
        }

        protected override void Visit(ObjectExpression node)
        {
            Encode(node, Member("properties", node.Properties));
        }

        protected override void Visit(Escape.Ast.Program node)
        {
            Encode(node, Member("body", node.Body));
        }

        protected override void Visit(Property node)
        {
            Encode(node, Member("key", node.Key), Member("value", node.Value), Member("kind", node.Kind));
        }

        protected override void Visit(ReturnStatement node)
        {
            Encode(node, Member("argument", node.Argument));
        }

        protected override void Visit(SequenceExpression node)
        {
            Encode(node, Member("expressions", node.Expressions));
        }

        protected override void Visit(SwitchCase node)
        {
            Encode(node, Member("test", node.Test), Member("consequent", node.Consequent));
        }

        protected override void Visit(SwitchStatement node)
        {
            Encode(node, Member("discriminant", node.Discriminant), Member("cases", node.Cases));
        }

        protected override void Visit(ThisExpression node)
        {
            Encode(node);
        }

        protected override void Visit(ThrowStatement node)
        {
            Encode(node, Member("argument", node.Argument));
        }

        protected override void Visit(TryStatement node)
        {
            Encode(node, Member("block", node.Block), 
                Member("guardedHandlers", node.GuardedHandlers), 
                Member("handlers", node.Handlers), 
                Member("finalizer", node.Finalizer));
        }

        protected override void Visit(UnaryExpression node)
        {
            Encode(node, Member("operator", node.Operator), Member("argument", node.Argument), 
                Member("prefix", node.Prefix));
        }

        protected override void Visit(UpdateExpression node)
        {
            Visit((UnaryExpression) node);
        }

        protected override void Visit(VariableDeclaration node)
        {
            Encode(node, Member("declarations", node.Declarations), Member("kind", node.Kind));
        }

        protected override void Visit(VariableDeclarator node)
        {
            Encode(node, Member("id", node.Id), Member("init", node.Init));
        }

        protected override void Visit(WhileStatement node)
        {
            Encode(node, Member("test", node.Test), Member("body", node.Body));
        }

        protected override void Visit(WithStatement node)
        {
            Encode(node, Member("object", node.Object), Member("body", node.Body));
        }
    }
}