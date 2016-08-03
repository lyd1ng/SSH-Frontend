using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace scp_frontend
{
    public partial class login : Form
    {
        string user, pass;
        public login()
        {
            user = "";
            pass = "";
            InitializeComponent();
        }

        public string GetUser() { return user; }
        public string GetPass() { return pass; }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            user = textBox1.Text;
            pass = textBox2.Text;
            this.Close();
        }
    }
}
