namespace Escape.Ast
{
    public class VariableDeclarator : Expression
    {
        public Identifier Id;
        public Expression Init;
    }
}