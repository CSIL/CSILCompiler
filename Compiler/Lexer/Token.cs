using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lexer
{
    

    class Token
    {
        string Type;
        string Contents;

        public Token(string type, string token)
        {
            Type = type;
            Contents = token;
            Console.WriteLine(this);
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
