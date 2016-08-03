using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace scp_frontend
{
    public partial class Form1 : Form
    {
        locale_explorer l_explorer;
        remote_explorer r_explorer;
        SshClient ssh_client;
        SshCommand ssh_cmd;
        explorer_exchange exchanger;

        string host = "";
        string user = "";
        string pass = "";
        public Form1()
        {
            InitializeComponent();
        }

        ~Form1()
        {
            ssh_client.Disconnect();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            l_explorer = new locale_explorer(@"C:\Users\" + System.Environment.UserName + @"\Desktop\", listBox1, listBox2);
            listBox1.MouseDoubleClick += l_explorer.locale_dir_double_click;
            listBox2.MouseDoubleClick += l_explorer.virtual_dir_double_click;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            login login_form = new login();
            login_form.ShowDialog();
            this.Text = "SSH Frontend (connecting)";
            user = login_form.GetUser();
            pass = login_form.GetPass();
            try
            {
                ssh_client = new SshClient(host, user, pass);
                ssh_client.Connect();
            }
            catch (Exception exception) 
            {
                if (exception is SshAuthenticationException || exception is ArgumentException)
                {
                    alert_form alert = new alert_form("Wrong username or password!");
                    alert.ShowDialog();
                }
                return;
            }
            ssh_cmd = ssh_client.CreateCommand("");
            this.Text = "SSH Frontend (connected)";
            r_explorer = new remote_explorer(host, listBox4, listBox3, ssh_cmd);
            listBox3.MouseDoubleClick += r_explorer.virtual_dir_double_click;
            listBox4.MouseDoubleClick += r_explorer.remote_dir_double_click;
            exchanger = new explorer_exchange(host, user, pass, l_explorer, r_explorer);
            button1.Click += exchanger.upload_button_pressed;
            button2.Click += exchanger.download_button_pressed;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0) { host = textBox1.Text; button3.Enabled = true; }
            else { host = ""; button3.Enabled = false; }
        }

        //Orphans
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
