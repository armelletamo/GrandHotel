using GrandHotelNirvana.Models;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandHotelNirvana
{
    public class Reservations : IReservation
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

        public List<TarifChambre> ListeDeChambre(Reservation reservation)
        {
            DateTime d = DateTime.Now;

            IQueryable<Chambre> chambre;
            List<Chambre> chambrereserve = new List<Chambre>();
            List<Chambre> chambredispo = new List<Chambre>();
            List<TarifChambre> tarifchambre = new List<TarifChambre>();
            try
            {
                chambre = grandhotel.Chambre.Where(x => x.NbLits == reservation.NbPersonnes);
                if (grandhotel.Reservation.Any(x => x.Jour == reservation.Jour))
                {
                    chambrereserve = grandhotel.Reservation
                        .Include(x => x.NumChambreNavigation)
                        .Where(x => x.Jour == reservation.Jour)
                        .Select(x => x.NumChambreNavigation)
                        .Where(x => x.NbLits == reservation.NbPersonnes).ToList();
                }
                chambredispo = chambre.Except(chambrereserve).ToList();               

                 var Tarifchambre = from c in chambredispo
                                   join tf in grandhotel.TarifChambre on c.Numero equals tf.NumChambre
                                   join t in grandhotel.Tarif on tf.CodeTarif equals t.Code
                                   where tf.CodeTarif.Contains(reservation.Jour.Year.ToString())
                                   select tf;
                tarifchambre = Tarifchambre.ToList();



            }
            catch (Exception ex)
            {

            }
            
            return tarifchambre;

        }

        public Chambre DetailDeChambre(int numero)
        {
            Chambre chb = new Chambre();
            try
            {
                short num = (short)numero;
                chb = grandhotel.Chambre.Where(x => x.Numero == num).FirstOrDefault();
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
