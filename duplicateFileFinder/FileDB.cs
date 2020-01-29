using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace duplicateFileFinder
{

    class _file
    {
        public string NameWithExt { get; }
        string Ext;
        public string Path { get; }
        public string Md5 { get; private set; }
        public bool NeedRemov { get; set; } = false;

        public _file(string path, string file, string ext, string md5="")
        {
            this.NameWithExt = file;
            this.Ext = ext;
            this.Path = path;
            this.Md5 = md5;
        }

        public override bool Equals(object obj)
        {
            // If this and obj do not refer to the same type, then they are not equal.
            if (obj.GetType() != this.GetType()) return false;

            // Return true if  x and y fields match.
            var other = (_file)obj;
            return (this.NameWithExt == other.NameWithExt) && (this.Path == other.Path) && (this.Md5 == other.Md5);
        }

        public override String ToString()
        {
            return Path+"\\"+NameWithExt;
        }

        public string GetMd5 ()
        {
            if (this.Md5.Count() > 0)
                return this.Md5;

            var md5 = MD5.Create();
            var data = File.ReadAllBytes(Path+"\\"+NameWithExt);
           this.Md5 = BitConverter.ToString(md5.ComputeHash(data)).Replace("-", string.Empty);
            return this.Md5;
        }

    }
    class FileDB
    {
        private  static List<_file> _files = new List<_file>();

        public FileDB()
        {
            Clear();
        }



        public bool AddFile (string path, string file, string ext, string md5="")
        {
            var f = new _file(path, file, ext, md5);
            _files.Add(f);
            if (md5 != null)
            {
                SignatureList.Add(md5);
            }
            return true;
        }

        public bool NeedRemove(string NameWithExt)
        {

            foreach (var f in _files)
            {
                if (f.NameWithExt.ToString().Equals(NameWithExt))
                    return f.NeedRemov;
            }
            return false;
        }

        public static _file[] FileListWithSignature (string sig)
        {
            List<_file> l = new List<_file>();

            foreach (var f in _files)
            {
                if (f.Md5.Equals(sig))
                    l.Add(f);
            }
            return l.ToArray();
        }

        public static void Clear ()
        {
            _files.Clear();
            SignatureList.Clear();
        }
     }
}
