using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BusinessIntelligence.Controls
{
    public partial class LogWindow : Form
    {
        public LogWindow()
        {
            InitializeComponent();
        }

        private void LogWindow_Load(object sender, EventArgs e)
        {

        }
        public void WriteMessage(string message)
        {
            richTextBox1.Text += "\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + message;
        }

        public string Content
        {
            get { return richTextBox1.Text; }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
          
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }
    }
}
