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

            DeroulerPartie();

            Console.ReadLine();

        }
        

        public static Pokemon EvoluerPokemon(Pokemon pokemon, List<Pokemon> ListePokemons)
        {
            if (ListePokemons.IndexOf(pokemon) % 3 != 2)
            {
                Console.WriteLine("L'évolution de " + pokemon + " en " + ListePokemons[ListePokemons.IndexOf(pokemon) + 1] + " est un succès.");

                //Quand il évolue, le pokémon gagne 2 capacités
                pokemon.NouvelleCapacite();
                pokemon.NouvelleCapacite();

                return (ListePokemons[ListePokemons.IndexOf(pokemon) + 1]);
            }
            else
            {
                Console.WriteLine("Impossible de faire évoluer " + pokemon + " : ce pokémon est déjà très badass.");
                return pokemon;
            }
                
        }
        
        public static void DeroulerPartie()
        {
            Console.WriteLine("\t\t\tPoke DOJO. \n\n\tUn jeu par Guillaume Grosse et Matéo Mahaut\n\t\tProjet Programmation Avancée\n\t\t\tENSC S6");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Vous vous apprétez à participer au plus grand tournoi de dresseurs qu'il soit !");

            // Permettre au joueur de choisir son nom 
            Console.WriteLine("\t\tQuel est votre nom, jeune dresseur ?");
            Console.Write("\t\t");
            string nom = Console.ReadLine();

            // Permettre au joueur de Choisir ses pokémons :
            int depart = 0;
            int selection = 0;
            ConsoleKey cki;
            List<Pokemon> sac = new List<Pokemon>();
            Arene arene = new Arene();

            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("Bonjour {0}, Avec quels Pokemons voulez vous jouer ?",nom);
                    int i = 0;
                    //Afficher la liste des pokémons
                    while (i <= 10)
                    {                    
                            if (i == selection)
                            {
                                //Coloration en fonction de si le Pokémon a déjà été selectionné
                                //On utilise la multiplication par 3 pour ne tomber que sur les pokémonsau premier stade d'évolution.
                                Console.ForegroundColor = !(sac.Contains(arene.PokeList1[3 * (depart + i)]))?ConsoleColor.DarkGreen: ConsoleColor.DarkRed;
                                Console.Write(">>");
                                Console.WriteLine("\t" + arene.PokeList1[3*(depart+i)].Nom + "\t" + arene.PokeList1[3 * (depart + i)].PV + " PV\t" + arene.PokeList1[3 * (depart + i)].PA + "PA\t" + "Elementaire de " + arene.PokeList1[3 * (depart + i)]._types[arene.PokeList1[3 * (depart + i)].TypeElementaire]);
                                Console.ResetColor();
                            }
                            else
                                Console.WriteLine("\t" + arene.PokeList1[3 * (depart + i)]);
                            i++;
                    }

                    //Rappel des choix
                    Console.WriteLine();
                    Console.WriteLine("Voici les pokémons que vous avez sélectionnés :");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    foreach(Pokemon p in sac)
                        Console.WriteLine(p);
                    Console.ResetColor();
                    Console.WriteLine("\nAppuyer sur --> A <-- pour annuler la sélection");

                    cki = Console.ReadKey().Key;

                    //Prise en compte des entrées clavier
                    if (cki == ConsoleKey.UpArrow)
                    {
                        if (selection != 0)
                            selection = (selection - 1);
                        else if (depart != 0)
                        {
                            depart -= 1;
                        }
                    }

                    if (cki == ConsoleKey.DownArrow)
                    {
                        if (selection != 10)
                            selection = (selection + 1);
                        else if (depart != arene.PokeList1.Count/3 - 11) 
                            depart += 1;
                    }

                    //On verifie que le joueur a effectué une selection, une Annulation, et qu'il n'a pas essayé de sélectionner deux fois le même pokémon.
                } while ((cki != ConsoleKey.Enter && cki != ConsoleKey.Spacebar && cki != ConsoleKey.A) || sac.Contains(arene.PokeList1[3 * (selection)]));

                //Permettre une annulation
                if (cki == ConsoleKey.A)
                    sac = new List<Pokemon>();
                else
                    sac.Add(arene.PokeList1[3*(selection + depart)]);

            } while (sac.Count < 3);
            //Créer le joueur personnalisé.
            Joueur j1 = new Joueur(nom, sac)
            {
                EstHumain = true
            };
            // 1 : Faire apparaitre l'arbre des joueurs
            arene.Competiteurs[0] = j1;
            arene.Arbre[0][0] = j1;

            Console.Clear();
            arene.AfficherArbreCompetition();
            Console.ReadKey();

            for (int roundNumber = 0; roundNumber < 3; roundNumber++)
            {
                for (int fightNumber = 0; fightNumber < arene.Arbre[roundNumber].Count; fightNumber+=2)
                {
                    JouerCombat(arene.Arbre[roundNumber][fightNumber], arene.Arbre[roundNumber][fightNumber + 1]);          
                }
            }
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
            Random R = new Random();
            j2.Actif = j2.Sac[R.Next(3)];
            Joueur temp = j2;
            if (j1.EstHumain || j2.EstHumain)
            {
                //On affiche à gauche le nom du joueur et à droite celui de son adversaire
                Console.WriteLine("Bienvenue dans ce tournoi :\n\n\t  *****\n {0} vs {1}\n\t  *****\n\nJouons à Pile ou Face pour déterminer qui commence.", j1.Nom, j2.Nom);
                Console.ReadKey();

                // Si le joueur perd au pile ou face, son adversaire commence, on inverse donc l'ordre.
                if (!JouerPileOuFace())
                {
                    j2 = j1;
                    j1 = temp;
                }
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
                    choix = Menu(j1);
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
                        if(j1.Actif.CapacitesSpeciales.Count!=0)
                        {
                            Console.WriteLine("Quelle Capacité spéciale voulez-vous utiliser ?");
                            for (int i = 0; i < j1.Actif.CapacitesSpeciales.Count; i++)
                            {
                                if (selection == i)
                                    Console.Write(">>");
                                Console.WriteLine("\t{0} - {1}", j1.Actif.CapacitesSpeciales[i]._nom, j1.Actif.CapacitesSpeciales[i]._alterations[0]);
                            }
                            cki = Console.ReadKey().Key;
                        }
                        //!! variante si aucune capa restante
                        else
                        {
                            Console.WriteLine("Votre pokémon est à court de capacités spéciales : il effectue une attaque");
                            cki = Console.ReadKey().Key;
                        }

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

        public static int Menu(Joueur j1)
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


            } while (cki != ConsoleKey.Enter && cki != ConsoleKey.Spacebar &&!(choix == 2 && j1.Actif.CapacitesSpeciales.Count == 0));
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
