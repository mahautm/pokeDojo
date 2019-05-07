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
            Pokemon p0 = new Pokemon("Bulbizarre", 45, 49, 'P');
            Pokemon p1 = new Pokemon("Salamèche", 39, 52, 'F');
            Pokemon p2 = new Pokemon("Carapuce", 44, 48, 'E');
            Pokemon p3 = new Pokemon("Givrali", 65, 60, 'G');
            Pokemon p4 = new Pokemon("Minidraco", 41, 64, 'D');
            Pokemon p5 = new Pokemon("Farfuret", 55, 95, 'T');
            Pokemon p6 = new Pokemon("Foretress", 75, 90, 'A');
            Pokemon p7 = new Pokemon("Embrylex", 50, 64, 'R');

            Joueur j0 = new Joueur("Guiguite38", new List<Pokemon> { p0, p1, p2 });
            Joueur j1 = new Joueur("Matmut14", new List<Pokemon> { p1, p2, p3 });
            Joueur j2 = new Joueur("Zgoogo33", new List<Pokemon> { p2, p3, p4 });
            Joueur j3 = new Joueur("BenDlaRochèl", new List<Pokemon> { p3, p4, p5 });

            Joueur j4 = new Joueur("CocoNono", new List<Pokemon> { p3, p4, p5 });
            Joueur j5 = new Joueur("MarTintin", new List<Pokemon> { p3, p4, p5 });
            Joueur j6 = new Joueur("GogoLeGoth", new List<Pokemon> { p3, p4, p5 });
            Joueur j7 = new Joueur("Marv-1 LeHun", new List<Pokemon> { p3, p4, p5 });

            Arene arene = new Arene(new List<Joueur> { j0, j1, j2, j3 , j4, j5, j6, j7});
            //Menu();

            arene.Arbre.Add(new List<Joueur> { j1, j2, j5, j7 });
            arene.Arbre.Add(new List<Joueur> { j1, j7 });
            arene.Arbre.Add(new List<Joueur> { j7 });

            arene.AfficherArbreCompetition();

            Console.ReadLine();

            //ChoisirPokemon(j0);
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
            Random R = new Random();
            //p2 est le pokemon actif de j2
            Pokemon p2 = j2.Sac[R.Next(3)];
            //p1 est le pokemon actif de j1
            Pokemon p1;
            if (j1.EstHumain)
            {
                Console.WriteLine("Bienvenue dans ce tournoi : {0} vs {1}", j1.Nom, j2.Nom);
                Console.WriteLine("Montez sur le Tatami, et choisissez votre premier pokémon !");
                p1 = ChoisirPokemon(j1);
            }
            else
                p1 = j1.Sac[R.Next(3)];

            while(p1.MarqueurDegats < p1.PV)
            {

            }

                return j1;
        }

        //deux participants, initiative  = 0 ou 1, désigne le joueur qui commence
        public static void JouerTour(Arene arene, List<Joueur> participants, int initiative)
        {
            Console.WriteLine("");
        }

        public static int Menu()
        {
            // Menu de choix : choisir une action au cours d'une partie à l'aide des flèches du clavier,
            //renvoie une valeur entre 1 et 4 en fonction du choix utilisateur
            ConsoleKey cki = ConsoleKey.UpArrow;
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
            Console.Clear();


            ConsoleKey cki = ConsoleKey.UpArrow;
            int choix = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("{0} ouvre son sac et regarde à l'interieur : \n", j.Nom);
                for (int i = 0; i < j.Sac.Count(); i++)
                {
                    if(choix == i)
                        Console.Write(">>");
                    Console.WriteLine("\t{0} \t: {1} PV, \t{2} PA", j.Sac[i].Nom, j.Sac[i].PV- j.Sac[i].MarqueurDegats, j.Sac[i].PA);
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


            } while (cki != ConsoleKey.Enter && cki != ConsoleKey.Spacebar);
            return j.Sac[choix];
        }
    }
}
