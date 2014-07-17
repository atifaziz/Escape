using Escape.Ast;

namespace Jint
{
    public class WithStatement : Statement
    {
        public Expression Object;
        public Statement Body;
    }
}