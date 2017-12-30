using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lexer.Implementation;

namespace RoseBud
{
    public class Program
    {
        static void Main(string[] args)
        {
            RegexLexer lexer = new RegexLexer(Console.ReadLine());
            foreach (Token t in lexer.GetAllTokens())
            {
                Console.WriteLine(t);
            }
            Console.ReadKey();
        }
    }
}
