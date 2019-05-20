using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeDojo_GGMM
{
    class AlterationEtatPA : AlterationEtat
    {
        public int Modificateur { get; set; }

        public AlterationEtatPA(int duree, bool reflexif, int modificateur) : base(duree, reflexif)
        {
            Modificateur = modificateur;
        }

        public override string ToString()
        {
            if (Modificateur < 0)
                return ("Affaiblissement - force diminuée de " + Modificateur + "%");
            else
                return ("Renforcement - force augmentée de " + Modificateur + "%");
        }
    }
}
