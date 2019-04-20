using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeDojo_GGMM
{
    class Pokemon
    {
        //Caractéristiques initiales du pokémon.
        //!!Les niveau d'autorisation de lecture et d'écriture sont encore sujets à évoluer.
        public string Nom { get; private set; }
        public int PV { get; private set; }
        public int PuissanceAttaque { get; set; }

        //!!Est-ce qu'on a un nombre limité de types ? l'ennoncé note "etc." ce qui me fait supposer que non.
        //!!Rappel de quelques types : plante, feu, eau, électrique, psy, poison, etc.
        public string Type { get;}

        //!!Je pense qu'une vérification s'impose, peut être même au sein d'une classe Type
        //!!Que l'on définirait. Il faut que le feu n'ai qu'une seule faiblesse assignée, pour
        //!! Empêcher deux pokémons de type feu d'avoir deux faiblesses différentes. Je pense à un tableau dans une classe.
        public string Faiblesse { get;}


        //Constructeur
        public Pokemon()
        {

        }
    }
}
