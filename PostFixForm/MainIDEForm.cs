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
        public MainIDEForm()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            code.Text = "";
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "TLANG Code (*.t)|*.t|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                code.Text = System.IO.File.ReadAllText(FileName);
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "TLANG Code (*.t)|*.t|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                System.IO.File.WriteAllText(FileName, code.Text);
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveAsToolStripMenuItem_Click(sender, e);
        }

        private void MainIDEForm_Load(object sender, EventArgs e) { }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            List<Token> tokens = new List<Token>();
            Lexer.Lexer lexer = new Lexer.Lexer(code.Text);
            tokens = lexer.GetAllTokens();

            PostFixForm.PostFixForm cComp = new PostFixForm.PostFixForm(tokens.ToArray());
        }
    }
}
