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
    public partial class alert_form : Form
    {
        string msg;
        public alert_form(string msg)
        {
            InitializeComponent();
            this.msg = msg;
        }

        private void alert_form_Load(object sender, EventArgs e)
        {
            label1.Text = msg;
            this.Width = (int)label1.Font.SizeInPoints*msg.Length;
        }
    }
}
