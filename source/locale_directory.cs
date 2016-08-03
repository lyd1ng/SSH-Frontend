using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace scp_frontend
{
    class locale_directory : directory
    {
        public locale_directory(string path)
        {
            this.path = path;
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
            this.entries = new List<directory_entry>();
            entries.Add(new directory_entry(path + "..\\", path, true));
            try
            {
                foreach (string entry_path in System.IO.Directory.GetDirectories(path))
                {
                    entries.Add(new directory_entry(entry_path + "\\", path, true));
                }
            }
            catch (UnauthorizedAccessException exception)
            {
                Form alert = new alert_form("Permission Denied!");
                alert.Show();
                path = Directory.GetParent(Directory.GetParent(path).FullName).FullName;
                read_directory();
                return;
            }
            foreach (string entry_path in System.IO.Directory.GetFiles(path))
            {
                entries.Add(new directory_entry(entry_path, path, false));
            }
        }
    }
}
