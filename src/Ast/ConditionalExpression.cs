namespace Escape.Ast
{
    public class ConditionalExpression : Expression
    {
        public Expression Test;
        public Expression Consequent;
        public Expression Alternate;
    }
}