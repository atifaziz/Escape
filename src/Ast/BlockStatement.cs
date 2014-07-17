using System.Collections.Generic;

namespace Escape.Ast
{
    public class BlockStatement : Statement
    {
        public IEnumerable<Statement> Body;
    }
}