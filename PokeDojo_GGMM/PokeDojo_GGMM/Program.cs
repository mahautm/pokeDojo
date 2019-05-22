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

            Random random = new Random();

            //Création des Listes pour les pokémons : Evolutions et types élémentaires
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

            //Création de la liste des 18 pokémons et de leurs 32 évolutions
            List<Pokemon> PokeList = CreerPokemons(basique,alpha,beta,types);

            //!! Affichage
            {
                /*
                foreach (Pokemon poke in PokeList)
                {
                    if (PokeList.IndexOf(poke) % 6 == 0)
                        Console.WriteLine("\n\nElement " + types[poke.TypeElementaire]);

                    if (PokeList.IndexOf(poke) % 6 == 3)
                        Console.WriteLine();
                    Console.Write(poke + "\t\t");
                }
                */
            }

            // Noms des Joueurs
            List<string> nomsDresseurs = new List<string>
            {
                "Sasha   ", "Pasha    ", "Datsha   ", "Chisha   ",
                "Guarasha", "Katiousha", "Shashasha", "Kurarasha",
                "Galusha ", "Crasha   ", "Exarsha  ", "TeleAsha ",
                "Shosha  ", "Moksha   ", "Geisha   ", "MilkShaha"
            };

            List<Joueur> dresseurs = CreerJoueurs(nomsDresseurs, PokeList, random);
            
            Arene arene = new Arene(dresseurs);

            //!! tests Guillaume
            {
            /*
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
            */
            }
            Console.ReadLine();
        }

        //!!=========
        //FIN DU MAIN
        //!!=========
        
        //Creation de Pokemons
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

        //Creation de Dresseurs
        public static List<Joueur> CreerJoueurs(List<string> nomsDresseurs, List<Pokemon> pokeList, Random random)
        {
            //Liste des dresseurs
            List<Joueur> dresseurs = new List<Joueur>();

            // création des 16 Joueurs
            foreach (string dresseur in nomsDresseurs)
            {
                List<int> indexPokemons = GenererNint(3, 0, 16, random);

                List<Pokemon> pokemons = new List<Pokemon>();
                foreach (int index in indexPokemons)
                {
                    pokemons.Add(pokeList[index * 3]);
                }
                dresseurs.Add(new Joueur(dresseur, pokemons));
            }

            return dresseurs;
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
            Console.WriteLine("Poke DOJO. \n\n Un jeu par Guillaume Grosse et Matéo Mahaut\nProjet Programmation Avancée\nENSC S6");
            Console.ReadKey();
            Console.WriteLine("Vous vous apprétez à participer au plus grand tournoi de dresseurs qu'il soit !");
            //!! Permettre au joueur de choisir son nom (et ses Pokémons ?)
            Console.WriteLine("Comment vous appelez vous jeune dresseur ?");
            string nom = Console.ReadLine();
            // Permettre au joueur de Choisir ses pokémons :
            //!!WIP
            List<Pokemon> sac = new List<Pokemon>();
            Joueur j1 = new Joueur(nom, sac);

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
            Console.Clear();
            //On affiche à gauche le nom du joueur et à droite celui de son adversaire
            Console.WriteLine("Bienvenue dans ce tournoi :\n\n\t  *****\n {0} vs {1}\n\t  *****\n\nJouons à Pile ou Face pour déterminer qui commence.", j1.Nom, j2.Nom);
            Console.ReadKey();

            // Si le joueur perd au pile ou face, son adversaire commence, on inverse donc l'ordre.
            Joueur temp;
            if (!JouerPileOuFace())
            {
                temp = j2;
                j2 = j1;
                j1 = temp;

            }
            Random R = new Random();
            j2.Actif = j2.Sac[R.Next(3)];
            if (j1.EstHumain || j2.EstHumain)
            {
                Console.WriteLine("Appuyez sur un touche, montez sur le Tatami, et choisissez un pokémon !");
                Console.ReadKey();

                if (j1.EstHumain)
                {
                    j1.Actif = ChoisirPokemonHumain(j1);
                    j2.Actif = j1.Sac[R.Next(3)];
                }
                else
                {
                    j1.Actif = j1.Sac[R.Next(3)];
                    j2.Actif = ChoisirPokemonHumain(j2);
                }

                Console.WriteLine("{0} : {1} je te choisis !\n", j1.Nom, j1.Actif.Nom);
                Console.WriteLine("{0} regarde dans son sac...\n{0} : {1} je te choisis !", j2.Nom, j2.Actif.Nom);
            }

            bool pokeVivant = true;
            
            //On entre dans les tours de partie. On ne peut sortir de cette boucle que lorsqu'un joueur gagne, car un return interrompt la fonction et donc la boucle.
            while (true)
            {
                //On joue un tour

                pokeVivant = JouerTour(j1, j2);
                //On verifie que le pokemon n'et pas mort pendant ce tours
                if (!pokeVivant)
                {
                    //s'il est mort on regarde s'il reste un pokémon vivant dans le sac de son dresseur,

                    Console.Write("{0} est KO...",j1.Actif.Nom);

                    foreach (Pokemon p in j2.Sac)
                    {
                        if (p.MarqueurDegats < p.PV)
                            j2.Actif = p;
                    }

                    //On regarde si le pokemon a pu être remplacé
                    if(j2.Actif.MarqueurDegats > j2.Actif.PV)
                    {
                        //Si ce n'est pas le cas, l'autre joueur a gagné.
                        return j1;
                    }
                    //Si le joueur est humain et qu'il n'a pas perdu, on le laisse choisir lui même le pokémon qu'il veut utiliser.
                    if (j2.EstHumain)
                        ChoisirPokemonHumain(j2);
                    Console.WriteLine(" {0} Le remplace avec {1} !", j2.Nom, j2.Actif.Nom);
                    Console.ReadKey();
                    //On inverse ensuite les joueurs pour alterner le joueur qui défend et celui qui attaque.
                }
                temp = j2;
                j2 = j1;
                j1 = temp;
            }
        }

        public static bool JouerTour(Joueur j1, Joueur j2)
        {
            Random R = new Random();
            int choix;
            if (j1.EstHumain || j2.EstHumain)
            {
                Console.WriteLine("C'est au tour de {0}, avec son pokémon {1} !", j1.Nom, j1.Actif.Nom);
                if (j1.EstHumain)
                {
                    AfficherCombat(j1, j2);
                    Console.WriteLine("{0} Attend vos instruction ...", j1.Actif.Nom);
                    //Dans le cas où le joueur est humain, on appelle l'affichage du menu pour savoir quel action le joueur veut effectuer
                    choix = Menu();
                }
                //Par défaut un joueur non-humain attaque.
                else choix = 0;
                if (j2.EstHumain)
                    AfficherCombat(j2, j1);
            }
            else choix = 0;

            //Si le joueur n'est pas humain, on le fait obligatoirement choisir d'attaquer.
            if (choix == 0)
            {
                //!!Attaque
                j2.Actif.RecevoirDegats(j1.Actif);
                if (j1.EstHumain || j2.EstHumain)
                {
                    Console.Clear();
                    //Attention, cette ligne de code ne prend pas en compte l'option que deux joueurs humains s'affrontent.
                    AfficherCombat(j1.EstHumain?j1:j2, j2.EstHumain ? j1 : j2);
                    Console.WriteLine("{0} Attaque {1}, qui perd {2} points de vie !", j1.Actif.Nom, j2.Actif.Nom, j2.Actif.HistoriqueDegats[j2.Actif.HistoriqueDegats.Count - 1]);
                    Console.ReadKey();
                }
                if (j2.Actif.MarqueurDegats > j2.Actif.PV)
                    return false;
            }
            else if (choix == 1)
            {
                //!!Replis d'un pokémon
                if (j1.EstHumain)
                {
                    j1.Actif = ChoisirPokemonHumain(j1);
                    Console.WriteLine("{0} : {1} je te choisis !", j1.Nom, j1.Actif.Nom);
                }
            }

























            else if (choix == 2)
            {
                //!!Capacité spéciale
                if (j1.EstHumain)
                {
                    ConsoleKey cki;
                    int selection = 0;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Quelle Capacité spéciale voulez-vous utiliser ?");
                        for (int i = 0; i<j1.Actif.CapacitesSpeciales.Count;i++)
                        {
                            if(selection == i)
                                Console.Write(">>");
                            Console.WriteLine("\t{0}, {1}", j1.Actif.CapacitesSpeciales[i]._nom, j1.Actif.CapacitesSpeciales[i]._alterations[0]);
                        }
                        cki = Console.ReadKey().Key;
                    } while (cki != ConsoleKey.Enter && cki != ConsoleKey.Spacebar);
                    Console.WriteLine("{0} Utilise {1}... Incroyable !");
                    j1.Actif.LancerCapacite(j2.Actif, selection);
                    Console.WriteLine("WIP : Cette option n'est pas encore disponible");
                }
            }















            else if (choix == 3)
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
                    Console.WriteLine(">>\tAttaque\t\t\t\tReplis\n\tCapacité spéciale\t\tFuite");
                else if (choix == 1)
                    Console.WriteLine("\tAttaque\t\t\t>>\tReplis\n\tCapacité spéciale\t\tFuite");
                else if (choix == 2)
                    Console.WriteLine("\tAttaque\t\t\t\tReplis\n>>\tCapacité spéciale\t\tFuite");
                else if (choix == 3)
                    Console.WriteLine("\tAttaque\t\t\t\tReplis\n\tCapacité spéciale\t>>\tFuite");

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

        public static Pokemon ChoisirPokemonHumain(Joueur j)
        {
            ConsoleKey cki;
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
                    if (j.Sac[i].MarqueurDegats > j.Sac[i].PV)
                    {
                        Console.Write("\tKO");
                    }
                    Console.WriteLine();

                }
                //Expliquer à l'utilisateur pourquoi il ne peut pas sélectionner son Pokémon mort.
                if (j.Sac[choix].MarqueurDegats > j.Sac[choix].PV)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\nVous ne pouvez pas choisir {0}, il est KO :(", j.Sac[choix].Nom);
                    Console.ResetColor();

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\n{0} est prêt, et a très envie d'aider au combat !", j.Sac[choix].Nom);
                    Console.ResetColor();
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


            } while (cki != ConsoleKey.Enter && cki != ConsoleKey.Spacebar || j.Sac[choix].MarqueurDegats > j.Sac[choix].PV);
            Console.Clear();
            return j.Sac[choix];
        }

        public static void AfficherCombat(Joueur j1, Joueur j2)
        {
            // On affiche des indicateurs de dégats aux pokémons actifs.
            for (int i = 0; i<5; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                if (j1.Actif.PV - j1.Actif.MarqueurDegats >= (4.0-i) / 5.0 * j1.Actif.PV)
                    Console.Write("|#|");
                else
                    Console.Write("| |");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                if (j2.Actif.PV - j2.Actif.MarqueurDegats >= (5.0 - i) / 5.0 * j2.Actif.PV)
                    Console.Write("\t\t\t\t|#|");
                else 
                    Console.Write("\t\t\t\t| |");
                Console.WriteLine();
            }
            Console.ResetColor();
            Console.WriteLine("{0} : {1}/{2}PV\t\t{3} : {4}/{5}PV\n", j1.Actif.Nom, j1.Actif.PV - j1.Actif.MarqueurDegats, j1.Actif.PV, j2.Actif.Nom, j2.Actif.PV - j2.Actif.MarqueurDegats, j2.Actif.PV);

        }
    }
}
