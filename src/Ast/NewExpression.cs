using System.Collections.Generic;

namespace Escape.Ast
{
    public class NewExpression : Expression
    {
        public Expression Callee;
        public IEnumerable<Expression> Arguments;
    }
}