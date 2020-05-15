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

        public int KapafBearbeitung { get; set; }
        public int RuestzeitBearbeitung { get; set; }
        public int KapaWarteschlange { get; set; }
        public int RuestzeitWarteschlange { get; set; }
        public int AnzSchicht { get; set; }
        public int Ubermin { get; set; }

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
                Ubermin = Math.Max(0, (kapaGesamt - SchichtDauer) / 5);
            }
            else if (kapaGesamt <= SchichtDauer * 2 + MaxUeberMinuten)
            {
                AnzSchicht = 2;
                Ubermin = Math.Max(0, (kapaGesamt - SchichtDauer * 2) / 5);
            }
            else
            {
                AnzSchicht = 3;
            }
        }

        private int CalcBearbGes()
        {
            return KapaProduktion + KapafBearbeitung + KapaWarteschlange;
        }

        private int CalcRusestGes()
        {
            return RuestzeitProduktion + RuestzeitBearbeitung + RuestzeitWarteschlange;
        }
    }
}