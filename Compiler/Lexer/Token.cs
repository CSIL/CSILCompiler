using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lexer
{
    
    /// <summary>
    /// A parser token
    /// </summary>
    public class Token
    {
        string Type;
        string Contents;

        /// <summary>
        /// Create a new token
        /// </summary>
        /// <param name="type">The token type of the token</param>
        /// <param name="token">the text of the token</param>
        public Token(string type, string token)
        {
            Type = type;
            Contents = token;
#if false
            Console.WriteLine(this);
#endif
        }


        /// <summary>
        /// Get the type of this token
        /// </summary>
        /// <returns>A string representing the type</returns>
        public string GetTokenType()
        {
            return this.Type;
        }


        /// <summary>
        /// An override of the tostring method for debugging purposes
        /// </summary>
        /// <returns>A string representing the token</returns>
        public override string ToString()
        {
            return Type.ToString() + " " + Contents;
        }

        /// <summary>
        /// Get the value of the token
        /// </summary>
        /// <returns>a string representing the value</returns>
        public string GetValue()
        {
            return this.Contents;
        }
    }
}
