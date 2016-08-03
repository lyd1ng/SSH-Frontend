using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace scp_frontend
{
    class directory_entry
    {
        string path;
        string name;
        bool is_directory;

        public directory_entry() { }

        public directory_entry(string path, string directory_path, bool is_directory)
        {
            this.path = path;
            this.name = path.Remove(0, directory_path.Length);
            this.is_directory = is_directory;
        }

        public void create_from_remote(string host, string name, bool is_directory)
        {
            this.path = host + name;
            this.name = name;
            this.is_directory = is_directory;
        }

        //Get Methods
        public string GetPath() { return path; }
        public string GetName() { return name; }
        public bool IsDirectory() { return is_directory; } 
    }
}
