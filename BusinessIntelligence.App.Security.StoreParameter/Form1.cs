using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessIntelligence.Util;
namespace BusinessIntelligence.App.Security.StoreParameter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ckEncrypt_CheckedChanged(object sender, EventArgs e)
        {
            txtKey.Enabled = ckEncrypt.Checked;
            txtOriginalValue.UseSystemPasswordChar = ckEncrypt.Checked;
        }

        private void btStore_Click(object sender, EventArgs e)
        {
  
                if (!ckEncrypt.Checked || Cryptography.CheckKey(txtKey.Text))
                {
                    var p = new Parameter(txtName.Text);
                    p.Value = txtOriginalValue.Text;
                    p.IsEncrypted = ckEncrypt.Checked;
                    p.Store();
                    MessageBox.Show("Parametro armazenado com sucesso.");
                }
                else
                {
                    MessageBox.Show("Chave de criptografia incorreta.", "Checagem de chave de criptografia");
                }
            }
        }
    }

