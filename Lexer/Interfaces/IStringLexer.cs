using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer.Interfaces
{
    /// <summary>
    /// A lexer that will work on code
    /// </summary>
    public interface IStringLexer<T> where T:Interfaces.IToken<string, string>
    {
        /// <summary>
        /// Get the tokens from the input list
        /// </summary>
        /// <returns>A list of tokens representing the input</returns>
        List<T> GetAllTokens();
    }
}
