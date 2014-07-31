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
            return new ArrayExpression
                   {
                       NodeType = SyntaxNodeType.ArrayExpression,
                       Elements = elements
                   };
        }

        public static AssignmentExpression Assignment(string op, Expression left, Expression right)
        {
            return new AssignmentExpression
                   {
                       NodeType = SyntaxNodeType.AssignmentExpression,
                       Operator = AssignmentExpression.ParseAssignmentOperator(op),
                       Left = left,
                       Right = right
                   };
        }

        public static Expression Binary(string op, Expression left, Expression right)
        {
            
            return (op == "||" || op == "&&")
                ? (Expression)new LogicalExpression
                              {
                                  NodeType = SyntaxNodeType.LogicalExpression,
                                  Operator = LogicalExpression.ParseLogicalOperator(op),
                                  Left = left,
                                  Right = right
                              }
                : new BinaryExpression
                  {
                      NodeType = SyntaxNodeType.BinaryExpression,
                      Operator = BinaryExpression.ParseBinaryOperator(op),
                      Left = left,
                      Right = right
                  };
        }

        public static BlockStatement Block(IEnumerable<Statement> body)
        {
            return new BlockStatement
                   {
                       NodeType = SyntaxNodeType.BlockStatement,
                       Body = body
                   };
        }

        public static BreakStatement Break(Identifier label)
        {
            return new BreakStatement
                   {
                       NodeType = SyntaxNodeType.BreakStatement,
                       Label = label
                   };
        }

        public static CallExpression Call(Expression callee, IEnumerable<Expression> args)
        {
            return new CallExpression
                   {
                       NodeType = SyntaxNodeType.CallExpression,
                       Callee = callee,
                       Arguments = args
                   };
        }

        public static CatchClause Catch(Identifier param, BlockStatement body)
        {
            return new CatchClause
                   {
                       NodeType = SyntaxNodeType.CatchClause,
                       Param = param,
                       Body = body
                   };
        }

        public static ConditionalExpression Conditional(Expression test, Expression consequent,
            Expression alternate)
        {
            return new ConditionalExpression
                   {
                       NodeType = SyntaxNodeType.ConditionalExpression,
                       Test = test,
                       Consequent = consequent,
                       Alternate = alternate
                   };
        }

        public static ContinueStatement Continue(Identifier label)
        {
            return new ContinueStatement
                   {
                       NodeType = SyntaxNodeType.ContinueStatement,
                       Label = label
                   };
        }

        public static DebuggerStatement Debugger()
        {
            return new DebuggerStatement
                   {
                       NodeType = SyntaxNodeType.DebuggerStatement
                   };
        }

        public static DoWhileStatement DoWhile(Statement body, Expression test)
        {
            return new DoWhileStatement
                   {
                       NodeType = SyntaxNodeType.DoWhileStatement,
                       Body = body,
                       Test = test
                   };
        }

        public static EmptyStatement Empty() // TODO singleton?
        {
            return new EmptyStatement
                   {
                       NodeType = SyntaxNodeType.EmptyStatement
                   };
        }

        public static ExpressionStatement Expression(Expression expression)
        {
            return new ExpressionStatement
                   {
                       NodeType = SyntaxNodeType.ExpressionStatement,
                       Expression = expression
                   };
        }

        public static ForStatement For(SyntaxNode init, Expression test, Expression update, Statement body)
        {
            return new ForStatement
                   {
                       NodeType = SyntaxNodeType.ForStatement,
                       Init = init,
                       Test = test,
                       Update = update,
                       Body = body
                   };
        }

        public static ForInStatement ForIn(SyntaxNode left, Expression right, Statement body)
        {
            return new ForInStatement
                   {
                       NodeType = SyntaxNodeType.ForInStatement,
                       Left = left,
                       Right = right,
                       Body = body,
                       Each = false
                   };
        }

        public static FunctionDeclaration FunctionDeclaration(Identifier id, IEnumerable<Identifier> parameters,
            IEnumerable<Expression> defaults, Statement body, bool strict)
        {
            return new FunctionDeclaration
                   {
                       NodeType = SyntaxNodeType.FunctionDeclaration,
                       Id = id,
                       Parameters = parameters,
                       Defaults = defaults,
                       Body = body,
                       Strict = strict,
                       Rest = null,
                       Generator = false,
                       Expression = false,
                   };
        }

        public static FunctionExpression Function(Identifier id, IEnumerable<Identifier> parameters,
            IEnumerable<Expression> defaults, Statement body, bool strict)
        {
            return new FunctionExpression
                   {
                       NodeType = SyntaxNodeType.FunctionExpression,
                       Id = id,
                       Parameters = parameters,
                       Defaults = defaults,
                       Body = body,
                       Strict = strict,
                       Rest = null,
                       Generator = false,
                       Expression = false,
                   };
        }

        public static Identifier Identifier(string name)
        {
            return new Identifier
                   {
                       NodeType = SyntaxNodeType.Identifier,
                       Name = name
                   };
        }

        public static IfStatement If(Expression test, Statement consequent, Statement alternate)
        {
            return new IfStatement
                   {
                       NodeType = SyntaxNodeType.IfStatement,
                       Test = test,
                       Consequent = consequent,
                       Alternate = alternate
                   };
        }

        public static LabelledStatement LabeledStatement(Identifier label, Statement body)
        {
            return new LabelledStatement
                   {
                       NodeType = SyntaxNodeType.LabeledStatement,
                       Label = label,
                       Body = body
                   };
        }

        public static Literal Literal(/* TODO Review */ bool isRegExp, object value, string raw)
        {
            return new Literal
                   {
                       NodeType  = isRegExp
                           ? SyntaxNodeType.RegularExpressionLiteral
                           : SyntaxNodeType.Literal,
                       Value = value,
                       Raw   = raw
                   };
        }

        public static MemberExpression Member(char accessor, Expression obj, Expression property)
        {
            return new MemberExpression
                   {
                       NodeType = SyntaxNodeType.MemberExpression,
                       Computed = accessor == '[',
                       Object = obj,
                       Property = property
                   };
        }

        public static NewExpression New(Expression callee, IEnumerable<Expression> args)
        {
            return new NewExpression
                   {
                       NodeType = SyntaxNodeType.NewExpression,
                       Callee = callee,
                       Arguments = args
                   };
        }

        public static ObjectExpression Object(IEnumerable<Property> properties)
        {
            return new ObjectExpression
                   {
                       NodeType = SyntaxNodeType.ObjectExpression,
                       Properties = properties
                   };
        }

        public static UpdateExpression Postfix(string op, Expression argument)
        {
            return new UpdateExpression
                   {
                       NodeType = SyntaxNodeType.UpdateExpression,
                       Operator = UnaryExpression.ParseUnaryOperator(op),
                       Argument = argument,
                       Prefix = false
                   };
        }

        public static Program Program(ICollection<Statement> body, bool strict)
        {
            return new Program
                   {
                       NodeType = SyntaxNodeType.Program,
                       Body = body,
                       Strict = strict,
                   };
        }

        public static Property Property(PropertyKind kind, IPropertyKeyExpression key, Expression value)
        {
            return new Property
                   {
                       NodeType = SyntaxNodeType.Property,
                       Key = key,
                       Value = value,
                       Kind = kind
                   };
        }

        public static ReturnStatement Return(Expression argument)
        {
            return new ReturnStatement
                   {
                       NodeType = SyntaxNodeType.ReturnStatement,
                       Argument = argument
                   };
        }

        public static SequenceExpression Sequence(IList<Expression> expressions)
        {
            return new SequenceExpression
                   {
                       NodeType = SyntaxNodeType.SequenceExpression,
                       Expressions = expressions
                   };
        }

        public static SwitchCase SwitchCase(Expression test, IEnumerable<Statement> consequent)
        {
            return new SwitchCase
                   {
                       NodeType = SyntaxNodeType.SwitchCase,
                       Test = test,
                       Consequent = consequent
                   };
        }

        public static SwitchStatement Switch(Expression discriminant, IEnumerable<SwitchCase> cases)
        {
            return new SwitchStatement
                   {
                       NodeType = SyntaxNodeType.SwitchStatement,
                       Discriminant = discriminant,
                       Cases = cases
                   };
        }

        public static ThisExpression This()
        {
            return new ThisExpression
                   {
                       NodeType = SyntaxNodeType.ThisExpression
                   };
        }

        public static ThrowStatement Throw(Expression argument)
        {
            return new ThrowStatement
                   {
                       NodeType = SyntaxNodeType.ThrowStatement,
                       Argument = argument
                   };
        }

        public static TryStatement Try(Statement block, IEnumerable<Statement> guardedHandlers,
            IEnumerable<CatchClause> handlers, Statement finalizer)
        {
            return new TryStatement
                   {
                       NodeType = SyntaxNodeType.TryStatement,
                       Block = block,
                       GuardedHandlers = guardedHandlers,
                       Handlers = handlers,
                       Finalizer = finalizer
                   };
        }

        public static UnaryExpression Unary(string op, Expression argument)
        {
            if (op == "++" || op == "--")
            {
                return new UpdateExpression
                       {
                           NodeType = SyntaxNodeType.UpdateExpression,
                           Operator = UnaryExpression.ParseUnaryOperator(op),
                           Argument = argument,
                           Prefix = true
                       };
            }

            return new UnaryExpression
                   {
                       NodeType = SyntaxNodeType.UnaryExpression,
                       Operator = UnaryExpression.ParseUnaryOperator(op),
                       Argument = argument,
                       Prefix = true
                   };
        }

        public static VariableDeclaration VariableDeclaration(IEnumerable<VariableDeclarator> declarations, string kind)
        {
            return new VariableDeclaration
                   {
                       NodeType = SyntaxNodeType.VariableDeclaration,
                       Declarations = declarations,
                       Kind = kind
                   };
        }

        public static VariableDeclarator VariableDeclarator(Identifier id, Expression init)
        {
            return new VariableDeclarator
                   {
                       NodeType = SyntaxNodeType.VariableDeclarator,
                       Id = id,
                       Init = init
                   };
        }

        public static WhileStatement While(Expression test, Statement body)
        {
            return new WhileStatement
                   {
                       NodeType = SyntaxNodeType.WhileStatement,
                       Test = test,
                       Body = body
                   };
        }

        public static WithStatement With(Expression obj, Statement body)
        {
            return new WithStatement
                   {
                       NodeType = SyntaxNodeType.WithStatement,
                       Object = obj,
                       Body = body
                   };
        }
    }
}