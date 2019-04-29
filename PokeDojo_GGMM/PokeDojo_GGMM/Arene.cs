 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeDojo_GGMM
{
    class Arene
    {
        private List<Joueur> _competiteurs;

        public List<Joueur> Competiteurs
        {
            get
            {
                return this._competiteurs;
            }
            set
            {
                if (value.Count == 4)
                    this._competiteurs = value;
                else Console.WriteLine("Il faut 4 compétiteurs exactement, vérifiez vos données !");
            }
        }
        
        //Constructeur
        public Arene(List<Joueur> competiteurs)
        {
            Competiteurs = competiteurs;
        }

        //Méthodes
        //!! une méthode qui génère l'arbre de tournoi, une qui permet de simuler une étape dans l'arbre 
        //!! (une partie du joueur, 15 parties simulées par exemple)
    }
}
