using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tvp2projekat
{
    [Serializable]
    class Skor
    {
        public int pozicija { get; set; }
        public int score { get; set; }
        public string username { get; set; }

        public Skor(string un, int sc)
        {
            pozicija = 0;
            username = un;
            score = sc;
        }

        public override string ToString()
        {
            return pozicija + username + score;
        }

    }
}
