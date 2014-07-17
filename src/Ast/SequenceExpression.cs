using System.Collections.Generic;

namespace Escape.Ast
{
    public class SequenceExpression : Expression
    {
        public IList<Expression> Expressions;
    }
}