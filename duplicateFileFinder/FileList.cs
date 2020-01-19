using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace duplicateFileFinder
{
    class FileList
    {
        static private  List<_file> _files = new List<_file>();
        
        
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
    }
}
