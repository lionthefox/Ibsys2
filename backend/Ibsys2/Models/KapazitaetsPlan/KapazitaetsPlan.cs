using Ibsys2.Models.Stammdaten;
using Ibsys2.Services;

namespace Ibsys2.Models.KapazitaetsPlan
{
    public class KapazitaetsPlan
    {

        private Arbeitsplatz Arbeitsplatz;
        private int KapaBedarf;
        private int Ruestzeit;
        private int KapaBedarfVorperiode;
        private int RuestzeitVorperiode;
        
        
        public KapazitaetsPlan(Arbeitsplatz arbeitsplatz)
        {
            
        }

    }
}