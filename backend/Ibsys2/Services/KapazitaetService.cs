using System;
using System.Collections.Generic;
using Ibsys2.Models;
using Ibsys2.Models.DispoEigenfertigung;
using Ibsys2.Models.ErgebnisseVorperiode;
using Ibsys2.Models.KapazitaetsPlan;

namespace Ibsys2.Services
{
    public class KapazitaetService
    {
        private DispoEFP1 dispoEfp1;
        private DispoEFP2 dispoEfp2;
        private DispoEFP3 dispoEfp3;
        private List<KapazitaetsPlan> KapazitaetsPlaene = new List<KapazitaetsPlan>();

        public KapazitaetService()
        {
            for (int i = 1; i < 16; i++)
            {
                if (i == 5)
                    continue;
                KapazitaetsPlaene.Add(new KapazitaetsPlan(i));
            }
        }

        public void clalcKapaPlan(DispoEigenfertigungen dispoEigenfertigungen, IList<Artikel> artikelStammdaten)
        {
            resetKapaPlan();
            calcKapaZeit(dispoEigenfertigungen.P1, artikelStammdaten);
            calcKapaZeit(dispoEigenfertigungen.P2, artikelStammdaten);
            calcKapaZeit(dispoEigenfertigungen.P3, artikelStammdaten);
            foreach (var kapazitaetsPlan in KapazitaetsPlaene)
            {
                kapazitaetsPlan.calcSchiten();
                // Console.Out.WriteLine("Arbeitsplatz: " + kapazitaetsPlan.ArbeitsplatzID + " MaschZeit " +
                // kapazitaetsPlan.KapaBedarf);
                // Console.Out.WriteLine("Arbeitsplatz: " + kapazitaetsPlan.ArbeitsplatzID + " Rüstzeit " +
                // kapazitaetsPlan.Ruestzeit);
                // Console.Out.WriteLine("Schichten: " + kapazitaetsPlan.AnzSchicht + " Überstunden " +
                // kapazitaetsPlan.Ubermin);
            }
        }

        private void calcKapaZeit(IList<DispoEFPos> dispoEf, IList<Artikel> artikelStammdaten)
        {
            foreach (var kapazitaetsPlan in KapazitaetsPlaene)
            {
                foreach (var efPos in dispoEf)
                {
                    if (efPos.Produktion == 0)
                        continue;
                    kapazitaetsPlan.KapaBedarf = kapazitaetsPlan.KapaBedarf + WartelisteArbeitsplatz.getZeitbedarf(
                        kapazitaetsPlan.ArbeitsplatzID, efPos.ArticleId,
                        efPos.Produktion, artikelStammdaten);
                    kapazitaetsPlan.Ruestzeit = kapazitaetsPlan.Ruestzeit + WartelisteArbeitsplatz.GetRuestzeit(
                        kapazitaetsPlan.ArbeitsplatzID, efPos.ArticleId, artikelStammdaten);
                }
            }
        }

        private void resetKapaPlan()
        {
            foreach (var kapazitaetsPlan in KapazitaetsPlaene)
            {
                kapazitaetsPlan.KapaBedarf = 0;
                kapazitaetsPlan.Ruestzeit = 0;
                kapazitaetsPlan.KapaBedarfVorperiode = 0;
                kapazitaetsPlan.RuestzeitVorperiode = 0;
            }
        }
    }
}