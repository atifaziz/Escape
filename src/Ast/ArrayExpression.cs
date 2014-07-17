using System.Collections.Generic;

namespace Escape.Ast
{
    public class ArrayExpression : Expression
    {
        public IEnumerable<Expression> Elements;
    }
}