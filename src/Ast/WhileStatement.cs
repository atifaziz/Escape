namespace Escape.Ast
{
    public class WhileStatement : Statement
    {
        public Expression Test;
        public Statement Body;
    }
}