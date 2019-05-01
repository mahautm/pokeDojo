using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeDojo_GGMM
{
    class Joueur
    {
        public string Nom { get; set; }

        //Un joueur peut avoir jusqu'à trois pokémon dans son sac.
        //!! Y a t'il une méthode où je n'ai pas besoin de la variable _sac et où je peux tout faire à la propriété?
        private List<Pokemon> _sac;
        public List<Pokemon> Sac
        {
            get
            {
                return this._sac;
            }
            set
            {
                if(value.Count == 3)
                this._sac = value;
            }
        }

        //Constructeur
        public Joueur(string nom, List<Pokemon> sac)
        {
            if(sac.Count != 3)
            {
                Console.WriteLine("Erreur, un joueur doit avoir 3 Pokémons");
                //!! Est-ce que le constructeur a moyen de s'autodétruire vu que les conditions ne sont pas remplies ? Sinon on lui en donne trois aléatoirement.
            }
            else
            {
                _sac = sac;
                nom = Nom;
            }

        }

        //!! Quand on aura une BDD de pokémons il faudra que ce constructeur tire des Pokemons aléatoirement.
        public Joueur() : this("Joueur",new List<Pokemon>()) { }
    }
}
