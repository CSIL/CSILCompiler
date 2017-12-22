using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer.Interfaces
{
    /// <summary>
    /// An interface to allow different types of tokens
    /// </summary>
    public interface IToken<T,T2>
    {
        /// <summary>
        /// Get the value of the token
        /// </summary>
        /// <returns>The value</returns>
        T2 GetValue();

        /// <summary>
        /// Get the type of the token
        /// </summary>
        /// <returns>A string representing the type</returns>
        T GetTokenType();
    }
}
