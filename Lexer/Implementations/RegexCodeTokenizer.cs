using System.Text.RegularExpressions;

namespace Lexer.Implementation
{
    class RegexCodeTokenizer
    {
        private string Code;

        public RegexCodeTokenizer(string code)
        {
            this.Code = code;
        }

        public string Get(string regex)
        {
            Match match = new Regex("^"+regex).Match(Code);
            if(match.Value != string.Empty)
            {
                Code = Code.Remove(0, match.Value.Length);
                return match.Value;
            }
            return null;
        }    
    }
}
