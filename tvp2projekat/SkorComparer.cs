using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tvp2projekat
{
    class SkorComparer : IComparer<Skor>
    {
        public int Compare(Skor x, Skor y)
        {
            if ((x.score) == y.score)
                return 0;
            else if (x.score < y.score)
                return 1;
            else return -1;
        }
    }
}
