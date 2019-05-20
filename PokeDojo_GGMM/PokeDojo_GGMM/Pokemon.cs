﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeDojo_GGMM
{
    class Pokemon
    {
        //!! verifier que la syntaxe est bonne
        public readonly List<string> _types = new List<string> { "Plante", "Feu", "Eau", "Glace", "Dragon", "Ténèbres", "Argent", "Roche"  };
        //Caractéristiques initiales du pokémon.
        //!!Les niveau d'autorisation de lecture et d'écriture sont encore sujets à évoluer.
        public string Nom { get; protected set; }
        //PV pour points de vie
        public int PV { get; protected set; }
        //PA pour Puissance d'attaque
        public int PA { get; protected set; }

        public List<int> HistoriqueDegats { get; set; }
        public int MarqueurDegats { get; set; }

        public int TypeElementaire { get; set; } //!! il n'y avait pas de setteur
        public int TypeVulnerable { get; set; } //!! il n'y avait pas de setteur

        public List<CapaciteSpeciale> CapacitesSpeciales { get; set; }
        public List<AlterationEtat> AlterationsEtat { get; set; }

        //le niveau d'évolution de 0 à 2 du pokémon
        public int Evolution { get; set; }

        private Random Random { get; set; }


        //Constructeur
        public Pokemon(string nom, int pV, int pA, char typeElementaire)//, int evolution)
        {
            Nom = nom;
            PV = pV;
            PA = pA;
            HistoriqueDegats = new List<int>();
            MarqueurDegats = 0;

            int i = 0;
            bool stay = true;
            while(stay && i<_types.Count)
            {
                if (typeElementaire == _types[i][0])
                {
                    TypeElementaire = i;
                    stay = false;
                }
                i++;
            }
            if (stay)
                Console.WriteLine("ERREUR : le Type \"{0}\" n'existe pas",typeElementaire);

            if (typeElementaire != _types.Count)
                TypeVulnerable = TypeElementaire+1;
            else
                TypeVulnerable = 0;
        }

        //Méthode
        public override string ToString()
        {
            //return "Nom : " + Nom +"\nType : " + _types[TypeElementaire] +"\nFaiblesse : " + _types[TypeVulnerable] + "\nPV : " + PV +"\nPA : " + PA;
            //return Nom + "\t" + PV + " PV\t" + PA + "\t" + "Elementaire de " + _types[TypeElementaire];
            return Nom;
        }

        public string ToString(bool valeur)
        {
            return "Nom : " + Nom +"\nType : " + _types[TypeElementaire] +"\nFaiblesse : " + _types[TypeVulnerable] + "\nPV : " + PV +"\nPA : " + PA;
            //return Nom + "\t" + PV + " PV\t" + PA + "\t" + "Elementaire de " + _types[TypeElementaire];
            //return Nom;
        }

        public bool RecevoirDegats(Pokemon ennemi)
        {
            Random R = new Random();
            int Degats = (int)Math.Ceiling((1 + (R.Next(-20, 20) / (double)100)) * ennemi.PA);
            if (ennemi.TypeElementaire == TypeVulnerable)
            {
                MarqueurDegats += 2*Degats;
                HistoriqueDegats.Add(2*Degats);
            }
            else
            {
                MarqueurDegats += Degats;
                HistoriqueDegats.Add(Degats);
            }

            if (MarqueurDegats >= PV)
                return true;
            return false;
        }
        
        public void NouvelleCapacite()
        {
            
        }

        //GENERE N INT ALEATOIRES
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
    }
}
