#region Copyright (c) 2004 Atif Aziz. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

namespace Esparse
{
    #region Imports

    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    #endregion

    /// <summary>
    /// Represents a writer that provides a fast, non-cached, forward-only 
    /// way of generating streams or files containing JSON Text according
    /// to the grammar rules laid out in 
    /// <a href="http://www.ietf.org/rfc/rfc4627.txt">RFC 4627</a>.
    /// </summary>

    // This is a slightly modified version of JsonTextWriter from ELMAH
    // 1.x with pretty printing support added.

    sealed class JsonTextWriter
    {
        readonly TextWriter _writer;
        readonly int[] _counters;
        readonly char[] _terminators;
        readonly char[] _indents;
        readonly int _indentUnit;
        string _memberName;

        public JsonTextWriter(TextWriter writer)
        {
            Debug.Assert(writer != null);
            _writer = writer;
            const int levels = 200 + /* root */ 1;
            _counters = new int[levels];
            _terminators = new char[levels];
            const string indent = "    ";
            _indents = string.Join(null, Enumerable.Repeat(indent, levels)).ToCharArray();
            _indentUnit = indent.Length;
        }

        public int Depth { get; set; }

        int ItemCount
        {
            get { return _counters[Depth]; }
            set { _counters[Depth] = value; }
        }

        char Terminator
        {
            get { return _terminators[Depth]; }
            set { _terminators[Depth] = value; }
        }

        public JsonTextWriter Object() { return StartStructured("{", "}"); }
        public JsonTextWriter EndObject() { return Pop(); }
        public JsonTextWriter Array() { return StartStructured("[", "]"); }
        public JsonTextWriter EndArray() { return Pop(); }
        public JsonTextWriter Pop() { return EndStructured(); }

        public JsonTextWriter Member(string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (_memberName != null) throw new InvalidOperationException("Missing member value.");
            _memberName = name;
            return this;
        }

        JsonTextWriter Write(string text) { return WriteImpl(text, /* raw */ false); }
        JsonTextWriter WriteEnquoted(string text) { return WriteImpl(text, /* raw */ true); }

        JsonTextWriter WriteImpl(string text, bool raw)
        {
            Debug.Assert(raw || !string.IsNullOrEmpty(text));

            if (Depth == 0 && (text.Length > 1 || (text[0] != '{' && text[0] != '[')))
                throw new InvalidOperationException();

            var writer = _writer;

            if (ItemCount > 0)
                writer.Write(',');

            if (Depth > 0)
            {
                writer.WriteLine();
                writer.Write(_indents, 0, _indentUnit * Depth);
            }

            var name = _memberName;
            _memberName = null;

            if (name != null)
            {
                Enquote(name, writer);
                writer.Write(": ");
            }

            if (raw) 
                Enquote(text, writer); 
            else 
                writer.Write(text);
            
            ItemCount = ItemCount + 1;

            return this;
        }

        public JsonTextWriter Number(int value) { return Write(value.ToString(CultureInfo.InvariantCulture)); }
        public JsonTextWriter Number(double value) { return Write(value.ToString(CultureInfo.InvariantCulture)); }
        public JsonTextWriter String(string str) { return str == null ? Null() : WriteEnquoted(str); }
        public JsonTextWriter Null() { return Write("null"); }
        public JsonTextWriter Boolean(bool value) { return Write(value ? "true" : "false"); }

        JsonTextWriter StartStructured(string start, string end)
        {
            if (Depth + 1 == _counters.Length)
                throw new Exception();

            Write(start);
            Depth++;
            Terminator = end[0];
            return this;
        }

        JsonTextWriter EndStructured()
        {
            var depth = Depth - 1;
            if (depth < 0)
                throw new Exception();

            if (ItemCount > 0)
            {
                _writer.WriteLine();
                _writer.Write(_indents, 0, _indentUnit * depth);
            }
            
            _writer.Write(Terminator);
            ItemCount = 0;
            Depth = depth;
            return this;
        }

        static void Enquote(string s, TextWriter writer)
        {
            Debug.Assert(writer != null);

            var length = (s ?? string.Empty).Length;

            writer.Write('"');

            for (var index = 0; index < length; index++)
            {
                Debug.Assert(s != null);
                var ch = s[index];

                switch (ch)
                {
                    case '\\':
                    case '"':
                    {
                        writer.Write('\\');
                        writer.Write(ch);
                        break;
                    }

                    case '\b': writer.Write("\\b"); break;
                    case '\t': writer.Write("\\t"); break;
                    case '\n': writer.Write("\\n"); break;
                    case '\f': writer.Write("\\f"); break;
                    case '\r': writer.Write("\\r"); break;

                    default:
                    {
                        if (ch < ' ')
                        {
                            writer.Write("\\u");
                            writer.Write(((int)ch).ToString("x4", CultureInfo.InvariantCulture));
                        }
                        else
                        {
                            writer.Write(ch);
                        }

                        break;
                    }
                }
            }

            writer.Write('"');
        }
    }
}