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
        public IEnumerable<Expression> Elements { get; private set; }
        
        public ArrayExpression(IEnumerable<Expression> elements, Location location = null) : 
            base(SyntaxNodeType.ArrayExpression, location)
        {
            if (elements == null) throw new ArgumentNullException("elements");
            Elements = elements;
        }
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
        public AssignmentOperator Operator { get; private set; }
        public Expression Left { get; private set; }
        public Expression Right { get; private set; }

        public AssignmentExpression(AssignmentOperator op, Expression left, Expression right, Location location = null) : 
            base(SyntaxNodeType.AssignmentExpression, location)
        {
            if (!FastEnumValidator<AssignmentOperator>.IsDefined((int) op)) throw new ArgumentOutOfRangeException("op");
            if (left == null) throw new ArgumentNullException("left");
            if (right == null) throw new ArgumentNullException("right");
            Operator = op;
            Left = left;
            Right = right;
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
        public BinaryOperator Operator { get; private set; }
        public Expression Left { get; private set; }
        public Expression Right { get; private set; }

        public BinaryExpression(BinaryOperator op, Expression left, Expression right, Location location = null) : 
            base(SyntaxNodeType.BinaryExpression, location)
        {
            if (!FastEnumValidator<BinaryOperator>.IsDefined((int) op)) throw new ArgumentOutOfRangeException("op");
            if (left == null) throw new ArgumentNullException("left");
            if (right == null) throw new ArgumentNullException("right");
            Operator = op;
            Left = left;
            Right = right;
        }
    }

    public class BlockStatement : Statement
    {
        public IEnumerable<Statement> Body { get; private set; }

        public BlockStatement(IEnumerable<Statement> body, Location location = null) : 
            base(SyntaxNodeType.BlockStatement, location)
        {
            if (body == null) throw new ArgumentNullException("body");
            Body = body;
        }
    }

    public class BreakStatement : Statement
    {
        public Identifier Label { get; private set; }

        public BreakStatement(Location location = null) : this(null, location) { }
        public BreakStatement(Identifier label, Location location = null) : 
            base(SyntaxNodeType.BreakStatement, location)
        {
            Label = label;
        }
    }

    public class CallExpression : Expression
    {
        public Expression Callee { get; private set; }
        public IEnumerable<Expression> Arguments { get; private set; }
        
        public CallExpression(Expression callee, IEnumerable<Expression> arguments, Location location = null) : 
            base(SyntaxNodeType.CallExpression, location)
        {
            if (callee == null) throw new ArgumentNullException("callee");
            if (arguments == null) throw new ArgumentNullException("arguments");
            Callee = callee;
            Arguments = arguments;
        }
    }

    public class CatchClause : Statement
    {
        public Identifier Param { get; private set; }
        public BlockStatement Body { get; private set; }
        
        public CatchClause(Identifier param, BlockStatement body, Location location = null) : 
            base(SyntaxNodeType.CatchClause, location)
        {
            if (param == null) throw new ArgumentNullException("param");
            if (body == null) throw new ArgumentNullException("body");
            Param = param;
            Body = body;
        }
    }

    public class ConditionalExpression : Expression
    {
        public Expression Test { get; private set; }
        public Expression Consequent { get; private set; }
        public Expression Alternate { get; private set; }
        
        public ConditionalExpression(Expression test, Expression consequent, Expression alternate, Location location = null) : 
            base(SyntaxNodeType.ConditionalExpression, location)
        {
            if (test == null) throw new ArgumentNullException("test");
            if (consequent == null) throw new ArgumentNullException("consequent");
            if (alternate == null) throw new ArgumentNullException("alternate");
            Test = test;
            Alternate = alternate;
            Consequent = consequent;
        }
    }

    public class ContinueStatement : Statement
    {
        public Identifier Label { get; private set; }

        public ContinueStatement(Location location = null) : this(null, location) { }
        public ContinueStatement(Identifier label, Location location = null) : 
            base(SyntaxNodeType.ContinueStatement, location)
        {
            Label = label;
        }
    }

    public class DebuggerStatement: Statement
    {
        public DebuggerStatement(Location location = null) : 
            base(SyntaxNodeType.DebuggerStatement, location) {}
    }

    public class DoWhileStatement  : Statement
    {
        public Statement Body { get; private set; }
        public Expression Test { get; private set; }

        public DoWhileStatement(Statement body, Expression test, Location location = null) : 
            base(SyntaxNodeType.DoWhileStatement, location)
        {
            if (body == null) throw new ArgumentNullException("body");
            if (test == null) throw new ArgumentNullException("test");
            Test = test;
            Body = body;
        }
    }

    public class EmptyStatement : Statement
    {
        public EmptyStatement(Location location = null) : 
            base(SyntaxNodeType.EmptyStatement, location) {}
    }

    public abstract class Expression : SyntaxNode
    {
        // an expression represents an actual value
        // foo() is an expression, a switch/case is a statement
        protected Expression(SyntaxNodeType nodeType, Location location = null) : 
            base(nodeType, location) {}
    }

    public class ExpressionStatement : Statement
    {
        public Expression Expression { get; private set; }
        
        public ExpressionStatement(Expression expression, Location location = null) : 
            base(SyntaxNodeType.ExpressionStatement, location)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            Expression = expression;
        }
    }

    public class ForInStatement : Statement
    {
        public SyntaxNode Left { get; private set; }
        public Expression Right { get; private set; }
        public Statement Body { get; private set; }
        public bool Each { get; private set; }

        public ForInStatement(SyntaxNode left, Expression right, Statement body) : 
            this(left, right, body, false) {}

        public ForInStatement(SyntaxNode left, Expression right, Statement body, bool each, Location location = null) : 
            base(SyntaxNodeType.ForInStatement, location)
        {
            if (left == null) throw new ArgumentNullException("left");
            if (right == null) throw new ArgumentNullException("right");
            if (body == null) throw new ArgumentNullException("body");
            Left = left;
            Right = right;
            Body = body;
            Each = each;
        }
    }

    public class ForStatement : Statement
    {
        // can be a Statement (var i) or an Expression (i=0)
        public SyntaxNode Init { get; private set; }
        public Expression Test { get; private set; }
        public Expression Update { get; private set; }
        public Statement Body { get; private set; }
        
        public ForStatement(SyntaxNode init, Expression test, Expression update, Statement body, Location location = null) : 
            base(SyntaxNodeType.ForStatement, location)
        {
            if (body == null) throw new ArgumentNullException("body");
            Body = body;
            Update = update;
            Test = test;
            Init = init;
        }
    }

    public class FunctionDeclaration : Statement
    {
        public Identifier Id { get; private set; }
        public IEnumerable<Identifier> Parameters { get; private set; }
        public Statement Body { get; private set; }
        public bool Strict { get; private set; }

        public FunctionDeclaration(Identifier id, 
            IEnumerable<Identifier> parameters, Statement body) : 
            this(id, parameters, body, false) {}

        public FunctionDeclaration(Identifier id, 
            IEnumerable<Identifier> parameters, 
            Statement body, bool strict, Location location = null) : 
            base(SyntaxNodeType.FunctionDeclaration, location)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (parameters == null) throw new ArgumentNullException("parameters");
            if (body == null) throw new ArgumentNullException("body");
            Id = id;
            Parameters = parameters;
            Body = body;
            Strict = strict;
        }

        #region ECMA6

        // ReSharper disable UnusedAutoPropertyAccessor.Local        
        public IEnumerable<Expression> Defaults { get { yield break; } }
        public SyntaxNode Rest { get; private set; }
        public bool Generator { get; private set; }
        public bool Expression { get; private set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Local
        
        #endregion 
    }

    public class FunctionExpression : Expression
    {
        public Identifier Id { get; private set; }
        public IEnumerable<Identifier> Parameters { get; private set; }
        public Statement Body { get; private set; }
        public bool Strict { get; private set; }

        public FunctionExpression(IEnumerable<Identifier> parameters, Statement body) : 
            this(null, parameters, body) {}

        public FunctionExpression(Identifier id, 
            IEnumerable<Identifier> parameters, Statement body) : 
            this(id, parameters, body, false) {}

        public FunctionExpression(IEnumerable<Identifier> parameters, 
            Statement body, bool strict) : 
            this(null, parameters, body, strict) {}

        public FunctionExpression(Identifier id, 
            IEnumerable<Identifier> parameters, 
            Statement body, bool strict, Location location = null) : 
            base(SyntaxNodeType.FunctionExpression, location)
        {
            if (parameters == null) throw new ArgumentNullException("parameters");
            if (body == null) throw new ArgumentNullException("body");
            Id = id;
            Parameters = parameters;
            Body = body;
            Strict = strict;
        }

        #region ECMA6
        
        // ReSharper disable UnusedAutoPropertyAccessor.Local
        public IEnumerable<Expression> Defaults { get { yield break; } }
        public SyntaxNode Rest { get; private set; }
        public bool Generator { get; private set; }
        public bool Expression { get; private set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Local
        
        #endregion
    }

    public class Identifier : Expression, IPropertyKeyExpression
    {
        public string Name { get; private set; }
        
        public Identifier(string name, Location location = null) : 
            base(SyntaxNodeType.Identifier, location)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (name.Length == 0) throw new ArgumentException(null, "name");
            Name = name;
        }

        public string GetKey() { return Name; }
    }

    public class IfStatement : Statement
    {
        public Expression Test { get; private set; }
        public Statement Consequent { get; private set; }
        public Statement Alternate { get; private set; }

        public IfStatement(Expression test, Statement consequent) : 
            this(test, consequent, null) {}

        public IfStatement(Expression test, Statement consequent, Statement alternate, Location location = null) : 
            base(SyntaxNodeType.IfStatement, location)
        {
            if (test == null) throw new ArgumentNullException("test");
            if (consequent == null) throw new ArgumentNullException("consequent");
            Test = test;
            Alternate = alternate;
            Consequent = consequent;
        }
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
        public Identifier Label { get; private set; }
        public Statement Body { get; private set; }
        public LabelledStatement(Identifier label, Statement body, Location location = null) : 
            base(SyntaxNodeType.LabeledStatement, location)
        {
            if (label == null) throw new ArgumentNullException("label");
            if (body == null) throw new ArgumentNullException("body");
            Label = label;
            Body = body;
        }
    }

    public class Literal : Expression, IPropertyKeyExpression
    {
        public object Value { get; private set; }
        public string Raw { get; private set; }

        public Literal(object value, string raw, Location location = null) : 
            base(SyntaxNodeType.Literal, location)
        {
            Raw = raw;
            Value = value;
        }

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
        public LogicalOperator Operator { get; private set; }
        public Expression Left { get; private set; }
        public Expression Right { get; private set; }

        public LogicalExpression(LogicalOperator op, Expression left, Expression right, Location location = null) :
            base(SyntaxNodeType.LogicalExpression, location)
        {
            if (!FastEnumValidator<LogicalOperator>.IsDefined((int) op)) throw new ArgumentOutOfRangeException("op");
            if (left == null) throw new ArgumentNullException("left");
            if (right == null) throw new ArgumentNullException("right");
            Operator = op;
            Left = left;
            Right = right;
        }
    }

    public class MemberExpression : Expression
    {
        public Expression Object { get; private set; }
        public Expression Property { get; private set; }
        public bool Computed { get; private set; } // true if an indexer is used and the property to be evaluated

        public MemberExpression(Expression obj, Expression property, bool computed, Location location = null) : 
            base(SyntaxNodeType.MemberExpression, location)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (property == null) throw new ArgumentNullException("property");
            Object = obj;
            Property = property;
            Computed = computed;
        }
    }

    public class NewExpression : Expression
    {
        public Expression Callee { get; private set; }
        public IEnumerable<Expression> Arguments { get; private set; }
        
        public NewExpression(Expression callee, IEnumerable<Expression> arguments, Location location = null) : 
            base(SyntaxNodeType.NewExpression, location)
        {
            if (callee == null) throw new ArgumentNullException("callee");
            if (arguments == null) throw new ArgumentNullException("arguments");
            Arguments = arguments;
            Callee = callee;
        }
    }

    public class ObjectExpression : Expression
    {
        public IEnumerable<Property> Properties { get; private set; }
        
        public ObjectExpression(IEnumerable<Property> properties, Location location = null) : 
            base(SyntaxNodeType.ObjectExpression, location)
        {
            if (properties == null) throw new ArgumentNullException("properties");
            Properties = properties;
        }
    }

    public class Program : Statement
    {
        public ICollection<Statement> Body { get; private set; }

        public List<Comment> Comments { get; set; }
        public List<Token> Tokens { get; set; }
        public List<ParserException> Errors { get; set; }
        public bool Strict { get; private set; }

        public Program(ICollection<Statement> body) : this(body, false) {}

        public Program(ICollection<Statement> body, bool strict, Location location = null) : 
            base(SyntaxNodeType.Program, location)
        {
            if (body == null) throw new ArgumentNullException("body");
            Body = body;
            Strict = strict;
        }
    }

    public enum PropertyKind { Init, Get, Set };

    public class Property : Expression
    {
        public PropertyKind Kind { get; private set; }
        public IPropertyKeyExpression Key { get; private set; }
        public Expression Value { get; private set; }
        
        public Property(PropertyKind kind, IPropertyKeyExpression key, Expression value, Location location = null) : 
            base(SyntaxNodeType.Property, location)
        {
            if (!FastEnumValidator<PropertyKind>.IsDefined((int) kind)) throw new ArgumentOutOfRangeException("kind");
            if (key == null) throw new ArgumentNullException("key");
            if (value == null) throw new ArgumentNullException("value");
            Value = value;
            Key = key;
            Kind = kind;
        }
    }

    public class ReturnStatement : Statement
    {
        public Expression Argument { get; private set; }

        public ReturnStatement(Location location = null) : this(null, location) { }
        public ReturnStatement(Expression argument, Location location = null) : 
            base(SyntaxNodeType.ReturnStatement, location)
        {
            Argument = argument;
        }
    }

    public class SequenceExpression : Expression
    {
        public IList<Expression> Expressions { get; private set; }

        public SequenceExpression(IList<Expression> expressions, Location location = null) : 
            base(SyntaxNodeType.SequenceExpression, location)
        {
            if (expressions == null) throw new ArgumentNullException("expressions");
            Expressions = expressions;
        }
    }

    public abstract class Statement : SyntaxNode
    {
        protected Statement(SyntaxNodeType nodeType, Location location = null) :
            base(nodeType, location) {}
    }

    public class SwitchCase : SyntaxNode
    {
        public Expression Test { get; private set; }
        public IEnumerable<Statement> Consequent { get; private set; }

        public SwitchCase(IEnumerable<Statement> consequent) : 
            this(null, consequent) {}

        public SwitchCase(Expression test, IEnumerable<Statement> consequent, Location location = null) : 
            base(SyntaxNodeType.SwitchCase, location)
        {
            if (consequent == null) throw new ArgumentNullException("consequent");
            Test = test;
            Consequent = consequent;
        }
    }

    public class SwitchStatement : Statement
    {
        public Expression Discriminant { get; private set; }
        public IEnumerable<SwitchCase> Cases { get; private set; }

        public SwitchStatement(Expression discriminant, IEnumerable<SwitchCase> cases, Location location = null) : 
            base(SyntaxNodeType.SwitchStatement, location)
        {
            if (discriminant == null) throw new ArgumentNullException("discriminant");
            if (cases == null) throw new ArgumentNullException("cases");
            Cases = cases;
            Discriminant = discriminant;
        }
    }

    public abstract class SyntaxNode
    {
        static readonly Location NoLocation = new Location(Position.Nil, Position.Nil);

        public SyntaxNodeType NodeType { get; private set; }
        public Location Location { get; private set; }

        Location InternalLocation     { get { return Location ?? NoLocation; } }
        public Range    IndexRange    { get { return InternalLocation.Range; } }
        public Position LocationStart { get { return InternalLocation.Start; } }
        public Position LocationEnd   { get { return InternalLocation.End;   } }

        protected SyntaxNode(SyntaxNodeType nodeType) : this(nodeType, null) {}

        protected SyntaxNode(SyntaxNodeType nodeType, Location location)
        {
            NodeType = nodeType;
            Location = location;
        }

        public string Source
        {
            get
            {
                Range range;
                var source = InternalLocation.Source;
                return source != null && !((range = IndexRange).IsEmpty)
                     ? source.Substring(range.Start, range.End - range.Start)
                     : null;
            }
        }

        [DebuggerStepThrough]
        public T As<T>() where T : SyntaxNode
        {
            return (T)this;
        }
    }

    public enum SyntaxNodeType
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

    public class ThisExpression : Expression
    {
        public ThisExpression(Location location = null) : 
            base(SyntaxNodeType.ThisExpression, location) { }
    }

    public class ThrowStatement : Statement
    {
        public Expression Argument { get; private set; }

        public ThrowStatement(Expression argument, Location location = null) : 
            base(SyntaxNodeType.ThrowStatement, location)
        {
            if (argument == null) throw new ArgumentNullException("argument");
            Argument = argument;
        }
    }

    public class TryStatement : Statement
    {
        public Statement Block { get; private set; }
        public IEnumerable<Statement> GuardedHandlers { get; private set; }
        public IEnumerable<CatchClause> Handlers { get; private set; }
        public Statement Finalizer { get; private set; }

        public TryStatement(Statement block, 
            IEnumerable<Statement> guardedHandlers, 
            IEnumerable<CatchClause> handlers) : 
            this(block, guardedHandlers, handlers, null) {}

        public TryStatement(Statement block, 
            IEnumerable<Statement> guardedHandlers, 
            IEnumerable<CatchClause> handlers, 
            Statement finalizer, Location location = null) : 
            base(SyntaxNodeType.TryStatement, location)
        {
            if (block == null) throw new ArgumentNullException("block");
            if (guardedHandlers == null) throw new ArgumentNullException("guardedHandlers");
            if (handlers == null) throw new ArgumentNullException("handlers");
            Block = block;
            GuardedHandlers = guardedHandlers;
            Handlers = handlers;
            Finalizer = finalizer;
        }
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
        public UnaryOperator Operator { get; private set; }
        public Expression Argument { get; private set; }
        public bool Prefix { get; private set; }

        public UnaryExpression(UnaryOperator op, Expression argument, bool prefix, Location location = null) :
            base(SyntaxNodeType.UnaryExpression, location)
        {
            if (!FastEnumValidator<UnaryOperator>.IsDefined((int) op) 
                || op == UnaryOperator.Increment
                || op == UnaryOperator.Decrement) 
                throw new ArgumentOutOfRangeException("op");
            if (argument == null) throw new ArgumentNullException("argument");
            Operator = op;
            Argument = argument;
            Prefix = prefix;
        }
    }

    public class UpdateExpression : Expression
    {
        public UnaryOperator Operator { get; private set; }
        public Expression Argument { get; private set; }
        public bool Prefix { get; private set; }

        public UpdateExpression(UnaryOperator op, Expression argument, bool prefix, Location location = null) :
            base(SyntaxNodeType.UpdateExpression, location)
        {
            if (   op != UnaryOperator.Increment 
                && op != UnaryOperator.Decrement) 
                throw new ArgumentOutOfRangeException("op");            
            if (argument == null) throw new ArgumentNullException("argument");
            Operator = op;
            Argument = argument;
            Prefix = prefix;
        }
    }

    public class VariableDeclaration : Statement
    {
        public IEnumerable<VariableDeclarator> Declarations { get; private set; }
        public string Kind { get; private set; }
        
        public VariableDeclaration(IEnumerable<VariableDeclarator> declarations, string kind, Location location = null) : 
            base(SyntaxNodeType.VariableDeclaration, location)
        {
            if (declarations == null) throw new ArgumentNullException("declarations");
            Declarations = declarations;
            Kind = kind;
        }
    }

    public class VariableDeclarator : Expression
    {
        public Identifier Id { get; private set; }
        public Expression Init { get; private set; }

        public VariableDeclarator(Identifier id) : this(id, null) {}
        public VariableDeclarator(Identifier id, Expression init, Location location = null) : 
            base(SyntaxNodeType.VariableDeclarator, location)
        {
            if (id == null) throw new ArgumentNullException("id");
            Id = id;
            Init = init;
        }
    }

    public class WhileStatement : Statement
    {
        public Expression Test { get; private set; }
        public Statement Body { get; private set; }
        
        public WhileStatement(Expression test, Statement body, Location location = null) : 
            base(SyntaxNodeType.WhileStatement, location)
        {
            if (test == null) throw new ArgumentNullException("test");
            if (body == null) throw new ArgumentNullException("body");
            Test = test;
            Body = body;
        }
    }

    public class WithStatement : Statement
    {
        public Expression Object { get; private set; }
        public Statement Body { get; private set; }

        public WithStatement(Expression obj, Statement body, Location location = null) : 
            base(SyntaxNodeType.WithStatement, location)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (body == null) throw new ArgumentNullException("body");
            Object = obj;
            Body = body;
        }
    }
}