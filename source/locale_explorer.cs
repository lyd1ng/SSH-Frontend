using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace scp_frontend
{
    class locale_explorer
    {
        locale_directory locale_dir;
        List<directory_entry> virtual_dir;
        ListBox locale_dir_box, virtual_dir_box;

        public locale_explorer(string path, ListBox locale_dir_box, ListBox virtual_dir_box)
        {
            this.locale_dir = new locale_directory(path);
            this.virtual_dir = new List<directory_entry>();
            this.locale_dir_box = locale_dir_box;
            this.virtual_dir_box = virtual_dir_box;
            update_local_dir_box();
            update_virtual_dir_box();
        }

        public void show()
        {
            locale_dir.read_directory();
            update_local_dir_box();
            update_virtual_dir_box();
        }

        public void locale_dir_double_click(object sender, MouseEventArgs e)
        {
            int index = locale_dir_box.IndexFromPoint(e.X, e.Y);
            directory_entry selection = locale_dir.GetEntryAt(index);
            if (selection.IsDirectory())
            {
                locale_dir.change_directory(selection);
                update_local_dir_box();
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
        public locale_directory get_locale_directory() { return locale_dir; }

        private void update_local_dir_box()
        {
            locale_dir_box.Items.Clear();
            locale_dir_box.Items.AddRange(locale_dir.GetEntrieNames().ToArray());
            locale_dir_box.Show();
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
