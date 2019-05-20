using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeDojo_GGMM
{
    abstract class AlterationEtat
    {
        public int Duree { get; set; }
        public bool _reflexif;

        public AlterationEtat(int duree, bool reflexif)
        {
            Duree = duree;
            _reflexif = reflexif;
        }
    }
}
