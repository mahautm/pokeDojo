using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeDojo_GGMM
{
    class AlterationEtatBouclier : AlterationEtat
    {
        public int PVBouclier { get; set; }

        public AlterationEtatBouclier(int duree, bool reflexif, int pvBouclier) : base(duree, reflexif)
        {
            PVBouclier = pvBouclier;
        }

        public override string ToString()
        {
            return "Bouclier de " + PVBouclier + " PV";
        }
    }
}
