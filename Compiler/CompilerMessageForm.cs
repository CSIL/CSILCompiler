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
    public partial class CompilerMessageForm : Form
    {
        public CompilerMessageForm()
        {
            InitializeComponent();
            textBox1.Enabled = false;
        }

        private void CompilerMessageForm_Load(object sender, EventArgs e)
        {

        }

        public void AddTextLine(string line)
        {
            this.textBox1.AppendText(line + "\r\n");
        }
    }
}
