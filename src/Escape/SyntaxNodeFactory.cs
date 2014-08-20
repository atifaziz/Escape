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

    public static partial class SyntaxNodeFactory 
    {
        public static ArrayExpression Array(IEnumerable<Expression> elements, Location location)
        {
            return new ArrayExpression(elements, location);
        }
        
        public static AssignmentExpression Assignment(AssignmentOperator op, Expression left, Expression right, Location location)
        {
            return new AssignmentExpression(op, left, right, location);
        }

        internal static Expression BinaryOrLogical(string op, Expression left, Expression right, Location location)
        {
            LogicalOperator? lop;
            return (lop = JavaScriptParser.TryParseLogicalOperator(op)) != null
                 ? (Expression) Logical(lop.Value, left, right, location)
                 : Binary(JavaScriptParser.ParseBinaryOperator(op), left, right, location);
        }

        public static BinaryExpression Binary(BinaryOperator op, Expression left, Expression right, Location location)
        {
            return new BinaryExpression(op, left, right, location);
        }

        public static LogicalExpression Logical(LogicalOperator op, Expression left, Expression right, Location location)
        {
            return new LogicalExpression(op, left, right, location);
        }
        
        public static BlockStatement Block(IEnumerable<Statement> body, Location location)
        {
            return new BlockStatement(body, location);
        }

        public static BreakStatement Break(Identifier label, Location location)
        {
            return new BreakStatement(label, location);
        }

        public static CallExpression Call(Expression callee, IEnumerable<Expression> args, Location location)
        {
            return new CallExpression(callee, args, location);
        }

        public static CatchClause Catch(Identifier param, BlockStatement body, Location location)
        {
            return new CatchClause(param, body, location);
        }

        public static ConditionalExpression Conditional(Expression test, Expression consequent,
            Expression alternate, Location location)
        {
            return new ConditionalExpression(test, consequent, alternate, location);
        }

        public static ContinueStatement Continue(Identifier label, Location location)
        {
            return new ContinueStatement(label, location);
        }

        public static DebuggerStatement Debugger(Location location)
        {
            return new DebuggerStatement(location);
        }

        public static DoWhileStatement DoWhile(Statement body, Expression test, Location location)
        {
            return new DoWhileStatement(body, test, location);
        }

        public static EmptyStatement Empty(Location location)
        {
            return new EmptyStatement(location);
        }

        public static ExpressionStatement Expression(Expression expression, Location location)
        {
            return new ExpressionStatement(expression, location);
        }

        public static ForStatement For(SyntaxNode init, Expression test, Expression update, Statement body, Location location)
        {
            return new ForStatement(init, test, update, body, location);
        }

        public static ForInStatement ForIn(SyntaxNode left, Expression right, Statement body, Location location)
        {
            return new ForInStatement(left, right, body, false, location);
        }

        public static FunctionDeclaration FunctionDeclaration(Identifier id, IEnumerable<Identifier> parameters,
            IEnumerable<Expression> defaults, Statement body, bool strict, Location location)
        {
            return new FunctionDeclaration(id, parameters, body, strict, location);
        }

        public static FunctionExpression Function(Identifier id, IEnumerable<Identifier> parameters,
            IEnumerable<Expression> defaults, Statement body, bool strict, Location location)
        {
            return new FunctionExpression(id, parameters, body, strict, location);
        }

        public static Identifier Identifier(string name, Location location)
        {
            return new Identifier(name, location);
        }

        public static IfStatement If(Expression test, Statement consequent, Statement alternate, Location location)
        {
            return new IfStatement(test, consequent, alternate, location);
        }

        public static LabelledStatement LabeledStatement(Identifier label, Statement body, Location location)
        {
            return new LabelledStatement(label, body, location);
        }

        public static Literal Literal(object value, string raw, Location location)
        {
            return new Literal(value, raw, location);
        }

        public static MemberExpression Member(char accessor, Expression obj, Expression property, Location location)
        {
            return new MemberExpression(obj, property, accessor == '[', location);
        }

        public static NewExpression New(Expression callee, IEnumerable<Expression> args, Location location)
        {
            return new NewExpression(callee, args, location);
        }

        public static ObjectExpression Object(IEnumerable<Property> properties, Location location)
        {
            return new ObjectExpression(properties, location);
        }

        public static UpdateExpression Postfix(UpdateOperator op, Expression argument, Location location)
        {
            return new UpdateExpression(op, argument, false, location);
        }

        public static Program Program(ICollection<Statement> body, bool strict, Location location)
        {
            return new Program(body, strict, location);
        }

        public static Property Property(PropertyKind kind, IPropertyKeyExpression key, Expression value, Location location)
        {
            return new Property(kind, key, value, location);
        }

        public static ReturnStatement Return(Expression argument, Location location)
        {
            return new ReturnStatement(argument, location);
        }

        public static SequenceExpression Sequence(IList<Expression> expressions, Location location)
        {
            return new SequenceExpression(expressions, location);
        }

        public static SwitchCase SwitchCase(Expression test, IEnumerable<Statement> consequent, Location location)
        {
            return new SwitchCase(test, consequent, location);
        }

        public static SwitchStatement Switch(Expression discriminant, IEnumerable<SwitchCase> cases, Location location)
        {
            return new SwitchStatement(discriminant, cases, location);
        }

        public static ThisExpression This(Location location)
        {
            return new ThisExpression(location);
        }

        public static ThrowStatement Throw(Expression argument, Location location)
        {
            return new ThrowStatement(argument, location);
        }

        public static TryStatement Try(Statement block, IEnumerable<Statement> guardedHandlers,
            IEnumerable<CatchClause> handlers, Statement finalizer, Location location)
        {
            return new TryStatement(block, guardedHandlers, handlers, finalizer, location);
        }

        public static UnaryExpression Unary(UnaryOperator op, Expression argument, Location location)
        {
            return new UnaryExpression(op, argument, true, location);
        }

        public static UpdateExpression Update(UpdateOperator op, Expression argument, Location location)
        {
            return new UpdateExpression(op, argument, true, location);
        }

        public static VariableDeclaration VariableDeclaration(IEnumerable<VariableDeclarator> declarations, string kind, Location location)
        {
            return new VariableDeclaration(declarations, kind, location);
        }

        public static VariableDeclarator VariableDeclarator(Identifier id, Expression init, Location location)
        {
            return new VariableDeclarator(id, init, location);
        }

        public static WhileStatement While(Expression test, Statement body, Location location)
        {
            return new WhileStatement(test, body, location);
        }

        public static WithStatement With(Expression obj, Statement body, Location location)
        {
            return new WithStatement(obj, body, location);
        }
    }

    delegate T Unmarked<out T>(Location location);

    partial class SyntaxNodeFactory
    {
        // Partially applied (up to but not including location) versions 
        // overloads SyntaxNodeFactory methods.

        internal static Unmarked<ArrayExpression> Array(IEnumerable<Expression> elements) { return loc => Array(elements, loc); }
        internal static Unmarked<AssignmentExpression> Assignment(AssignmentOperator op, Expression left, Expression right) { return loc => Assignment(op, left, right, loc); }
        internal static Unmarked<Expression> BinaryOrLogical(string op, Expression left, Expression right) { return loc => BinaryOrLogical(op, left, right, loc); }
        internal static Unmarked<BinaryExpression> Binary(BinaryOperator op, Expression left, Expression right) { return loc => Binary(op, left, right, loc); }
        internal static Unmarked<LogicalExpression> Logical(LogicalOperator op, Expression left, Expression right) { return loc => Logical(op, left, right, loc); }
        internal static Unmarked<BlockStatement> Block(IEnumerable<Statement> body) { return loc => Block(body, loc); }
        internal static Unmarked<BreakStatement> Break(Identifier label) { return loc => Break(label, loc); }
        internal static Unmarked<CallExpression> Call(Expression callee, IEnumerable<Expression> args) { return loc => Call(callee, args, loc); }
        internal static Unmarked<CatchClause> Catch(Identifier param, BlockStatement body) { return loc => Catch(param, body, loc); }
        internal static Unmarked<ConditionalExpression> Conditional(Expression test, Expression consequent, Expression alternate) { return loc => new ConditionalExpression(test, consequent, alternate, loc); }
        internal static Unmarked<ContinueStatement> Continue(Identifier label) { return loc => Continue(label, loc); }
        internal static Unmarked<DebuggerStatement> Debugger() { return loc => new DebuggerStatement(loc); }
        internal static Unmarked<DoWhileStatement> DoWhile(Statement body, Expression test) { return loc => DoWhile(body, test, loc); }
        internal static Unmarked<EmptyStatement> Empty() { return Empty; }
        internal static Unmarked<ExpressionStatement> Expression(Expression expression) { return loc => Expression(expression, loc); }
        internal static Unmarked<ForStatement> For(SyntaxNode init, Expression test, Expression update, Statement body) { return loc => For(init, test, update, body, loc); }
        internal static Unmarked<ForInStatement> ForIn(SyntaxNode left, Expression right, Statement body) { return loc => ForIn(left, right, body, loc); }
        internal static Unmarked<FunctionDeclaration> FunctionDeclaration(Identifier id, IEnumerable<Identifier> parameters, IEnumerable<Expression> defaults, Statement body, bool strict) { return loc => FunctionDeclaration(id, parameters, defaults, body, strict, loc); }
        internal static Unmarked<FunctionExpression> Function(Identifier id, IEnumerable<Identifier> parameters, IEnumerable<Expression> defaults, Statement body, bool strict) { return loc => new FunctionExpression(id, parameters, body, strict, loc); }
        internal static Unmarked<Identifier> Identifier(string name) { return loc => Identifier(name, loc); }
        internal static Unmarked<IfStatement> If(Expression test, Statement consequent, Statement alternate) { return loc => If(test, consequent, alternate, loc); }
        internal static Unmarked<LabelledStatement> LabeledStatement(Identifier label, Statement body) { return loc => LabeledStatement(label, body, loc); }
        internal static Unmarked<Literal> Literal(object value, string raw) { return loc => Literal(value, raw, loc); }
        internal static Unmarked<MemberExpression> Member(char accessor, Expression obj, Expression property) { return loc => Member(accessor, obj, property, loc); }
        internal static Unmarked<NewExpression> New(Expression callee, IEnumerable<Expression> args) { return loc => New(callee, args, loc); }
        internal static Unmarked<ObjectExpression> Object(IEnumerable<Property> properties) { return loc => Object(properties, loc); }
        internal static Unmarked<UpdateExpression> Postfix(UpdateOperator op, Expression argument) { return loc => Postfix(op, argument, loc); }
        internal static Unmarked<Program> Program(ICollection<Statement> body, bool strict) { return loc => Program(body, strict, loc); }
        internal static Unmarked<Property> Property(PropertyKind kind, IPropertyKeyExpression key, Expression value) { return loc => Property(kind, key, value, loc); }
        internal static Unmarked<ReturnStatement> Return(Expression argument) { return loc => Return(argument, loc); }
        internal static Unmarked<SequenceExpression> Sequence(IList<Expression> expressions) { return loc => Sequence(expressions, loc); }
        internal static Unmarked<SwitchCase> SwitchCase(Expression test, IEnumerable<Statement> consequent) { return loc => SwitchCase(test, consequent, loc); }
        internal static Unmarked<SwitchStatement> Switch(Expression discriminant, IEnumerable<SwitchCase> cases) { return loc => Switch(discriminant, cases, loc); }
        internal static Unmarked<ThisExpression> This() { return This; }
        internal static Unmarked<ThrowStatement> Throw(Expression argument) { return loc => Throw(argument, loc); }
        internal static Unmarked<TryStatement> Try(Statement block, IEnumerable<Statement> guardedHandlers, IEnumerable<CatchClause> handlers, Statement finalizer) { return loc => Try(block, guardedHandlers, handlers, finalizer, loc); }
        internal static Unmarked<UnaryExpression> Unary(UnaryOperator op, Expression argument) { return loc => Unary(op, argument, loc); }
        internal static Unmarked<UpdateExpression> Update(UpdateOperator op, Expression argument) { return loc => Update(op, argument, loc); }
        internal static Unmarked<VariableDeclaration> VariableDeclaration(IEnumerable<VariableDeclarator> declarations, string kind) { return loc => VariableDeclaration(declarations, kind, loc); }
        internal static Unmarked<VariableDeclarator> VariableDeclarator(Identifier id, Expression init) { return loc => VariableDeclarator(id, init, loc); }
        internal static Unmarked<WhileStatement> While(Expression test, Statement body) { return loc => While(test, body, loc); }
        internal static Unmarked<WithStatement> With(Expression obj, Statement body) { return loc => With(obj, body, loc); }    }
}