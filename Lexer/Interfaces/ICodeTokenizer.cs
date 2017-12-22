using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer.Interfaces
{
    /// <summary>
    /// Tokenises a string according to certain rules
    /// </summary>
    public interface ICodeTokenizer
    {
        /// <summary>
        /// Get the next token string
        /// </summary>
        /// <param name="searchString">A string to check for</param>
        /// <returns>The string that was found</returns>
        string Get(string searchString);

        /// <summary>
        /// Add a string into the string to be tokenised
        /// 
        /// This is usefull for c-style includes 
        /// </summary>
        /// <param name="toPut">The string to add</param>
        void PutString(string toPut);
    }
}
