using System.Collections.Generic;

namespace Escape.Ast
{
    public class ObjectExpression : Expression
    {
        public IEnumerable<Property> Properties;
    }
}