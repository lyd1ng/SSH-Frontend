using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace scp_frontend
{
    abstract class directory
    {
        protected string path;
        protected List<directory_entry> entries;

        //Get Methods
        public string GetPath() { return path; }
        public directory_entry GetEntryAt(int index) { return entries[index]; }
        public List<string> GetEntrieNames()
        {
            List<string> names = new List<string>();
            foreach (directory_entry entry in entries)
            {
                names.Add(entry.GetName());
            }
            return names;
        }
        public List<string> GetFileNames()
        {
            List<string> files = new List<string>();
            for (int i = 0; i < entries.Count; i++)
            {
                if (!entries[i].IsDirectory()) { files.Add(entries[i].GetName()); }
            }
            return files;
        }
        public List<string> GetSubdirectoryNames()
        {
            List<string> directories = new List<string>();
            for (int i = 0; i < entries.Count; i++)
            {
                if (entries[i].IsDirectory()) { directories.Add(entries[i].GetName()); }
            }
            return directories;
        }
        public List<string> GetEntriePaths()
        {
            List<string> names = new List<string>(entries.Count);
            for (int i = 0; i < entries.Count; i++)
            {
                names[i] = entries[i].GetPath();
            }
            return names;
        }
        public List<string> GetFilePath()
        {
            List<string> files = new List<string>();
            for (int i = 0; i < entries.Count; i++)
            {
                if (!entries[i].IsDirectory()) { files.Add(entries[i].GetPath()); }
            }
            return files;
        }
        public List<string> GetSubdirectoryPaths()
        {
            List<string> directories = new List<string>();
            for (int i = 0; i < entries.Count; i++)
            {
                if (entries[i].IsDirectory()) { directories.Add(entries[i].GetPath()); }
            }
            return directories;
        }
    }
}
