using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeDojo_GGMM
{
    class Program
    {
        static void Main(string[] args)
        {
            //!! LIENS POUR LES POKEMONS
            //!! https://bulbapedia.bulbagarden.net/wiki/List_of_Pok%C3%A9mon_by_base_stats_(Generation_VII-present)
            //!! https://www.pokebip.com/pokedex/4eme_generation_pokeliste_liste_des_pokemon.html

            //!! Old liste de pokémons
            {
                /*
                Pokemon p0 = new Pokemon("Bulbizarre", 45, 49, 'P');
                Pokemon p1 = new Pokemon("Salamèche", 39, 52, 'F');
                Pokemon p2 = new Pokemon("Carapuce", 44, 48, 'E');
                Pokemon p3 = new Pokemon("Givrali", 65, 60, 'G');

                Pokemon p4 = new Pokemon("Minidraco", 41, 64, 'D');
                Pokemon p5 = new Pokemon("Farfuret", 55, 95, 'T');
                Pokemon p6 = new Pokemon("Foretress", 75, 90, 'A');
                Pokemon p7 = new Pokemon("Embrylex", 50, 64, 'R');
                */
            }
            //!! Old liste des joueurs
            {
                /*
            Joueur j0 = new Joueur("Guiguite38", new List<Pokemon> { p0, p1, p2 });
            Joueur j1 = new Joueur("Matmut14", new List<Pokemon> { p1, p2, p3 });
            Joueur j2 = new Joueur("Zgoogo33", new List<Pokemon> { p2, p3, p4 });
            Joueur j3 = new Joueur("BenDlaRochèl", new List<Pokemon> { p3, p4, p5 });

            Joueur j4 = new Joueur("CocoNono", new List<Pokemon> { p3, p4, p5 });
            Joueur j5 = new Joueur("MarTintin", new List<Pokemon> { p3, p4, p5 });
            Joueur j6 = new Joueur("GogoLeGoth", new List<Pokemon> { p3, p4, p5 });
            Joueur j7 = new Joueur("Marv-1 LeHun", new List<Pokemon> { p3, p4, p5 });
            */
            }
            //!! Old Arene
            {
                /*
                Arene arene = new Arene(new List<Joueur> { j0, j1, j2, j3, j4, j5, j6, j7 });
                arene.Arbre.Add(new List<Joueur> { j1, j2, j5, j7 });
                arene.Arbre.Add(new List<Joueur> { j1, j7 });
                arene.Arbre.Add(new List<Joueur> { j7 });
                */
            }

            //Création des Listes pour les pokémons
            List<string> basique = new List<string> {
                "TrukiPik",    "ShozaFeuil",
                "TrukiBrul",   "TrukaMèsh",
                "TrukiNaj",    "TrukaBull",
                "TrukiCaï",    "ShozaRtik",
                "TrukiVol",    "TrukaDézèl",
                "TrukiCrain",  "TrukaOtik",
                "TrukiBrill",  "TrukaVide",
                "TrukiKrak",   "TrukaLcair"
            };
            List<string> alpha = new List<string>
            {
                "Mini", "Maki", "Piti", "Fifi",
                "Loli", "Kawai", "Shipi", "Mimi"
            };
            List<string> beta = new List<string>
            {
                "Mega", "Peta", "Supra", "Masta",
                "Tera", "Hypra", "Giga", "Ultra"
            };
            List<string> types = new List<string>
            {
                "Plante", "Feu", "Eau", "Glace", "Dragon", "Ténèbres", "Argent", "Roche"
            };

            List<Pokemon> PokeList = CreerPokemons(basique,alpha,beta,types);

            foreach (Pokemon poke in PokeList)
            {
                if (PokeList.IndexOf(poke) % 6 == 0)
                    Console.WriteLine("\n\nElement " + types[poke.TypeElementaire]);

                if (PokeList.IndexOf(poke) % 6 == 3)
                    Console.WriteLine();
                Console.Write(poke + "\t\t");
            }

            List<string> nomsDresseurs = new List<string>
            {
                "Sasha   ", "Pasha    ", "Datsha   ", "Chisha   ",
                "Guarasha", "Katiousha", "Shashasha", "Kurarasha",
                "Galusha ", "Crasha   ", "Exarsha  ", "TeleAsha ",
                "Shosha  ", "Moksha   ", "Geisha   ", "MilkShaha"
            };

            List<Joueur> dresseurs = new List<Joueur>();

            Random random = new Random();

            foreach (string dresseur in nomsDresseurs)
            {
                //Console.WriteLine("\n\n" + dresseur);
                
                List<int> indexPokemons = GenererNint(3,0,16,random);
                /*
                foreach (int index in indexPokemons)
                    Console.Write(" " + index);
*/

                List<Pokemon> pokemons = new List<Pokemon>();
                foreach (int index in indexPokemons)
                {
                    pokemons.Add(PokeList[index*3]);
                }
                dresseurs.Add(new Joueur(dresseur, pokemons));
            }

            Console.WriteLine("\n\nVoici les dresseurs !!!");
            foreach (Joueur dresseur in dresseurs)
                Console.WriteLine(dresseur);

            List<int> indexTest = GenererNint(10, 0, 16, random);

            Console.WriteLine("\n\nEvolution !!!");
            foreach (int index in indexTest)
            {
                Console.WriteLine(EvoluerPokemon(PokeList[index], PokeList).ToString(true) + "\n");
            }

            AlterationEtatPA alter = new AlterationEtatPA(3, true, -30);
            AlterationEtatPA alter2 = new AlterationEtatPA(3, false, 30);
            CapaciteSpeciale test = new CapaciteSpeciale(PokeList[1].Nom, types[PokeList[1].TypeElementaire], new List<AlterationEtat> { alter,alter2 });

            Console.WriteLine(test);

            Console.ReadLine();

            //AlterationEtatPA test = new AlterationEtatPA("test", 3, true, 30);
        }

        //!!=========
        //FIN DU MAIN
        //!!=========
        
        //Creation de Pokemons en mode fonction
        public static List<Pokemon> CreerPokemons(List<string> basique, List<string> alpha, List<string> beta, List<string> types)
        {
            List<Pokemon> ListePokemons = new List<Pokemon>();

            int count = 0;
            int indexType = 0;
            Random random = new Random();

            foreach (string basik in basique)
            {
                if (count % 2 == 0 && count != 0)
                    indexType++;

                ListePokemons.Add(new Pokemon(alpha[random.Next(alpha.Count)] + basik, random.Next(50, 75), random.Next(15, 25), types[indexType][0]));

                ListePokemons.Add(new Pokemon(basik, random.Next(100, 150), random.Next(35, 55), types[indexType][0]));

                ListePokemons.Add(new Pokemon(beta[random.Next(beta.Count)] + basik, random.Next(200, 250), random.Next(45, 65), types[indexType][0]));

                count++;
            }

            return ListePokemons;
        }

        public static Pokemon EvoluerPokemon(Pokemon pokemon, List<Pokemon> ListePokemons)
        {
            if (ListePokemons.IndexOf(pokemon) % 3 != 2)
            {
                Console.WriteLine("L'évolution de " + pokemon + " en " + ListePokemons[ListePokemons.IndexOf(pokemon) + 1] + " est un succès.");
                return (ListePokemons[ListePokemons.IndexOf(pokemon) + 1]);
            }
            else
            {
                Console.WriteLine("Impossible de faire évoluer " + pokemon + " : ce pokémon est déjà très badass.");
                return pokemon;
            }
                
        }

         

        //GENERE N INT ALEATOIRES
        public static List<int> GenererNint(int N, int min, int max, Random random)
        {
            List<int> entiers = new List<int> { };
            int valeur;

            while (entiers.Count() != N)
            {
                valeur = random.Next(min,max);
                if (!entiers.Contains(valeur))
                {
                    entiers.Add(valeur);
                }
            }

            return entiers;
        }


        public static void DeroulerPartie(Arene arene)
        {
            //!! 1 : Faire apparaitre l'arbre des joueurs
            arene.AfficherArbreCompetition();

            //!! 2 : Faire jouer le match entre le joueur et son adversaire
                //!! Choisir au hasard lequel commence grâce au pile ou face
                //!! Chacun leur tour les joueurs choisissent leurs actions, à la fin l'un d'entre eux meurent
                    //!! Si le joueur meurt, aller à l'écran de fin, montrer les arbres simulés pour qu'il sache quel joueur virtuel a gagné
                    //!! Si l'adversaire meurt, aller à l'étape suivante

            //!! 3 : Simuler les matchs entre les joueurs aléatoires
                //!! Faire jouer une partie mais sans afficher les écrans texte intermédiaires
                //!! OU Assigner une probabilité de victoire en fonction du type et de la puissance d'attaque en mode 'light'
            //!! Itérer jusqu'à la finale
                //!! Ajouter un écran de victoire et un HIGHSCORES pour l'UX
            ;
        }
        // Dans la version actuelle du jeu il ne peut y avoir qu'un joueur humain.
        // S'il y a un joueur humain, c'est j1
        public static Joueur JouerCombat(Joueur j1, Joueur j2)
        {
            bool commencerJ1 = JouerPileOuFace();
            Random R = new Random();
            j2.Actif = j2.Sac[R.Next(3)];
            if (j1.EstHumain)
            {
                Console.WriteLine("Bienvenue dans ce tournoi : {0} vs {1}", j1.Nom, j2.Nom);
                Console.WriteLine("Montez sur le Tatami, et choisissez votre premier pokémon !");
                Console.WriteLine("\n\t\t--- Appuyer sur une touche pour monter sur le Tatami ---");
                Console.ReadKey();
                j1.Actif = ChoisirPokemon(j1);
                //Console.WriteLine("{0} : {1} je te choisis !",j1.Nom, p1.Nom);
                //Console.WriteLine("{0} regarde dans son sac...\n{0} : {1} je te choisis !", j2.Nom, p2.Nom);
            }
            else
                j1.Actif = j1.Sac[R.Next(3)];

            List<Joueur> Dojo;

            bool pokeVivant = true;
            while (pokeVivant)
            {
                if (commencerJ1)
                {
                    pokeVivant = JouerTour(j1, j2);
                }
                if (!pokeVivant)
                {
                    //Si le pokémon actif est KO, on cherche dans le sac s'il reste des pokémons vivants
                    foreach (Pokemon p in j2.Sac)
                        if (p.MarqueurDegats < p.PV)
                            j2.Actif = p;
                    if (j2.Actif.MarqueurDegats < j2.Actif.PV)
                        // Si ce n'est pas le cas, c'est l'autre joueur qui a gagné.
                        return j1;
                }
                else pokeVivant = JouerTour(j2, j1);
                if (!pokeVivant)
                {
                    if (true) ;
                }

                commencerJ1 = true;
            }


            return j1;
        }

        //deux participants, initiative  = 0 ou 1, désigne le joueur qui commence
        public static bool JouerTour(Joueur j1, Joueur j2)
        {
            Random R = new Random();
            int choix;
            if (j1.EstHumain)
            {

                Console.WriteLine("{0} Attend vos instruction ...", j1.Actif.Nom);
                choix = Menu();
            }
            else choix = 1;
            if (choix == 1)
            {
                //!!Attaque

                j2.Actif.RecevoirDegats(j1.Actif);
                if (j1.EstHumain || j2.EstHumain)
                {
                    Console.WriteLine("{0} Attaque {1}, qui perd {2} points de vie !", j1.Actif.Nom, j2.Actif.Nom, j2.Actif.HistoriqueDegats[-1]);
                }
                if (j2.Actif.MarqueurDegats > j2.Actif.PV)
                    return false;
            }
            else if (choix == 2)
            {
                //!!Replis d'un pokémon
                if (j1.EstHumain)
                {
                    ChoisirPokemon(j1);
                    Console.WriteLine("{0} : {1} je te choisis !", j1.Nom, j1.Actif.Nom);
                }
            }
            else if (choix == 3)
            {
                //!!Soin
                if (j1.EstHumain)
                {
                    Console.WriteLine("WIP : Cette option n'est pas encore disponible");
                }
            }
            else if (choix == 4)
            {
                //!!Fuite
                if (j1.EstHumain)
                {
                    Console.WriteLine("WIP : Cette option n'est pas encore disponible");
                }
            }
            return true;
        }

        public static int Menu()
        {
            // Menu de choix : choisir une action au cours d'une partie à l'aide des flèches du clavier,
            //renvoie une valeur entre 1 et 4 en fonction du choix utilisateur
            ConsoleKey cki = Console.ReadKey().Key;
            int choix = 0;
            do
            {
                Console.Clear();
                if (choix == 0)
                    Console.WriteLine(">>\tAttaque\t\t\tReplis\n\tSoin\t\t\tFuite");
                else if (choix == 1)
                    Console.WriteLine("\tAttaque\t\t>>\tReplis\n\tSoin\t\t\tFuite");
                else if (choix == 2)
                    Console.WriteLine("\tAttaque\t\t\tReplis\n>>\tSoin\t\t\tFuite");
                else if (choix == 3)
                    Console.WriteLine("\tAttaque\t\t\tReplis\n\tSoin\t\t>>\tFuite");

                //Attente puis enregistrement d'une entrée utilisateur
                cki = Console.ReadKey().Key;

                //Modification de l'option menu choisie en fonction de la saisie utilisateur
                if (cki == ConsoleKey.LeftArrow)
                    choix = (choix - 1) % 4;
                if (cki == ConsoleKey.RightArrow)
                    choix = (choix + 1) % 4;
                if (cki == ConsoleKey.UpArrow)
                    choix = (choix + 2) % 4;
                if (cki == ConsoleKey.DownArrow)
                    choix = (choix - 2) % 4;

                if (choix < 0)
                    choix = 4 + choix;


            } while (cki != ConsoleKey.Enter && cki != ConsoleKey.Spacebar);
            return choix;
        }

        public static bool JouerPileOuFace()
        {
            bool choix = true;
            ConsoleKey cki = ConsoleKey.UpArrow;
            Random R = new Random();

            //Menu controllable avec les flèches du clavier
            do
            {
                Console.Clear();
                if (choix == true)
                    Console.WriteLine("  --> Pile <--   ou       Face\n\n(utiliser les flèches du clavier pour sélectionner)");
                else Console.WriteLine("      Pile       ou   --> Face <--\n\n(utiliser les flèches du clavier pour sélectionner)");
                cki = Console.ReadKey().Key;


                if (cki == ConsoleKey.LeftArrow || cki == ConsoleKey.RightArrow) choix = !choix;

            } while (cki != ConsoleKey.Enter && cki != ConsoleKey.Spacebar);
            Console.Clear();

            //Application du choix menu, utilisation de l' "aléatoire" de la machine
            if (choix == (new Random().Next(2) == 0))
            {
                Console.WriteLine("... Gagné !! Bravo, tu joueras donc le premier");
                Console.ReadKey();
                return true;
            }
            else
            {
                Console.WriteLine("... Perdu. Pas de chance. Je commencerai donc la partie.");
                Console.ReadKey();
                return false;
            }
        }

        public static Pokemon ChoisirPokemon(Joueur j)
        {
            //!! Attention il faut empêcher de choisir les pokémons KO
            Console.Clear();


            ConsoleKey cki = ConsoleKey.UpArrow;
            int choix = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("{0} ouvre son sac et regarde à l'interieur : \n", j.Nom);
                for (int i = 0; i < j.Sac.Count(); i++)
                {
                    if (choix == i)
                    {
                        if (j.Sac[choix].MarqueurDegats < j.Sac[choix].PV)
                            Console.Write(">>");
                        else Console.Write("*|");
                    }

                    Console.Write("\t{0} \t: {1} PV, \t{2} PA", j.Sac[i].Nom, j.Sac[i].PV - j.Sac[i].MarqueurDegats, j.Sac[i].PA);
                    if (j.Sac[choix].MarqueurDegats < j.Sac[choix].PV)
                        Console.Write("\tKO");
                    Console.WriteLine();

                }
                //Attente puis enregistrement d'une entrée utilisateur
                cki = Console.ReadKey().Key;

                //Modification de l'option menu choisie en fonction de la saisie utilisateur
                if (cki == ConsoleKey.UpArrow)
                    choix = (choix - 1) % 3;
                if (cki == ConsoleKey.DownArrow)
                    choix = (choix + 1) % 3;

                if (choix < 0)
                    choix = 3 + choix;


            } while (cki != ConsoleKey.Enter && cki != ConsoleKey.Spacebar && j.Sac[choix].MarqueurDegats < j.Sac[choix].PV);
            Console.Clear();
            return j.Sac[choix];
        }
    }
}
