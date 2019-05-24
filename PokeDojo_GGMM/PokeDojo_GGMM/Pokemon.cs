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

        public List<CapaciteSpeciale> CapacitesSpeciales = new List<CapaciteSpeciale>();
        public List<AlterationEtat> AlterationsEtat = new List<AlterationEtat>();

        //le niveau d'évolution de 0 à 2 du pokémon
        public int Evolution { get; set; }
        public int KillCount { get; set; }

        private static Random Random = new Random();


        //Constructeur
        public Pokemon(string nom, int pV, int pA, char typeElementaire)//, int evolution)
        {
            Nom = nom;
            PV = pV;
            PA = pA;
            HistoriqueDegats = new List<int>();
            MarqueurDegats = 0;
                        
            if (pV < 76)
                Evolution = 0;
            else
            {
                if (pV < 151)
                    Evolution = 1;
                else
                    Evolution = 2;
            }

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
            foreach(AlterationEtat alteration in ennemi.AlterationsEtat)
            {
                if (alteration is AlterationEtatPA)
                {
                    AlterationEtatPA boost = (AlterationEtatPA)alteration;
                    if (boost.Duree == 0)
                    {
                        //boost.Modificateur = 0;
                        //!!pb de modification de taille de collection
                        ennemi.AlterationsEtat.Remove(alteration);
                    }
                        
                    else
                        alteration.Duree -= 1;

                    forceAttaque += boost.Modificateur;
                }
            }

            Random R = new Random();
            int Degats = (int)Math.Ceiling(
                (forceAttaque + R.Next(-20,20)) / (double)100 * ennemi.PA
                );

            //!! cas où les dégâts sont tellement réduits qu'ils pourraient soigner l'ennemi (100%-150% -> -50% * PA infligé en dégâts , soit Dégâts < 0 !)
            if (Degats < 0) Degats = 0;

            // gestion bouclier
            bool attaqueAbsorbée = false;

            foreach (AlterationEtat alteration in AlterationsEtat)
            {
                if(alteration is AlterationEtatBouclier)
                {
                    AlterationEtatBouclier bouclier = (AlterationEtatBouclier)alteration;
                    //AlterationEtatBouclier bouclier = new AlterationEtatBouclier(AlterationsEtat[indexAlteration].;
                    if (!attaqueAbsorbée)
                    {
                        if (bouclier.Duree == 0 || bouclier.PVBouclier < 0)
                        {
                            //bouclier.PVBouclier = -1;
                            //!!pb de modification de taille de collection
                            AlterationsEtat.Remove(bouclier);
                        }
                        else
                            alteration.Duree -= 1;

                        bouclier.PVBouclier -= Degats;
                        Degats -= bouclier.PVBouclier;

                        //!! est-ce que cette ligne est prise en compte ?
                        if (Degats > bouclier.PVBouclier)
                            Console.WriteLine("Le bouclier de {0} a été désintégré alors qu'il absorbait {1} points de dégâts.", ennemi.Nom,bouclier.PVBouclier);
                        else
                            Console.WriteLine("Le bouclier de {0} l'immunise en absorbant {1} points de dégâts.", ennemi.Nom,Degats);

                    }
                }

                if (Degats <= 0) attaqueAbsorbée = true;
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
                CapacitesSpeciales.Add(new CapaciteSpeciale(Nom, _types[TypeElementaire], new List<AlterationEtat> { new AlterationEtatBouclier(2, true, 20*(3-Evolution) + PV/(2+Evolution)) }));
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
            if (CapacitesSpeciales.Count() != 0)
            {
                //si le pouvoir est réflexif, il affecte le pokémon
                if (CapacitesSpeciales[capacite]._alterations[0]._reflexif)
                {
                    //!! Console.WriteLine(Nom + " lance " + CapacitesSpeciales[capacite] + " et bénéficie de : " + CapacitesSpeciales[capacite]._alterations[0]);
                    Console.WriteLine("{0}Nom lance {1} et bénéficie de : {2}", Nom, CapacitesSpeciales[capacite], CapacitesSpeciales[capacite]._alterations[0]);
                    AlterationsEtat.Add(CapacitesSpeciales[capacite]._alterations[0]);
                }

                //sinon il affecte le pokémon adverse
                else
                {
                    //!! Console.WriteLine(Nom + " lance " + CapacitesSpeciales[capacite] + " sur " + ennemi + " qui est affecté par " + CapacitesSpeciales[capacite]._alterations[0]);
                    Console.WriteLine("{0}Nom lance {1} sur {2} qui est affecté par : {3}", Nom, ennemi.Nom, CapacitesSpeciales[capacite], CapacitesSpeciales[capacite]._alterations[0]);
                    ennemi.AlterationsEtat.Add(CapacitesSpeciales[capacite]._alterations[0]);
                }
                CapacitesSpeciales.Remove(CapacitesSpeciales[capacite]);
            }

            //!!else
                //!!Console.WriteLine("Votre pokémon est à court de capacités !");
            
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
