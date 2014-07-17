using System.Collections.Generic;

namespace Escape.Ast
{
    public class VariableDeclaration : Statement
    {
        public IEnumerable<VariableDeclarator> Declarations;
        public string Kind;
    }
}