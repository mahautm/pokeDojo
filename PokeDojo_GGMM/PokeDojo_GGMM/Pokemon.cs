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

        public int TypeElementaire { get; set; }
        public int TypeVulnerable { get; set; }

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
            // gestion puissance d'attaque
            int forceAttaque = 100;
            foreach(AlterationEtatPA alteration in ennemi.AlterationsEtat)
            {
                if (alteration.Duree == 0)
                    ennemi.AlterationsEtat.Remove(alteration);
                else
                    alteration.Duree -= 1;

                forceAttaque += alteration.Modificateur;
            }

            Random R = new Random();
            int Degats = (int)Math.Ceiling(
                (forceAttaque + R.Next(-20,20)) / (double)100 * ennemi.PA
                );

            //!! cas possible où les dégâts sont tellement réduits qu'ils pourraient soigner l'ennemi (100%-150% -> -50% * PA infligé en dégâts , soit Dégâts < 0 !)
            if (Degats < 0) Degats = 0;

            // gestion bouclier
            bool attaqueAbsorbée = false;
            foreach (AlterationEtatBouclier shield in ennemi.AlterationsEtat)
            {
                if (!attaqueAbsorbée)
                {
                    if (shield.Duree == 0 || shield.PVBouclier < 0)
                        AlterationsEtat.Remove(shield);
                    else
                        shield.Duree -= 1;

                    shield.PVBouclier -= Degats;
                    Degats -= shield.PVBouclier;
                }
                if (Degats <= 0) attaqueAbsorbée = !attaqueAbsorbée;
            }

            // application des dégâts
            if (Degats > 0)
            {
                if (ennemi.TypeElementaire == TypeVulnerable)
                {
                    MarqueurDegats += 2 * Degats;
                    HistoriqueDegats.Add(2 * Degats);
                }
                else
                {
                    MarqueurDegats += Degats;
                    HistoriqueDegats.Add(Degats);
                }
            }
            if (MarqueurDegats >= PV)
                return true;
            return false;
        }
        
        //Creation d'une nouvelle Capacité
        public void NouvelleCapacite()
        {
            int typePouvoir = Random.Next(3);
            if(typePouvoir == 0)
            {
                //!! le poké gagne un shield de 2 tours absorbant jusqu'à [ 50*(3-Evolution) + PV / (2+Evolution) ] dégâts
                CapacitesSpeciales.Add(new CapaciteSpeciale(Nom, _types[TypeElementaire], new List<AlterationEtat> { new AlterationEtatBouclier(2, true, 50*(3-Evolution) + PV/(2+Evolution)) }));
            }
            else
            if (typePouvoir == 1)
            {
                //!! le poké gagne 50% de dégâts en + pdt 3 tours
                CapacitesSpeciales.Add(new CapaciteSpeciale(Nom, _types[TypeElementaire], new List<AlterationEtat> { new AlterationEtatPA(3, true, 50) }));
            }
            else
            {
                //!! l'ennemi gagne 50% de dégâts en + pdt 3 tours
                CapacitesSpeciales.Add(new CapaciteSpeciale(Nom, _types[TypeElementaire], new List<AlterationEtat> { new AlterationEtatPA(3, false, -50) })); 
            }
        }

        public void LancerCapacite(Pokemon ennemi, int capacite)
        {
            //si le pouvoir est réflexif, il affecte le pokémon
            if (CapacitesSpeciales[capacite]._alterations[0]._reflexif)
            {
                AlterationsEtat.Add(CapacitesSpeciales[capacite]._alterations[0]);
            }
                
            //sinon il affecte le pokémon adverse
            else
                ennemi.AlterationsEtat.Add(CapacitesSpeciales[capacite]._alterations[0]);
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
