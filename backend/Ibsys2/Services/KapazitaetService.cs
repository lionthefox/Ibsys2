using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Ibsys2.Models;
using Ibsys2.Models.DispoEigenfertigung;
using Ibsys2.Models.ErgebnisseVorperiode;
using Ibsys2.Models.Stueckliste;

namespace Ibsys2.Services
{
    public class KapazitaetService
    {
        private DispoEFP1 dispoEfp1;
        public KapazitaetService(DispoEfService dispoEfService, IList<Artikel> artikelStammdaten)
        {
            dispoEfp1 = dispoEfService.DispoEfp1;
            DispoEFP2 dispoEfp2 = dispoEfService.DispoEfp2;
            DispoEFP3 dispoEfp3 = dispoEfService.DispoEfP3;
        }

        public void clalcKapaPlam()
        {
            foreach (var efPos in dispoEfp1.ListDispoEfPos)
            {
                Console.Out.WriteLine(efPos.Produktion.ToString());
            }
            
        }
        
    }
}