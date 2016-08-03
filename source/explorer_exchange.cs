using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Renci.SshNet;
using System.IO;

namespace scp_frontend
{
    class explorer_exchange
    {
        string host, user, pass;
        ScpClient scp_client;
        locale_explorer l_explorer;
        remote_explorer r_explorer;

        public explorer_exchange(string host, string user, string pass, locale_explorer l_explorer, remote_explorer r_explorer)
        {
            this.host = host;
            this.user = user;
            this.l_explorer = l_explorer;
            this.r_explorer = r_explorer;
            this.scp_client = new ScpClient(host, user, pass);
            this.scp_client.Connect();
        }

        ~explorer_exchange()
        {
            scp_client.Disconnect();
        }

        public void upload_button_pressed(object sender, EventArgs e)
        {
            foreach (directory_entry entry in l_explorer.get_virtual_directory())
            {
                FileInfo info = new FileInfo(entry.GetPath());
                scp_client.Upload(info, r_explorer.get_remote_directory().GetPath() + entry.GetName());
            }
            l_explorer.get_virtual_directory().Clear();
            l_explorer.show();
            r_explorer.show();
        }

        public void download_button_pressed(object sender, EventArgs e)
        {
            foreach (directory_entry entry in r_explorer.get_virtual_directory())
            {
                FileInfo info = new FileInfo(l_explorer.get_locale_directory().GetPath() + entry.GetName());
                scp_client.Download(entry.GetPath(), info);
            }
            r_explorer.get_virtual_directory().Clear();
            l_explorer.show();
            r_explorer.show();
        }
    }
}
