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

namespace Escape
{
    using System.Collections.Generic;
    using Ast;

    public static class SyntaxNodeFactory 
    {
        public static ArrayExpression Array(IEnumerable<Expression> elements)
        {
            return new ArrayExpression(elements);
        }
        
        public static AssignmentExpression Assignment(string op, Expression left, Expression right)
        {
            return new AssignmentExpression(AssignmentExpression.ParseAssignmentOperator(op), left, right);
        }

        public static Expression Binary(string op, Expression left, Expression right)
        {
            return (op == "||" || op == "&&")
                 ? (Expression) new LogicalExpression (LogicalExpression.ParseLogicalOperator(op), left, right)
                 : new BinaryExpression(BinaryExpression.ParseBinaryOperator(op), left, right);
        }

        public static BlockStatement Block(IEnumerable<Statement> body)
        {
            return new BlockStatement(body);
        }

        public static BreakStatement Break(Identifier label)
        {
            return new BreakStatement(label);
        }

        public static CallExpression Call(Expression callee, IEnumerable<Expression> args)
        {
            return new CallExpression(callee, args);
        }

        public static CatchClause Catch(Identifier param, BlockStatement body)
        {
            return new CatchClause(param, body);
        }

        public static ConditionalExpression Conditional(Expression test, Expression consequent,
            Expression alternate)
        {
            return new ConditionalExpression(test, consequent, alternate);
        }

        public static ContinueStatement Continue(Identifier label)
        {
            return new ContinueStatement(label);
        }

        public static DebuggerStatement Debugger()
        {
            return new DebuggerStatement() /* TODO Singleton? */;
        }

        public static DoWhileStatement DoWhile(Statement body, Expression test)
        {
            return new DoWhileStatement(body, test);
        }

        public static EmptyStatement Empty() // TODO singleton?
        {
            return new EmptyStatement();
        }

        public static ExpressionStatement Expression(Expression expression)
        {
            return new ExpressionStatement(expression);
        }

        public static ForStatement For(SyntaxNode init, Expression test, Expression update, Statement body)
        {
            return new ForStatement(init, test, update, body);
        }

        public static ForInStatement ForIn(SyntaxNode left, Expression right, Statement body)
        {
            return new ForInStatement(left, right, body, false);
        }

        public static FunctionDeclaration FunctionDeclaration(Identifier id, IEnumerable<Identifier> parameters,
            IEnumerable<Expression> defaults, Statement body, bool strict)
        {
            return new FunctionDeclaration(id, parameters, body, strict);
        }

        public static FunctionExpression Function(Identifier id, IEnumerable<Identifier> parameters,
            IEnumerable<Expression> defaults, Statement body, bool strict)
        {
            return new FunctionExpression(id, parameters, body, strict);
        }

        public static Identifier Identifier(string name)
        {
            return new Identifier(name);
        }

        public static IfStatement If(Expression test, Statement consequent, Statement alternate)
        {
            return new IfStatement(test, consequent, alternate);
        }

        public static LabelledStatement LabeledStatement(Identifier label, Statement body)
        {
            return new LabelledStatement(label, body);
        }

        public static Literal Literal(object value, string raw)
        {
            return new Literal(value, raw);
        }

        public static MemberExpression Member(char accessor, Expression obj, Expression property)
        {
            return new MemberExpression(obj, property, accessor == '[');
        }

        public static NewExpression New(Expression callee, IEnumerable<Expression> args)
        {
            return new NewExpression(callee, args);
        }

        public static ObjectExpression Object(IEnumerable<Property> properties)
        {
            return new ObjectExpression(properties);
        }

        public static UpdateExpression Postfix(string op, Expression argument)
        {
            return new UpdateExpression(UnaryExpression.ParseUnaryOperator(op), argument, false);
        }

        public static Program Program(ICollection<Statement> body, bool strict)
        {
            return new Program(body, strict);
        }

        public static Property Property(PropertyKind kind, IPropertyKeyExpression key, Expression value)
        {
            return new Property(kind, key, value);
        }

        public static ReturnStatement Return(Expression argument)
        {
            return new ReturnStatement(argument);
        }

        public static SequenceExpression Sequence(IList<Expression> expressions)
        {
            return new SequenceExpression(expressions);
        }

        public static SwitchCase SwitchCase(Expression test, IEnumerable<Statement> consequent)
        {
            return new SwitchCase(test, consequent);
        }

        public static SwitchStatement Switch(Expression discriminant, IEnumerable<SwitchCase> cases)
        {
            return new SwitchStatement(discriminant, cases);
        }

        public static ThisExpression This()
        {
            return new ThisExpression() /* TODO Singleton? */;
        }

        public static ThrowStatement Throw(Expression argument)
        {
            return new ThrowStatement(argument);
        }

        public static TryStatement Try(Statement block, IEnumerable<Statement> guardedHandlers,
            IEnumerable<CatchClause> handlers, Statement finalizer)
        {
            return new TryStatement(block, guardedHandlers, handlers, finalizer);
        }

        public static UnaryExpression Unary(string op, Expression argument)
        {
            var pop = UnaryExpression.ParseUnaryOperator(op);
            return pop == UnaryOperator.Increment || pop == UnaryOperator.Decrement
                 ? new UpdateExpression(pop, argument, true)
                 : new UnaryExpression(pop, argument, true);
        }

        public static VariableDeclaration VariableDeclaration(IEnumerable<VariableDeclarator> declarations, string kind)
        {
            return new VariableDeclaration(declarations, kind);
        }

        public static VariableDeclarator VariableDeclarator(Identifier id, Expression init)
        {
            return new VariableDeclarator(id, init);
        }

        public static WhileStatement While(Expression test, Statement body)
        {
            return new WhileStatement(test, body);
        }

        public static WithStatement With(Expression obj, Statement body)
        {
            return new WithStatement(obj, body);
        }
    }
}