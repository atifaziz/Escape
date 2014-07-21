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

namespace Escape.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class ArrayExpression : Expression
    {
        public IEnumerable<Expression> Elements;
    }

    public enum AssignmentOperator
    {
        Assign,
        PlusAssign,
        MinusAssign,
        TimesAssign,
        DivideAssign,
        ModuloAssign,
        BitwiseAndAssign,
        BitwiseOrAssign,
        BitwiseXOrAssign,
        LeftShiftAssign,
        RightShiftAssign,
        UnsignedRightShiftAssign,
    }

    public class AssignmentExpression : Expression
    {
        public AssignmentOperator Operator;
        public Expression Left;
        public Expression Right;

        public static AssignmentOperator ParseAssignmentOperator(string op)
        {
            switch (op)
            {
                case "=":
                    return AssignmentOperator.Assign;
                case "+=":
                    return AssignmentOperator.PlusAssign;
                case "-=":
                    return AssignmentOperator.MinusAssign;
                case "*=":
                    return AssignmentOperator.TimesAssign;
                case "/=":
                    return AssignmentOperator.DivideAssign;
                case "%=":
                    return AssignmentOperator.ModuloAssign;
                case "&=":
                    return AssignmentOperator.BitwiseAndAssign;
                case "|=":
                    return AssignmentOperator.BitwiseOrAssign;
                case "^=":
                    return AssignmentOperator.BitwiseXOrAssign;
                case "<<=":
                    return AssignmentOperator.LeftShiftAssign;
                case ">>=":
                    return AssignmentOperator.RightShiftAssign;
                case ">>>=":
                    return AssignmentOperator.UnsignedRightShiftAssign;

                default:
                    throw new ArgumentOutOfRangeException("Invalid assignment operator: " + op);
            }
        }
    }

    public enum BinaryOperator
    {
        Plus,
        Minus,
        Times,
        Divide,
        Modulo,
        Equal,
        NotEqual,
        Greater,
        GreaterOrEqual,
        Less,
        LessOrEqual,
        StrictlyEqual,
        StricltyNotEqual,
        BitwiseAnd,
        BitwiseOr,
        BitwiseXOr,
        LeftShift,
        RightShift,
        UnsignedRightShift,
        InstanceOf,
        In,
    }

    public class BinaryExpression : Expression
    {
        public BinaryOperator Operator;
        public Expression Left;
        public Expression Right;

        public static BinaryOperator ParseBinaryOperator(string op)
        {
            switch (op)
            {
                case "+":
                    return BinaryOperator.Plus;
                case "-":
                    return BinaryOperator.Minus;
                case "*":
                    return BinaryOperator.Times;
                case "/":
                    return BinaryOperator.Divide;
                case "%":
                    return BinaryOperator.Modulo;
                case "==":
                    return BinaryOperator.Equal;
                case "!=":
                    return BinaryOperator.NotEqual;
                case ">":
                    return BinaryOperator.Greater;
                case ">=":
                    return BinaryOperator.GreaterOrEqual;
                case "<":
                    return BinaryOperator.Less;
                case "<=":
                    return BinaryOperator.LessOrEqual;
                case "===":
                    return BinaryOperator.StrictlyEqual;
                case "!==":
                    return BinaryOperator.StricltyNotEqual;
                case "&":
                    return BinaryOperator.BitwiseAnd;
                case "|":
                    return BinaryOperator.BitwiseOr;
                case "^":
                    return BinaryOperator.BitwiseXOr;
                case "<<":
                    return BinaryOperator.LeftShift;
                case ">>":
                    return BinaryOperator.RightShift;
                case ">>>":
                    return BinaryOperator.UnsignedRightShift;
                case "instanceof":
                    return BinaryOperator.InstanceOf;
                case "in":
                    return BinaryOperator.In;

                default: 
                    throw new ArgumentOutOfRangeException("Invalid binary operator: " + op);
            }
        }
    }

    public class BlockStatement : Statement
    {
        public IEnumerable<Statement> Body;
    }

    public class BreakStatement : Statement
    {
        public Identifier Label;
    }

    public class CallExpression : Expression
    {
        public Expression Callee;
        public IEnumerable<Expression> Arguments;
    }

    public class CatchClause : Statement
    {
        public Identifier Param;
        public BlockStatement Body;
    }

    public class ConditionalExpression : Expression
    {
        public Expression Test;
        public Expression Consequent;
        public Expression Alternate;
    }

    public class ContinueStatement : Statement
    {
        public Identifier Label;
    }

    public class DebuggerStatement: Statement {}

    public class DoWhileStatement  : Statement
    {
        public Statement Body;
        public Expression Test;
    }

    public class EmptyStatement : Statement {}

    public class Expression : SyntaxNode
    {
        // an expression represents an actual value
        // foo() is an expression, a switch/case is a statement
    }

    public class ExpressionStatement : Statement
    {
        public Expression Expression;
    }

    public class ForInStatement : Statement
    {
        public SyntaxNode Left;
        public Expression Right;
        public Statement Body;
        public bool Each;
    }

    public class ForStatement : Statement
    {
        // can be a Statement (var i) or an Expression (i=0)
        public SyntaxNode Init;
        public Expression Test;
        public Expression Update;
        public Statement Body;
    }

    public class FunctionDeclaration : Statement, IFunctionDeclaration
    {
        public FunctionDeclaration()
        {
            VariableDeclarations = new List<VariableDeclaration>();
        }

        public Identifier Id { get; set; }
        public IEnumerable<Identifier> Parameters { get; set; }
        public Statement Body { get; set; }
        public bool Strict { get; set; }

        public IList<VariableDeclaration> VariableDeclarations { get; set; }

        #region ECMA6
        
        public IEnumerable<Expression> Defaults;
        public SyntaxNode Rest;
        public bool Generator;
        public bool Expression;
        
        #endregion

        public IList<FunctionDeclaration> FunctionDeclarations { get; set; }
    }

    public class FunctionExpression : Expression, IFunctionDeclaration
    {
        public FunctionExpression()
        {
            VariableDeclarations = new List<VariableDeclaration>();
        }

        public Identifier Id { get; set; }
        public IEnumerable<Identifier> Parameters { get; set; }
        public Statement Body { get; set; }
        public bool Strict { get; set; }

        public IList<VariableDeclaration> VariableDeclarations { get; set; }
        public IList<FunctionDeclaration> FunctionDeclarations { get; set; }

        #region ECMA6
        public IEnumerable<Expression> Defaults;
        public SyntaxNode Rest;
        public bool Generator;
        public bool Expression;
        #endregion
    }

    public class Identifier : Expression, IPropertyKeyExpression
    {
        public string Name;
        
        public string GetKey()
        {
            return Name;
        }
    }

    public class IfStatement : Statement
    {
        public Expression Test;
        public Statement Consequent;
        public Statement Alternate;
    }

    /// <summary>
    /// Reprensents an expression which can be a key of a Property
    /// </summary>
    public interface IPropertyKeyExpression
    {
        string GetKey();
    }

    public class LabelledStatement : Statement
    {
        public Identifier Label;
        public Statement Body;
    }

    public class Literal : Expression, IPropertyKeyExpression
    {
        public object Value;
        public string Raw;

        public string GetKey()
        {
            return Value.ToString();
        }
    }

    public enum LogicalOperator
    {
        LogicalAnd,
        LogicalOr
    }

    public class LogicalExpression : Expression
    {
        public LogicalOperator Operator;
        public Expression Left;
        public Expression Right;
        
        public static LogicalOperator ParseLogicalOperator(string op)
        {
            switch (op)
            {
                case "&&":
                    return LogicalOperator.LogicalAnd;
                case "||":
                    return LogicalOperator.LogicalOr;

                default:
                    throw new ArgumentOutOfRangeException("Invalid binary operator: " + op);
            }
        }
    }

    public class MemberExpression : Expression
    {
        public Expression Object;
        public Expression Property;

        // true if an indexer is used and the property to be evaluated
        public bool Computed;
    }

    public class NewExpression : Expression
    {
        public Expression Callee;
        public IEnumerable<Expression> Arguments;
    }

    public class ObjectExpression : Expression
    {
        public IEnumerable<Property> Properties;
    }

    public class Program : Statement, IVariableScope, IFunctionScope
    {
        public Program()
        {
            VariableDeclarations = new List<VariableDeclaration>();
        }
        public ICollection<Statement> Body;

        public List<Comment> Comments;
        public List<Token> Tokens;
        public List<ParserException> Errors;
        public bool Strict;

        public IList<VariableDeclaration> VariableDeclarations { get; set; }
        public IList<FunctionDeclaration> FunctionDeclarations { get; set; }
    }

    [Flags]
    public enum PropertyKind
    {
        Data = 1,
        Get = 2,
        Set = 4
    };

    public class Property : Expression
    {
        public PropertyKind Kind;
        public IPropertyKeyExpression Key;
        public Expression Value;
    }

    public class RegExpLiteral : Expression, IPropertyKeyExpression
    {
        public object Value;
        public string Raw;
        public string Flags;
        
        public string GetKey()
        {
            return Value.ToString();
        }
    }

    public class ReturnStatement : Statement
    {
        public Expression Argument;
    }

    public class SequenceExpression : Expression
    {
        public IList<Expression> Expressions;
    }

    public class Statement : SyntaxNode
    {
        public string LabelSet;
    }

    public class SwitchCase : SyntaxNode
    {
        public Expression Test;
        public IEnumerable<Statement> Consequent;
    }

    public class SwitchStatement : Statement
    {
        public Expression Discriminant;
        public IEnumerable<SwitchCase> Cases;
    }

    public class SyntaxNode
    {
        public SyntaxNodes Type;
        public int[] Range;
        public Location Location;

        [DebuggerStepThrough]
        public T As<T>() where T : SyntaxNode
        {
            return (T)this;
        }
    }

    public enum SyntaxNodes
    {
        AssignmentExpression,
        ArrayExpression,
        BlockStatement,
        BinaryExpression,
        BreakStatement,
        CallExpression,
        CatchClause,
        ConditionalExpression,
        ContinueStatement,
        DoWhileStatement,
        DebuggerStatement,
        EmptyStatement,
        ExpressionStatement,
        ForStatement,
        ForInStatement,
        FunctionDeclaration,
        FunctionExpression,
        Identifier,
        IfStatement,
        Literal,
        RegularExpressionLiteral,
        LabeledStatement,
        LogicalExpression,
        MemberExpression,
        NewExpression,
        ObjectExpression,
        Program,
        Property,
        ReturnStatement,
        SequenceExpression,
        SwitchStatement,
        SwitchCase,
        ThisExpression,
        ThrowStatement,
        TryStatement,
        UnaryExpression,
        UpdateExpression,
        VariableDeclaration,
        VariableDeclarator,
        WhileStatement,
        WithStatement
    };

    public class ThisExpression : Expression {}

    public class ThrowStatement : Statement
    {
        public Expression Argument;
    }

    public class TryStatement : Statement
    {
        public Statement Block;
        public IEnumerable<Statement> GuardedHandlers;
        public IEnumerable<CatchClause> Handlers;
        public Statement Finalizer;
    }

    public enum UnaryOperator
    {
        Plus,
        Minus,
        BitwiseNot,
        LogicalNot,
        Delete,
        Void,
        TypeOf,
        Increment,
        Decrement,
    }

    public class UnaryExpression : Expression
    {
        public UnaryOperator Operator;
        public Expression Argument;
        public bool Prefix;

        public static UnaryOperator ParseUnaryOperator(string op)
        {
            switch (op)
            {
                case "+":
                    return UnaryOperator.Plus;
                case "-":
                    return UnaryOperator.Minus;
                case "++":
                    return UnaryOperator.Increment;
                case "--":
                    return UnaryOperator.Decrement;
                case "~":
                    return UnaryOperator.BitwiseNot;
                case "!":
                    return UnaryOperator.LogicalNot;
                case "delete":
                    return UnaryOperator.Delete;
                case "void":
                    return UnaryOperator.Void;
                case "typeof":
                    return UnaryOperator.TypeOf;
                default:
                    throw new ArgumentOutOfRangeException("Invalid unary operator: " + op);
            }
        }
    }

    public class UpdateExpression : UnaryExpression {}

    public class VariableDeclaration : Statement
    {
        public IEnumerable<VariableDeclarator> Declarations;
        public string Kind;
    }

    public class VariableDeclarator : Expression
    {
        public Identifier Id;
        public Expression Init;
    }

    public class WhileStatement : Statement
    {
        public Expression Test;
        public Statement Body;
    }

    public class WithStatement : Statement
    {
        public Expression Object;
        public Statement Body;
    }
}