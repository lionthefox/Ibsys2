using System;
using System.Collections.Generic;
using Ibsys2.Models;
using Ibsys2.Models.DispoEigenfertigung;
using Ibsys2.Models.ErgebnisseVorperiode;
using Ibsys2.Models.KapazitaetsPlan;
using Ibsys2.Models.Stammdaten;

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

        public IList<KapazitaetsPlan> clalcKapaPlan(DispoEigenfertigungen dispoEigenfertigungen,
            IList<Artikel> artikelStammdaten)
        {
            ResetKapaPlan();
            CalcKapaZeit(dispoEigenfertigungen.P1, artikelStammdaten);
            CalcKapaZeit(dispoEigenfertigungen.P2, artikelStammdaten);
            CalcKapaZeit(dispoEigenfertigungen.P3, artikelStammdaten);
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

            return KapazitaetsPlaene;
        }

        private void CalcKapaZeit(IList<DispoEFPos> dispoEf, IList<Artikel> artikelStammdaten)
        {
            foreach (var kapazitaetsPlan in KapazitaetsPlaene)
            {
                foreach (var efPos in dispoEf)
                {
                    if (efPos.Produktion > 0)
                    {
                        kapazitaetsPlan.KapaProduktion = kapazitaetsPlan.KapaProduktion + WartelisteArbeitsplatz.getZeitbedarf(
                            kapazitaetsPlan.ArbeitsplatzId, efPos.ArticleId,
                            efPos.Produktion, artikelStammdaten);
                        kapazitaetsPlan.RuestzeitProduktion = kapazitaetsPlan.RuestzeitProduktion + WartelisteArbeitsplatz.GetRuestzeit(
                            kapazitaetsPlan.ArbeitsplatzId, efPos.ArticleId,
                            artikelStammdaten);
                    }

                    if (efPos.AuftraegeBearbeitung > 0)
                    {
                        kapazitaetsPlan.KapafBearbeitung += WartelisteArbeitsplatz.getZeitbedarf(
                            kapazitaetsPlan.ArbeitsplatzId, efPos.ArticleId,
                            efPos.AuftraegeBearbeitung, artikelStammdaten);
                        kapazitaetsPlan.RuestzeitBearbeitung += WartelisteArbeitsplatz.GetRuestzeit(
                            kapazitaetsPlan.ArbeitsplatzId, efPos.ArticleId,
                            artikelStammdaten);
                    }

                    if (efPos.AuftraegeWarteschlange <= 0) continue;
                    kapazitaetsPlan.KapaWarteschlange += WartelisteArbeitsplatz.getZeitbedarf(
                        kapazitaetsPlan.ArbeitsplatzId, efPos.ArticleId,
                        efPos.AuftraegeBearbeitung, artikelStammdaten);
                    kapazitaetsPlan.RuestzeitWarteschlange += WartelisteArbeitsplatz.GetRuestzeit(
                        kapazitaetsPlan.ArbeitsplatzId, efPos.ArticleId,
                        artikelStammdaten);
                }
            }
        }

        private void ResetKapaPlan()
        {
            foreach (var kapazitaetsPlan in KapazitaetsPlaene)
            {
                kapazitaetsPlan.KapaProduktion = 0;
                kapazitaetsPlan.RuestzeitProduktion = 0;
                
                kapazitaetsPlan.KapafBearbeitung = 0;
                kapazitaetsPlan.RuestzeitBearbeitung = 0;
                
                kapazitaetsPlan.KapaWarteschlange = 0;
                kapazitaetsPlan.RuestzeitWarteschlange = 0;
            }
        }
    }
}