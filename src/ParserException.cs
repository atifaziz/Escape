using System;

namespace Escape
{
    public class ParserException : Exception
    {
        public int Column;
        public string Description;
        public int Index;
        public int LineNumber;
        public string Source;

        public ParserException(string message) : base(message)
        {
        }
    }
}