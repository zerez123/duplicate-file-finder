using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace duplicateFileFinder
{
    class FileList
    {
        private List<string> _filesList = new List<string>();



        private void PrepareFileList(string parentdir, bool recursive)
        {
            var dr = new DirectoryInfo(parentdir);

            if (recursive)
            {
                foreach (DirectoryInfo dir in dr.GetDirectories())
                {
                    try
                    {
                        PrepareFileList(dir.FullName, recursive);
                    }
                    catch { }
                }
            }

            foreach (FileInfo file in dr.GetFiles())
            {
                try
                {
                   _filesList.Add(file.FullName.ToString());
                }
                catch { }
            }
        }

        public string[] GetFileList(string parentdir, bool recursive)
        {
            PrepareFileList(parentdir, recursive);
            return _filesList.ToArray();
        }
    }
}
