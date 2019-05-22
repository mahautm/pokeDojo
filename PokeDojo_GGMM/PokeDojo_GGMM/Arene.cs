 
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
                return _competiteurs;
            }
            set
            {
                if (value.Count == 16)
                    _competiteurs = value;
                else Console.WriteLine("Il faut 16 compétiteurs exactement, vérifiez vos données !");
            }
        }


        //!! je propose de modéliser l'arbre avec une liste de listes de joueurs : 16 au premier round, 8 au deuxième, 4 au suivant, etc.
        //!! on aurait une liste de forme [ [16] , [8] , [4] , [2] , [1]  ]
        //!! et on pourrait utiliser une fonction qui affiche l'arbre sous forme pyramidale
        //!! liste joueurs + P.id

        private List<List<Joueur>> _arbre;
        public List<List<Joueur>> Arbre
        {
            get
            {
                return _arbre;
            }
            set
            {
                _arbre = value;
                {
                    /*
                    //!! on vérifie s'il y a des doublons dans value
                    bool doublon = false;
                    foreach(Joueur competiteur in value[_arbre.Count])
                    {
                        int count = 0;
                        foreach(Joueur compare in value[_arbre.Count])
                        {
                            if (competiteur == compare)
                                count++;
                            if (count > 1)
                                doublon = true;
                        }
                    }

                    //!! si :
                    //!! 1/ il y a deux fois mois de joueurs qu'au tour précédent
                    //!! 2/ il n'y a pas de doublons
                    //!! 3/ il y a un élément liste de plus dans value que dans _arbre (1 profondeur de plus dans la compétition)
                    if (value[_arbre.Count].Count <= _arbre[_arbre.Count - 1].Count / 2 && !doublon && value.Count == _arbre.Count + 1)
                    {
                        _arbre = value;
                    }
                    */
                }
            }
        }



        //Constructeur
        public Arene(List<Joueur> competiteurs)
        {
            Competiteurs = competiteurs;
            _arbre= new List<List<Joueur>> { competiteurs };
        }

        //Méthodes
        //!! une méthode qui génère l'arbre de tournoi, une qui permet de simuler une étape dans l'arbre 
        //!! (une partie du joueur, 15 parties simulées par exemple)

        public void AfficherArbreCompetition()
        {
            // affiche la liste des compétiteurs
            Console.WriteLine("Voici les competiteurs !");
            foreach (Joueur competiteur in Arbre[0])
            {
                int index = Arbre[0].IndexOf(competiteur)+1;
                string numeroJoueur = "";
                if (index < 10)
                    numeroJoueur += 0 + "" + index;
                else
                    numeroJoueur += index;
                Console.Write("P" + numeroJoueur + " " + competiteur + "\t\t");

                if(Arbre[0].IndexOf(competiteur)%4==3)
                    Console.WriteLine();
            }

            // affiche l'arbre de la compétition
            foreach (List<Joueur> Round in Arbre)
            {
                int round = Arbre.IndexOf(Round) + 1;
                Console.WriteLine("\n\nRound " + round);

                int positionVS = 0;
                foreach (Joueur competiteurRestant in Round)
                {
                    
                    int index = Arbre[0].IndexOf(competiteurRestant)+1;
                    string numeroJoueur = "";
                    if (index < 10)
                        numeroJoueur += 0  + "" + index;
                    else
                        numeroJoueur += index;

                    if(positionVS % 2 == 0 && Round.Count != 1)
                    {
                        positionVS++;
                        Console.Write("P" + numeroJoueur + " VS ");
                    }
                        
                    else
                    {
                        positionVS++;
                        Console.Write("P" + numeroJoueur + "\t");
                    }                       
                    
                }
            }
        }
    }
}
