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
        public IEnumerable<Expression> Elements { get; set; }
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
        public AssignmentOperator Operator { get; set; }
        public Expression Left { get; set; }
        public Expression Right { get; set; }

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
        public BinaryOperator Operator { get; set; }
        public Expression Left { get; set; }
        public Expression Right { get; set; }

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
        public IEnumerable<Statement> Body { get; set; }
    }

    public class BreakStatement : Statement
    {
        public Identifier Label { get; set; }
    }

    public class CallExpression : Expression
    {
        public Expression Callee { get; set; }
        public IEnumerable<Expression> Arguments { get; set; }
    }

    public class CatchClause : Statement
    {
        public Identifier Param { get; set; }
        public BlockStatement Body { get; set; }
    }

    public class ConditionalExpression : Expression
    {
        public Expression Test { get; set; }
        public Expression Consequent { get; set; }
        public Expression Alternate { get; set; }
    }

    public class ContinueStatement : Statement
    {
        public Identifier Label { get; set; }
    }

    public class DebuggerStatement: Statement {}

    public class DoWhileStatement  : Statement
    {
        public Statement Body { get; set; }
        public Expression Test { get; set; }
    }

    public class EmptyStatement : Statement {}

    public class Expression : SyntaxNode
    {
        // an expression represents an actual value
        // foo() is an expression, a switch/case is a statement
    }

    public class ExpressionStatement : Statement
    {
        public Expression Expression { get; set; }
    }

    public class ForInStatement : Statement
    {
        public SyntaxNode Left { get; set; }
        public Expression Right { get; set; }
        public Statement Body { get; set; }
        public bool Each { get; set; }
    }

    public class ForStatement : Statement
    {
        // can be a Statement (var i) or an Expression (i=0)
        public SyntaxNode Init { get; set; }
        public Expression Test { get; set; }
        public Expression Update { get; set; }
        public Statement Body { get; set; }
    }

    public class FunctionDeclaration : Statement
    {
        public Identifier Id { get; set; }
        public IEnumerable<Identifier> Parameters { get; set; }
        public Statement Body { get; set; }
        public bool Strict { get; set; }

        #region ECMA6
        
        public IEnumerable<Expression> Defaults { get; set; }
        public SyntaxNode Rest { get; set; }
        public bool Generator { get; set; }
        public bool Expression { get; set; }
        
        #endregion
    }

    public class FunctionExpression : Expression
    {
        public Identifier Id { get; set; }
        public IEnumerable<Identifier> Parameters { get; set; }
        public Statement Body { get; set; }
        public bool Strict { get; set; }

        #region ECMA6
        public IEnumerable<Expression> Defaults { get; set; }
        public SyntaxNode Rest { get; set; }
        public bool Generator { get; set; }
        public bool Expression { get; set; }
        #endregion
    }

    public class Identifier : Expression, IPropertyKeyExpression
    {
        public string Name { get; set; }
        
        public string GetKey()
        {
            return Name;
        }
    }

    public class IfStatement : Statement
    {
        public Expression Test { get; set; }
        public Statement Consequent { get; set; }
        public Statement Alternate { get; set; }
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
        public Identifier Label { get; set; }
        public Statement Body { get; set; }
    }

    public class Literal : Expression, IPropertyKeyExpression
    {
        public object Value { get; set; }
        public string Raw { get; set; }

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
        public LogicalOperator Operator { get; set; }
        public Expression Left { get; set; }
        public Expression Right { get; set; }
        
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
        public Expression Object { get; set; }
        public Expression Property { get; set; }

        // true if an indexer is used and the property to be evaluated
        public bool Computed { get; set; }
    }

    public class NewExpression : Expression
    {
        public Expression Callee { get; set; }
        public IEnumerable<Expression> Arguments { get; set; }
    }

    public class ObjectExpression : Expression
    {
        public IEnumerable<Property> Properties { get; set; }
    }

    public class Program : Statement
    {
        public ICollection<Statement> Body { get; set; }

        public List<Comment> Comments { get; set; }
        public List<Token> Tokens { get; set; }
        public List<ParserException> Errors { get; set; }
        public bool Strict { get; set; }
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
        public PropertyKind Kind { get; set; }
        public IPropertyKeyExpression Key { get; set; }
        public Expression Value { get; set; }
    }

    public class RegExpLiteral : Expression, IPropertyKeyExpression
    {
        public object Value { get; set; }
        public string Raw { get; set; }
        public string Flags { get; set; }
        
        public string GetKey()
        {
            return Value.ToString();
        }
    }

    public class ReturnStatement : Statement
    {
        public Expression Argument { get; set; }
    }

    public class SequenceExpression : Expression
    {
        public IList<Expression> Expressions { get; set; }
    }

    public class Statement : SyntaxNode
    {
        public string LabelSet { get; set; }
    }

    public class SwitchCase : SyntaxNode
    {
        public Expression Test { get; set; }
        public IEnumerable<Statement> Consequent { get; set; }
    }

    public class SwitchStatement : Statement
    {
        public Expression Discriminant { get; set; }
        public IEnumerable<SwitchCase> Cases { get; set; }
    }

    public class SyntaxNode
    {
        public SyntaxNodes Type { get; set; }
        public int[] Range;
        public Location Location { get; set; }

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
        public Expression Argument { get; set; }
    }

    public class TryStatement : Statement
    {
        public Statement Block { get; set; }
        public IEnumerable<Statement> GuardedHandlers { get; set; }
        public IEnumerable<CatchClause> Handlers { get; set; }
        public Statement Finalizer { get; set; }
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
        public UnaryOperator Operator { get; set; }
        public Expression Argument { get; set; }
        public bool Prefix { get; set; }

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
        public IEnumerable<VariableDeclarator> Declarations { get; set; }
        public string Kind { get; set; }
    }

    public class VariableDeclarator : Expression
    {
        public Identifier Id { get; set; }
        public Expression Init { get; set; }
    }

    public class WhileStatement : Statement
    {
        public Expression Test { get; set; }
        public Statement Body { get; set; }
    }

    public class WithStatement : Statement
    {
        public Expression Object { get; set; }
        public Statement Body { get; set; }
    }
}