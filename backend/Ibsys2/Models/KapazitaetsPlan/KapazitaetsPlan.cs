using System;
using Ibsys2.Models.Stammdaten;
using Ibsys2.Services;

namespace Ibsys2.Models.KapazitaetsPlan
{
    public class KapazitaetsPlan
    {
        public int ArbeitsplatzID { get; }
        public int KapaBedarf { get; set; }
        public int Ruestzeit { get; set; }
        public int KapaBedarfVorperiode { get; set; }
        public int RuestzeitVorperiode { get; set; }
        public int AnzSchicht { get; set; }
        public int Ubermin { get; set; }

        private readonly int SchichtDauer = 2400;
        private readonly int MaxUeberMinuten = 1200;

        public KapazitaetsPlan(int arbeitsplatzID)
        {
            ArbeitsplatzID = arbeitsplatzID;
        }

        public void calcSchiten()
        {
            int GesamtKapa = KapaBedarf + Ruestzeit + KapaBedarfVorperiode + RuestzeitVorperiode;
            if (GesamtKapa <= SchichtDauer + MaxUeberMinuten)
            {
                AnzSchicht = 1;
                Ubermin = Math.Max(0, (GesamtKapa - SchichtDauer) / 5);
            }
            else if (GesamtKapa <= SchichtDauer * 2 + MaxUeberMinuten)
            {
                AnzSchicht = 2;
                Ubermin = Math.Max(0, (GesamtKapa - SchichtDauer * 2) / 5);
            }
            else
            {
                AnzSchicht = 3;
            }
        }
    }
}