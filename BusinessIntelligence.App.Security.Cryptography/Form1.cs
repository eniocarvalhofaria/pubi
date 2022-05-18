using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BusinessIntelligence.App.Security.Cryptography
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtEncryptedValue.Text = BusinessIntelligence.Util.Cryptography.Encrypt(txtOriginalValue.Text, txtKey.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtOriginalValue.Text = BusinessIntelligence.Util.Cryptography.Decrypt(txtEncryptedValue.Text, txtKey.Text);
        }
    }
}
