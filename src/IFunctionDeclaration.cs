using System.Collections.Generic;
using Escape.Ast;

namespace Escape
{
    using Ast;

    public interface IFunctionDeclaration : IFunctionScope
    {
        Identifier Id { get; }
        IEnumerable<Identifier> Parameters { get; }
        Statement Body { get; }
        bool Strict { get; }
    }
}