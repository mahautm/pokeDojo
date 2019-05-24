using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeDojo_GGMM
{
    class CapaciteSpeciale
    {
        /*
        public static List<string> __nomsCapaciteSpeciale = new List<string>
        {
            "Atak", "Clok", "Viok", "Souk", "Flik",
            "Cirk", "Phok", "Plak", "Bisk", "Rauk"
        };
        */
        public static List<string> __nomsCapaciteSpeciale = new List<string>
        {
            "Chok", "Clok", "Viok", "Tork", "Flok",
            "Krok", "Prok", "Grok", "Brok", "Plok"
        };
        private static Random random = new Random();

        private static List<char> __voyelles = new List<char> { 'A', 'E', 'I', 'O', 'U', 'Y' };

        public string _nom;
        public List<AlterationEtat> _alterations;
                       
        public CapaciteSpeciale(string pokeNom, string pokeType, List<AlterationEtat> alterations)
        {
            _nom = __nomsCapaciteSpeciale[random.Next(__nomsCapaciteSpeciale.Count)];

            if(__voyelles.Contains(pokeType[0]))
                _nom += " d'" + pokeType;
            else
                _nom += " de " + pokeType;
                        
            //!! _nom += pokeNom.Substring(4);

            _alterations = alterations;
        }


        public override string ToString()
        {
            string chres = _nom;
            chres+="\n========";
            if(_alterations.Count>0)
                foreach (AlterationEtat alteration in _alterations)
                    chres += "\n" + alteration;

            return chres;
        }

    }
}
