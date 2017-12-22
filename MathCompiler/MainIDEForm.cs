using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Lexer;

namespace MathCompiler
{
    public partial class MainIDEForm : Form
    {
        /// <summary>
        /// Create a new ideForm and initialize it
        /// </summary>
        public MainIDEForm() => InitializeComponent();

        private void NewFile(object sender, EventArgs e) => code.Text = "";

        /// <summary>
        /// Open a file and read the code into the buffer
        /// </summary>
        /// <param name="sender">The Form that sent the request</param>
        /// <param name="e">The event data</param>
        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                Filter = "TLANG Code (*.t)|*.t|All Files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                code.Text = System.IO.File.ReadAllText(FileName);
            }
        }

        /// <summary>
        /// Read the text from the buffer into the selected file
        /// </summary>
        /// <param name="sender">The form that sent the request</param>
        /// <param name="e">The event data</param>
        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                Filter = "TLANG Code (*.t)|*.t|All Files (*.*)|*.*"
            };
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                System.IO.File.WriteAllText(FileName, code.Text);
            }
        }


        private void CompileButton_Click(object sender, EventArgs e)
        {
            List<Lexer.Interfaces.IToken<string, string>> tokens = new List<Lexer.Interfaces.IToken<string, string>>();
            Lexer.Interfaces.IStringLexer<Lexer.Interfaces.IToken<string, string>> lexer = new Lexer.Implementation.RegexLexer(code.Text);
            tokens = lexer.GetAllTokens();

            MathCompiler mathCompiler = new MathCompiler(tokens.ToArray());
            toolStripStatusLabel.Text = mathCompiler.MainMethod();
        }
    }
}
