using System.Collections.Generic;

namespace Escape.Ast
{
    public class CallExpression : Expression
    {
        public Expression Callee;
        public IEnumerable<Expression> Arguments;
    }
}