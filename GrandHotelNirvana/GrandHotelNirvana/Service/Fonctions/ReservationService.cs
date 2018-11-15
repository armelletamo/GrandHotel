using GrandHotelNirvana.Models;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandHotelNirvana
{
    public class ReservationService : IReservationService
    {
        GrandHotelContext grandhotel = new GrandHotelContext();

        private bool alreadyDisposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            // Don't dispose more than once.
            if (alreadyDisposed)
                return;
            if (isDisposing)
            {
                // elided: free managed resources here.
            }
            alreadyDisposed = true;
        }

        public List<ChambreVM> ListeDeChambre(Reservation reservation)
        {
            IQueryable<Chambre> chambre;
            IQueryable<Chambre> chambrereserve=null;
            List<Chambre> chambredispo = new List<Chambre>();
            List<TarifChambre> tarifchambre = new List<TarifChambre>();
            List<ChambreVM> ChambreEtPrix = new List<ChambreVM>();

            try
            {
                chambre = grandhotel.Chambre.Where(x => x.NbLits == reservation.NbPersonnes);
                if (grandhotel.Reservation.Any(x => x.Jour == reservation.Jour))
                {
                    chambrereserve = grandhotel.Reservation
                        .Include(x => x.NumChambreNavigation)
                        .Where(x => x.Jour == reservation.Jour)
                        .Select(x => x.NumChambreNavigation)
                        .Where(x => x.NbLits == reservation.NbPersonnes);
                }
                if(chambrereserve!=null)
                    chambredispo = chambre.Except(chambrereserve).ToList(); 
                else
                    chambredispo = chambre.ToList();

                var Tarifchambre = from c in chambredispo
                                   join tf in grandhotel.TarifChambre on c.Numero equals tf.NumChambre
                                   join t in grandhotel.Tarif on tf.CodeTarif equals t.Code
                                   where tf.CodeTarif.Contains(reservation.Jour.Year.ToString())
                                   select tf;

                tarifchambre = Tarifchambre.ToList();
                foreach(var chb in tarifchambre)
                {
                    ChambreVM c = new ChambreVM();
                    c.Bain = chb.NumChambreNavigation.Bain;
                    c.Douche= chb.NumChambreNavigation.Douche;
                    c.Etage= chb.NumChambreNavigation.Etage;
                    c.NbLits= chb.NumChambreNavigation.NbLits;
                    c.Numero= chb.NumChambreNavigation.Numero;
                    c.NumTel= chb.NumChambreNavigation.NumTel;
                    c.Wc= chb.NumChambreNavigation.Wc;
                    c.Prix= chb.CodeTarifNavigation.Prix;
                    ChambreEtPrix.Add(c);
                }
            }
            catch (Exception ex)
            {

            }
            
            return ChambreEtPrix;

        }

        public ChambreVM DetailDeChambre(int numero)
        {
            ChambreVM chb = new ChambreVM();
            try
            {
                Chambre chambre = new Chambre();
                short num = (short)numero;
                chambre = grandhotel.Chambre.Where(x => x.Numero == num).FirstOrDefault();
                chb.Numero = chambre.Numero;
                chb.Bain = chambre.Bain;
                chb.Douche = chambre.Douche;
                chb.Etage = chambre.Etage;
                chb.NbLits = chambre.NbLits;
                chb.NumTel = chambre.NumTel;
                chb.Wc = chambre.Wc;
            }
            catch (Exception ex)
            {

            }
            return chb;
        }

        public async Task<int> AjouterReservation(Reservation reserv)
        {
            int i = 0;
            try
            {
                grandhotel.Reservation.Add(reserv);
                await grandhotel.SaveChangesAsync();
                i = 1;
            }
            catch (Exception ex)
            {

            }
            return i;
        }

       
    }
}
