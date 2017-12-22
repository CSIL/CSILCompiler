namespace Lexer.Implementation
{

    /// <summary>
    /// A parser token
    /// </summary>
    public class Token:Interfaces.IToken<string , string>
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
            System.Console.WriteLine(this);
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
            return Type + " " + Contents;
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
