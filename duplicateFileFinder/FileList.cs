using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace duplicateFileFinder
{
    class FileList
    {
        static private  List<File> files = new List<File>();
        static public List<string> hashs = new List<string>();
        
        private  class File
        {
            string NameWithExt;
            string Ext;
            string Path;
            string Md5;

            public File(string path, string file, string ext, string md5)
            {
                this.NameWithExt = file;
                this.Ext = ext;
                this.Path = path;
                this.Md5 = md5;
            }
        }

        static public bool AddFile (string path, string file, string ext, string md5)
        {
            var f = new File(path, file, ext, md5);
            files.Add(f);
            if (md5 != null)
            {
                if (hashs.IndexOf( md5) == -1)
                {
                    hashs.Add(md5);
                }
            }
            return true;
        }
    }
}
