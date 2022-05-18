using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
namespace BusinessIntelligence.RunAs
{
    public partial class FormAuthenticator : Form
    {
        public FormAuthenticator()
        {
            InitializeComponent();
        }
   
        string _Domain;

        public string Domain
        {
            get { return _Domain; }
            set { _Domain = value; }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Cancelled = true;
            this.Close();
        }
        private string _UserId;

        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        private string _Pwd;

        public string Pwd
        {
            get { return _Pwd; }
            set { _Pwd = value; }
        }
        private void btOk_Click(object sender, EventArgs e)
        {



            Pwd = txtPassword.Text;
            UserId = txtUserId.Text;
            this.Close();
        }
        private bool _Cancelled;

        public bool Cancelled
        {
            get { return _Cancelled; }
            set { _Cancelled = value; }
        }
        private void OnAuthenticator_Load(object sender, EventArgs e)
        {
            txtUserId.Text = Environment.UserName;
            //          txtPassword.Text = "Zy@UsYME.h";
        }
    }
}
