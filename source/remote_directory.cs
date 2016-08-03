using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace scp_frontend
{
    class remote_directory : directory
    {
        SshCommand command;
        string host;
        public remote_directory(string host, SshCommand command)
        {
            this.host = host;
            this.path = command.Execute("pwd");
            this.path = this.path.Replace('\n', '/');
            this.command = command;
            read_directory();
        }

        public void change_directory(directory_entry entry)
        {
            if (entry.IsDirectory() && entries.Contains(entry))
            {
                this.path = entry.GetPath();
                read_directory();
            }
        }

        public void read_directory()
        {
            string result = command.Execute("cd " + path + " && ls -d -1 ../ */");
            string[] entries = result.Split("\n".ToCharArray());
            this.entries = new List<directory_entry>();
            foreach (string entry in entries)
            {
                directory_entry dir_entry = new directory_entry();
                dir_entry.create_from_remote(this.path, entry, true);
                this.entries.Add(dir_entry);
            }

            result = command.Execute("cd " + path + " && ls -p -1 | grep -v /");
            entries = result.Split("\n".ToCharArray());
            foreach (string entry in entries)
            {
                directory_entry dir_entry = new directory_entry();
                dir_entry.create_from_remote(this.path, entry, false);
                this.entries.Add(dir_entry);
            }
        }
    }
}
