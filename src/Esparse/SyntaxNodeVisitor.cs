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
    using System;
    using Escape.Ast;

    abstract class SyntaxNodeVisitor
    {
        public virtual void Visit(SyntaxNode node)
        {
            switch (node.NodeType)
            {
                case SyntaxNodeType.ArrayExpression: Visit((ArrayExpression) node); break;
                case SyntaxNodeType.AssignmentExpression: Visit((AssignmentExpression) node); break;
                case SyntaxNodeType.BinaryExpression: Visit((BinaryExpression) node); break;
                case SyntaxNodeType.BlockStatement: Visit((BlockStatement) node); break;
                case SyntaxNodeType.BreakStatement: Visit((BreakStatement) node); break;
                case SyntaxNodeType.CallExpression: Visit((CallExpression) node); break;
                case SyntaxNodeType.CatchClause: Visit((CatchClause) node); break;
                case SyntaxNodeType.ConditionalExpression: Visit((ConditionalExpression) node); break;
                case SyntaxNodeType.ContinueStatement: Visit((ContinueStatement) node); break;
                case SyntaxNodeType.DebuggerStatement: Visit((DebuggerStatement) node); break;
                case SyntaxNodeType.DoWhileStatement: Visit((DoWhileStatement) node); break;
                case SyntaxNodeType.EmptyStatement: Visit((EmptyStatement) node); break;
                case SyntaxNodeType.ExpressionStatement: Visit((ExpressionStatement) node); break;
                case SyntaxNodeType.ForInStatement: Visit((ForInStatement) node); break;
                case SyntaxNodeType.ForStatement: Visit((ForStatement) node); break;
                case SyntaxNodeType.FunctionDeclaration: Visit((FunctionDeclaration) node); break;
                case SyntaxNodeType.FunctionExpression: Visit((FunctionExpression) node); break;
                case SyntaxNodeType.Identifier: Visit((Identifier) node); break;
                case SyntaxNodeType.IfStatement: Visit((IfStatement) node); break;
                case SyntaxNodeType.LabeledStatement: Visit((LabelledStatement) node); break;
                case SyntaxNodeType.Literal: Visit((Literal) node); break;
                case SyntaxNodeType.LogicalExpression: Visit((LogicalExpression) node); break;
                case SyntaxNodeType.MemberExpression: Visit((MemberExpression) node); break;
                case SyntaxNodeType.NewExpression: Visit((NewExpression) node); break;
                case SyntaxNodeType.ObjectExpression: Visit((ObjectExpression) node); break;
                case SyntaxNodeType.Program: Visit((Escape.Ast.Program) node); break;
                case SyntaxNodeType.Property: Visit((Property) node); break;
                case SyntaxNodeType.RegularExpressionLiteral: Visit((Literal) node); break;
                case SyntaxNodeType.ReturnStatement: Visit((ReturnStatement) node); break;
                case SyntaxNodeType.SequenceExpression: Visit((SequenceExpression) node); break;
                case SyntaxNodeType.SwitchCase: Visit((SwitchCase) node); break;
                case SyntaxNodeType.SwitchStatement: Visit((SwitchStatement) node); break;
                case SyntaxNodeType.ThisExpression: Visit((ThisExpression) node); break;
                case SyntaxNodeType.ThrowStatement: Visit((ThrowStatement) node); break;
                case SyntaxNodeType.TryStatement: Visit((TryStatement) node); break;
                case SyntaxNodeType.UnaryExpression: Visit((UnaryExpression) node); break;
                case SyntaxNodeType.UpdateExpression: Visit((UpdateExpression) node); break;
                case SyntaxNodeType.VariableDeclaration: Visit((VariableDeclaration) node); break;
                case SyntaxNodeType.VariableDeclarator: Visit((VariableDeclarator) node); break;
                case SyntaxNodeType.WhileStatement: Visit((WhileStatement) node); break;
                case SyntaxNodeType.WithStatement: Visit((WithStatement) node); break;
                default: throw new NotImplementedException();
            }
        }

        protected abstract void Visit(ArrayExpression node);
        protected abstract void Visit(AssignmentExpression node);
        protected abstract void Visit(BinaryExpression node);
        protected abstract void Visit(BlockStatement node);
        protected abstract void Visit(BreakStatement node);
        protected abstract void Visit(CallExpression node);
        protected abstract void Visit(CatchClause node);
        protected abstract void Visit(ConditionalExpression node);
        protected abstract void Visit(ContinueStatement node);
        protected abstract void Visit(DebuggerStatement node);
        protected abstract void Visit(DoWhileStatement node);
        protected abstract void Visit(EmptyStatement node);
        protected abstract void Visit(ExpressionStatement node);
        protected abstract void Visit(ForInStatement node);
        protected abstract void Visit(ForStatement node);
        protected abstract void Visit(FunctionDeclaration node);
        protected abstract void Visit(FunctionExpression node);
        protected abstract void Visit(Identifier node);
        protected abstract void Visit(IfStatement node);
        protected abstract void Visit(LabelledStatement node);
        protected abstract void Visit(Literal node);
        protected abstract void Visit(LogicalExpression node);
        protected abstract void Visit(MemberExpression node);
        protected abstract void Visit(NewExpression node);
        protected abstract void Visit(ObjectExpression node);
        protected abstract void Visit(Escape.Ast.Program node);
        protected abstract void Visit(Property node);
        protected abstract void Visit(ReturnStatement node);
        protected abstract void Visit(SequenceExpression node);
        protected abstract void Visit(SwitchCase node);
        protected abstract void Visit(SwitchStatement node);
        protected abstract void Visit(ThisExpression node);
        protected abstract void Visit(ThrowStatement node);
        protected abstract void Visit(TryStatement node);
        protected abstract void Visit(UnaryExpression node);
        protected abstract void Visit(UpdateExpression node);
        protected abstract void Visit(VariableDeclaration node);
        protected abstract void Visit(VariableDeclarator node);
        protected abstract void Visit(WhileStatement node);
        protected abstract void Visit(WithStatement node);
    }
}