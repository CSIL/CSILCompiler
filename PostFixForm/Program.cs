using System;
using System.Windows.Forms;

namespace MathCompiler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string Expression = Microsoft.VisualBasic.Interaction.InputBox("Input an expression: ", "Math Input", "1+1");
            Lexer.Lexer lexer = new Lexer.Lexer(Expression);
            PostFixForm.PostFixForm form = new PostFixForm.PostFixForm(lexer.GetAllTokens().ToArray());
        }
    }
}
