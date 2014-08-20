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

    static class Extensions
    {
        public static string JsText(this BinaryOperator op)
        {
            switch (op)
            {
                case BinaryOperator.Plus: return "+";
                case BinaryOperator.Minus: return "-";
                case BinaryOperator.Times: return "*";
                case BinaryOperator.Divide: return "/";
                case BinaryOperator.Modulo: return "%";
                case BinaryOperator.Equal: return "==";
                case BinaryOperator.NotEqual: return "!=";
                case BinaryOperator.Greater: return ">";
                case BinaryOperator.GreaterOrEqual: return ">=";
                case BinaryOperator.Less: return "<";
                case BinaryOperator.LessOrEqual: return "<=";
                case BinaryOperator.StrictlyEqual: return "===";
                case BinaryOperator.StricltyNotEqual: return "!==";
                case BinaryOperator.BitwiseAnd: return "&";
                case BinaryOperator.BitwiseOr: return "|";
                case BinaryOperator.BitwiseXOr: return "^";
                case BinaryOperator.LeftShift: return "<<";
                case BinaryOperator.RightShift: return ">>";
                case BinaryOperator.UnsignedRightShift: return ">>>";
                case BinaryOperator.InstanceOf: return "instanceof";
                case BinaryOperator.In: return "in";
                default: throw new NotSupportedException();
            }
        }

        public static string JsText(this UnaryOperator op)
        {
            switch (op)
            {
                case UnaryOperator.Plus: return "+";
                case UnaryOperator.Minus: return "-";
                case UnaryOperator.BitwiseNot: return "~";
                case UnaryOperator.LogicalNot: return "!";
                case UnaryOperator.Delete: return "delete";
                case UnaryOperator.Void: return "void";
                case UnaryOperator.TypeOf: return "typeof";
                default: throw new NotSupportedException();
            }
        }

        public static string JsText(this UpdateOperator op)
        {
            switch (op)
            {
                case UpdateOperator.Increment: return "++";
                case UpdateOperator.Decrement: return "--";
                default: throw new NotSupportedException();
            }
        }

        public static string JsText(this AssignmentOperator op)
        {
            switch (op)
            {
                case AssignmentOperator.Assign: return "=";
                case AssignmentOperator.PlusAssign: return "+=";
                case AssignmentOperator.MinusAssign: return "-=";
                case AssignmentOperator.TimesAssign: return "*=";
                case AssignmentOperator.DivideAssign: return "/=";
                case AssignmentOperator.ModuloAssign: return "%=";
                case AssignmentOperator.BitwiseAndAssign: return "&=";
                case AssignmentOperator.BitwiseOrAssign: return "|=";
                case AssignmentOperator.BitwiseXOrAssign: return "^=";
                case AssignmentOperator.LeftShiftAssign: return "<<=";
                case AssignmentOperator.RightShiftAssign: return ">>=";
                case AssignmentOperator.UnsignedRightShiftAssign: return ">>>=";
                default: throw new NotSupportedException();
            }
        }

        public static string JsText(this LogicalOperator op)
        {
            switch (op)
            {
                case LogicalOperator.LogicalAnd: return "&&";
                case LogicalOperator.LogicalOr: return "||";
                default: throw new NotSupportedException();
            }
        }
    }
}