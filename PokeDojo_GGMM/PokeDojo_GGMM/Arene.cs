﻿ 
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

        internal List<Pokemon> PokeList1 { get => PokeList; set => PokeList = value; }
        
        //Création des Listes pour les pokémons : Evolutions et types élémentaires
        readonly List<string> basique = new List<string> {
                "TrukiPik",    "TrukaFeuil", "Brikactus", "Shozasève",  "Poképi",    "Pikabaga",
                "TrukiBrul",   "TrukaMèsh",  "Brikatizé", "Shozardentt","Pokrépite", "Pikalumett",
                "TrukiNaj",    "TrukaBull",  "Brikégou",  "Shozakeuz",  "Pokékluz",  "Pikakduk",
                "TrukiCaï",    "ShozaRtik",  "Briglassé", "Shozigloo",  "Pokibéri",  "Pikalpin",
                "TrukiVol",    "TrukaDézèl", "Brikaéré",  "Shozailée",  "Pokécaillé","Pikazoté",
                "TrukiCrain",  "TrukaOtik",  "Brikopak",  "Shozobskur", "Pokérétik", "Pikabîme",
                "TrukiBrill",  "TrukaVide",  "Brigrena",  "Shozonyx",   "Pokétain",  "PikAgate",
                "TrukiKrak",   "TrukaLcair", "Brikolitik","Shozabsid", "Pokengrès", "Pikaveau"
            };
        readonly List<string> mimi = new List<string>
            {
                "Mini",     "Maki",     "Piti",     "Fifi",
                "Loli",     "Kawai",    "Shipi",    "Mimi",
                "Toopi",    "Kiwi",     "Sisi",     "Merri",
                "Bibi",     "Joli",     "Piti",     "Dodi",
            };
        readonly List<string> mega = new List<string>
            {
                "Mega", "Peta", "Supra", "Masta",
                "Tera", "Hypra", "Giga", "Ultra",
                "Oura", "Alpha", "Octa", "Kobra",
                "Grota", "Bigta", "Femta", "Vigra"
            };
        readonly List<string> types = new List<string>
            {
                "Plante", "Feu", "Eau", "Glace", "Dragon", "Ténèbres", "Argent", "Roche"
            };
        readonly List<string> nomsDresseurs = new List<string>
            {
                "Sasha   ", "Pasha    ", "Datsha   ", "Chisha   ",
                "Guarasha", "Katiousha", "Shashasha", "Kurarasha",
                "Galusha ", "Crasha   ", "Exarsha  ", "TeleAsha ",
                "Shosha  ", "Moksha   ", "Geisha   ", "MilkShaha"
            };

        public List<Pokemon> PokeList;


        //Constructeur
        public Arene(List<Joueur> competiteurs)
        {
            PokeList1 = CreerPokemons(basique, mimi, mega, types);
            Competiteurs = competiteurs;
            _arbre= new List<List<Joueur>> { competiteurs };
        }
        public Arene()
        {
            PokeList1 = CreerPokemons(basique, mimi, mega, types);
            Random random = new Random();
            Competiteurs = CreerJoueurs(nomsDresseurs, PokeList1, random);
            _arbre = new List<List<Joueur>> { Competiteurs };
        }

        //Méthodes
        //!! une méthode qui génère l'arbre de tournoi, une qui permet de simuler une étape dans l'arbre 
        //!! (une partie du joueur, 15 parties simulées par exemple)

        public static List<Pokemon> CreerPokemons(List<string> basique, List<string> mimi, List<string> mega, List<string> types)
        {
            List<Pokemon> ListePokemons = new List<Pokemon>();

            int count = 0;
            int indexType = 0;
            Random random = new Random();

            foreach (string basik in basique)
            {
                if (count % 6 == 0 && count != 0)
                    indexType++;

                ListePokemons.Add(new Pokemon(mimi[random.Next(mimi.Count)] + basik, random.Next(50, 75), random.Next(15, 25), types[indexType][0]));

                ListePokemons.Add(new Pokemon(basik, random.Next(100, 150), random.Next(35, 55), types[indexType][0]));

                ListePokemons.Add(new Pokemon(mega[random.Next(mega.Count)] + basik, random.Next(200, 250), random.Next(45, 65), types[indexType][0]));


                count++;
            }

            return ListePokemons;
        }

        public static List<Joueur> CreerJoueurs(List<string> nomsDresseurs, List<Pokemon> pokeList, Random random)
        {
            List<Pokemon> miniPokemon = new List<Pokemon>();
            foreach (Pokemon pokemon in pokeList)
                if(pokemon.Evolution==0)
                    miniPokemon.Add(pokemon);

            //Liste des dresseurs
            List<Joueur> dresseurs = new List<Joueur>();

            // création des 16 Joueurs
            foreach (string dresseur in nomsDresseurs)
            {
                List<int> indexPokemons = GenererNint(3, 0, 47, random);

                List<Pokemon> pokemons = new List<Pokemon>();
                foreach (int index in indexPokemons)
                {
                    //on copie le modèle présent dans pokeList
                    pokemons.Add(new Pokemon(miniPokemon[index].Nom, miniPokemon[index].PV, miniPokemon[index].PA, miniPokemon[index]._types[miniPokemon[index].TypeElementaire][0]));
                }
                foreach (Pokemon poke in pokemons)
                    poke.NouvelleCapacite();

                dresseurs.Add(new Joueur(dresseur, pokemons));
            }

            return dresseurs;
        }

        public static List<int> GenererNint(int N, int min, int max, Random random)
        {
            List<int> entiers = new List<int> { };
            int valeur;

            while (entiers.Count() != N)
            {
                valeur = random.Next(min, max);
                if (!entiers.Contains(valeur))
                {
                    entiers.Add(valeur);
                }
            }

            return entiers;
        }

        public void AfficherArbreCompetition()
        {
            // affiche la liste des compétiteurs
            Console.WriteLine("Voici les competiteurs !\n");
            foreach (Joueur competiteur in Arbre[0])
            {
                int index = Arbre[0].IndexOf(competiteur)+1;
                string numeroJoueur = "";
                if (index < 10)
                    numeroJoueur += 0 + "" + index;
                else
                    numeroJoueur += index;
                Console.Write("P" + numeroJoueur + " " + competiteur + "\n");

                if(Arbre[0].IndexOf(competiteur)%4==3)
                    Console.WriteLine();
            }

            // affiche l'arbre de la compétition
            foreach (List<Joueur> Round in Arbre)
            {
                int round = Arbre.IndexOf(Round) + 1;
                Console.WriteLine("\n\nLe round " + round + " est composé des combats :");

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
