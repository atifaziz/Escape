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
    using System.Linq;
    using System.Reflection;

    static class FastEnumValidator<T> where T : struct
#if !PORTABLE
        , IConvertible
#endif
    {
        // ReSharper disable StaticFieldInGenericType
        static readonly int Min;
        static readonly int Max;
        // ReSharper restore StaticFieldInGenericType

        public static bool IsDefined(int value) { return value >= Min && value <= Max; }        

        static FastEnumValidator()
        {
            if (typeof(T).GetTypeInfo().IsDefined(typeof(FlagsAttribute)))
                throw new NotSupportedException("Bitmap enumeration are not supported.");
            var values = Enum.GetValues(typeof(T))
#if PORTABLE
                             .Cast<IConvertible>()
#else
                             .Cast<T>()
#endif
                            .Select(v => v.ToInt32(null)).ToArray();
            Min = values.Min();
            Max = values.Max();
        }
    }
}