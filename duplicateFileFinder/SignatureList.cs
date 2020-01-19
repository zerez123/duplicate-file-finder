using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace duplicateFileFinder
{
    class Signature
    {
        public string Md5 { get; set; }
        public Int32 Count { get; private set; }

        public Signature(string md5, Int32 count)
        {
            this.Md5 = md5;
            this.Count = count;
        }

        public void IncCount()
        {
            Count++;
        }
    }

    class SignatureList
    {
        static private List<Signature> _hashs = new List<Signature>();

        private static Int32 IndexOf(string md5)
        {
            for (var i = 0; i < _hashs.Count; i++)
            {
                if (_hashs[i].Md5.Equals(md5))
                {
                    return i;
                }
            }
            return -1;
        }

         public static void Clear()
        {
            _hashs.Clear();
        }

         public static void Add(string md5)
        {
            if (IndexOf(md5) == -1)
                _hashs.Add(new Signature(md5, 1));
            else
                _hashs[IndexOf(md5)].IncCount();
        }

        public static Signature[] GetSignatures ()
        {
            return _hashs.ToArray();
        }

    }
}
