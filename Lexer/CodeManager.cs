using System.Text.RegularExpressions;

namespace Lexer
{
    class CodeTokenizer
    {
        string Code;

        public CodeTokenizer(string code)
        {
            this.Code = code;
        }

        public string self()
        {
            return Code;
        }

        public string Get(string regex)
        {
            Match match = new Regex("^"+regex).Match(Code);
            if(match.Value != string.Empty)
            {
                Code = Code.Remove(0, match.Value.Length);
                return match.Value;
            }
            else
            {
                return null;
            }
        }

        public void PutString(string sequence)
        {
            Code = sequence + Code;
        }
       

    }
}
