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
        static private  List<_file> _files = new List<_file>();
        static private List<string> _filesList = new List<string>();
        
        
        private  class _file
        {
            string NameWithExt;
            string Ext;
            string Path;
            string Md5;

            public _file(string path, string file, string ext, string md5)
            {
                this.NameWithExt = file;
                this.Ext = ext;
                this.Path = path;
                this.Md5 = md5;
            }
        }

        static public bool AddFile (string path, string file, string ext, string md5)
        {
            var f = new _file(path, file, ext, md5);
            _files.Add(f);
            if (md5 != null)
            {
                SignatureList.Add(md5);
            }
            return true;
        }

        static public void Clear ()
        {
            _files.Clear();
            SignatureList.Clear();
        }

        static private void PrepareFileList (string parentdir, string whild, bool recursive)
        {
            var dr = new DirectoryInfo (parentdir);

            if (recursive)
            {
                foreach (DirectoryInfo dir in dr.GetDirectories())
                {
                    try
                    {
                        PrepareFileList(dir.FullName, whild, recursive);
                    }
                    catch { }
                }
            }

            foreach (FileInfo file in dr.GetFiles())
            {
                try
                {
                    if (!file.Extension.ToString().Equals(whild))
                        _filesList.Add(file.FullName.ToString());
                }
                catch { }
            }
        }

        public static string[] GetFileList (string parentdir, string whild, bool recursive)
        {
            PrepareFileList(parentdir, whild, recursive);
            return _filesList.ToArray();
        }
    }
}
