using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace duplicateFileFinder
{
    class SignatureList
    {
        static private List<_signature> _hashs = new List<_signature>();

        private class _signature
        {
            public string Md5  { get; set; }
            public Int32 Count { get; private set; }

            public _signature(string md5, Int32 count)
            {
                this.Md5 = md5;
                this.Count = count;
            }

            public void IncCount()
            {
                Count++;
            }
        }


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
                _hashs.Add(new _signature(md5, 0));
            else
                _hashs[IndexOf(md5)].IncCount();
        }


    }
}
