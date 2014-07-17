namespace Escape.Ast
{
    public class IfStatement : Statement
    {
        public Expression Test;
        public Statement Consequent;
        public Statement Alternate;
    }
}