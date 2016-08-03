using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Renci.SshNet;

namespace scp_frontend
{
    class remote_explorer
    {
        remote_directory remote_dir;
        List<directory_entry> virtual_dir;
        ListBox remote_dir_box, virtual_dir_box;

        public remote_explorer(string host, ListBox remote_dir_box, ListBox virtual_dir_box, SshCommand cmd)
        {
            remote_dir = new remote_directory(host, cmd);
            virtual_dir = new List<directory_entry>();
            this.remote_dir_box = remote_dir_box;
            this.virtual_dir_box = virtual_dir_box;
            update_remote_dir_box();
            update_virtual_dir_box();
        }

        public void show()
        {
            remote_dir.read_directory();
            update_remote_dir_box();
            update_virtual_dir_box();
        }

        public void remote_dir_double_click(object sender, MouseEventArgs e)
        {
            int index = remote_dir_box.IndexFromPoint(e.X, e.Y);
            directory_entry selection = remote_dir.GetEntryAt(index);
            if (selection.IsDirectory())
            {
                remote_dir.change_directory(selection);
                update_remote_dir_box();
            }
            else
            {
                virtual_dir.Add(selection);
                update_virtual_dir_box();
            }
        }

        public void virtual_dir_double_click(object sender, MouseEventArgs e)
        {
            int index = virtual_dir_box.IndexFromPoint(e.X, e.Y);
            directory_entry selection = virtual_dir[index];
            virtual_dir.Remove(selection);
            update_virtual_dir_box();
        }

        public List<directory_entry> get_virtual_directory() { return virtual_dir; }
        public remote_directory get_remote_directory() { return remote_dir; }

        private void update_remote_dir_box()
        {
            remote_dir_box.Items.Clear();
            remote_dir_box.Items.AddRange(remote_dir.GetEntrieNames().ToArray());
            remote_dir_box.Show();
        }

        private void update_virtual_dir_box()
        {
            virtual_dir_box.Items.Clear();
            foreach (directory_entry entry in virtual_dir)
            {
                virtual_dir_box.Items.Add(entry.GetName());
            }
            virtual_dir_box.Show();
        }
    }
}
