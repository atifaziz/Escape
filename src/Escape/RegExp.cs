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
    using System;
    using System.Text.RegularExpressions;

    [Flags]
    public enum RegExpFlags
    {
        None = 0,
        Global = 1,
        IgnoreCase = 2,
        Multiline = 4
    }

    public sealed class RegExp : IEquatable<RegExp>
    {
        public string Pattern { get; private set; }
        public RegExpFlags Flags { get; private set; }

        static readonly string[] FlagsStrings = { string.Empty, "g", "i", "gi", "m", "gm", "im", "gim" };

        public RegExp(string pattern, RegExpFlags flags)
        {
            if (pattern == null) throw new ArgumentNullException("pattern");
            if (((uint) flags & ((uint) (RegExpFlags.Global | RegExpFlags.IgnoreCase | RegExpFlags.Multiline) ^ uint.MaxValue)) != 0)
                throw new ArgumentOutOfRangeException("flags");
            // ReSharper disable once ObjectCreationAsStatement
            new Regex(pattern, RegexOptions.ECMAScript); // validate
            Pattern = pattern;
            Flags = flags;
        }

        public RegExp(string pattern, string flags) :
            this(pattern, ParseFlags(flags)) {}

        static RegExpFlags ParseFlags(string input)
        {
            var flags = RegExpFlags.None;
            if (input.IndexOf('g') >= 0) flags |= RegExpFlags.Global;
            if (input.IndexOf('i') >= 0) flags |= RegExpFlags.IgnoreCase;
            if (input.IndexOf('m') >= 0) flags |= RegExpFlags.Multiline;
            return flags;
        }

        public bool Equals(RegExp other)
        {
            return !ReferenceEquals(null, other) &&
                   (ReferenceEquals(this, other) || string.Equals(Pattern, other.Pattern) && Flags == other.Flags);
        }

        public override bool Equals(object obj) { return Equals(obj as RegExp); }
        public override int GetHashCode() { return unchecked((Pattern.GetHashCode() * 397) ^ (int) Flags); }
        public static bool operator ==(RegExp left, RegExp right) { return Equals(left, right);  }
        public static bool operator !=(RegExp left, RegExp right) { return !Equals(left, right); }

        public override string ToString()
        {
            // http://www.ecma-international.org/ecma-262/5.1/#sec-15.10.6.4

            // Return the String value formed by concatenating the Strings 
            // "/", the String value of the source property of this RegExp 
            // object, and "/"; plus "g" if the global property is true, 
            // "i" if the ignoreCase property is true, and "m" if the 
            // multiline property is true.
            
            return "/" + Pattern + "/" + FlagsStrings[(int)Flags];
        }
    }
}