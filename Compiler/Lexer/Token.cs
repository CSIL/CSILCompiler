using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lexer
{
    

    public class Token
    {
        string Type;
        string Contents;

        public Token(string type, string token)
        {
            Type = type;
            Contents = token;
#if false
            Console.WriteLine(this);
#endif
        }

        public string GetTokenType()
        {
            return this.Type;
        }

        public override string ToString()
        {
            return Type.ToString() + " " + Contents;
        }

        public string GetValue()
        {
            return this.Contents;
        }
    }
}
