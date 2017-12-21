using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compiler
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

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void fileMenu_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveAsToolStripMenuItem_Click(sender, e);
        }

        private void MainIDEForm_Load(object sender, EventArgs e)
        {

        }


        private List<Lexer.Token> tokens = new List<Lexer.Token>();
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Lexer.Lexer lexer = new Lexer.Lexer(code.Text);
            tokens = lexer.getAllTokens();

            Interpreter.MathCompiler mathCompiler = new Interpreter.MathCompiler(tokens.ToArray());
            toolStripStatusLabel.Text = mathCompiler.MainMethod();
        }
    }
}
