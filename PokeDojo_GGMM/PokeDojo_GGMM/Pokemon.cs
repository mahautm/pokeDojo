using System;
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
        public int TypeElementaire { get;}
        public int TypeVulnerable { get; }



        //Constructeur
        public Pokemon(string nom, int pV, int pA, char typeElementaire)
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
                TypeVulnerable = typeElementaire++;
            else
                TypeVulnerable = 0;

        }

        //Méthode
        public override string ToString()
        {
            //!! liste chainées
            return "Nom : " + Nom +"\nType : " + _types[TypeElementaire] +"\nFaiblesse : " + _types[TypeVulnerable] + "\nPV : " + PV +"\nPA : " + PA;
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
        
    }
}
