using System.Collections.Generic;

namespace Escape.Ast
{
    public class SwitchCase : SyntaxNode
    {
        public Expression Test;
        public IEnumerable<Statement> Consequent;
    }
}