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
            //!! Pour Guillaume : Je note les commentaires qui devront être
            //!! supprimés avant le rendu final avec 2 points d'exclammation
            //!! C'est pour m'aider à coder, pas pour expliquer le code.

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

            Arene arene = new Arene(new List<Joueur> { j0, j1, j2, j3 });
            Menu();
        }

        public static void DeroulerPartie(Arene arene)
        {
            ;
        }

        public static void JouerCombat(Arene arene)
        {
            ;
        }

        //deux participants, initiative  = 0 ou 1, désigne le joueur qui commence
        public static void JouerTour(Arene arene, List<Joueur> participants, int initiative)
        {
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

    }
}
