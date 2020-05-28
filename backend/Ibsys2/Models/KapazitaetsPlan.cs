using System;
using Ibsys2.Models.Stammdaten;
using Ibsys2.Services;

namespace Ibsys2.Models.KapazitaetsPlan
{
    public class KapazitaetsPlan
    {
        public int ArbeitsplatzId { get; }
        public int KapaProduktion { get; set; }
        public int RuestzeitProduktion { get; set; }

        public int KapaVorperiode { get; set; }
        public int RuestVorperiode { get; set; }
        public int AnzSchicht { get; set; }
        public int Uebermin { get; set; }

        private readonly int SchichtDauer = 2400;
        private readonly int MaxUeberMinuten = 1200;

        public KapazitaetsPlan(int arbeitsplatzID)
        {
            ArbeitsplatzId = arbeitsplatzID;
        }

        public void CalcSchichten()
        {
            var kapaGesamt = CalcBearbGes() + CalcRusestGes();
            if (kapaGesamt <= SchichtDauer + MaxUeberMinuten)
            {
                AnzSchicht = 1;
                Uebermin = Math.Max(0, (kapaGesamt - SchichtDauer) / 5);
            }
            else if (kapaGesamt <= SchichtDauer * 2 + MaxUeberMinuten)
            {
                AnzSchicht = 2;
                Uebermin = Math.Max(0, (kapaGesamt - SchichtDauer * 2) / 5);
            }
            else
            {
                AnzSchicht = 3;
                Uebermin = 0;
            }
        }

        private int CalcBearbGes()
        {
            return KapaProduktion + KapaVorperiode;
        }

        private int CalcRusestGes()
        {
            return RuestzeitProduktion + RuestVorperiode;
        }
    }
}