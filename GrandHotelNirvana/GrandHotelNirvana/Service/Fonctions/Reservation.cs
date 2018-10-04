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

        public List<Chambre> ListeDeChambre(Reservation reservation)
        {
            List<Chambre> chambre = new List<Chambre>();
            List<Chambre> chambrereserve = new List<Chambre>();
            List<Chambre> chambredispo = new List<Chambre>();
            try
            {
                chambre = grandhotel.Chambre.Where(x => x.NbLits == reservation.NbPersonnes).ToList();
                if (grandhotel.Reservation.Any(x => x.Jour == reservation.Jour))
                {
                    chambrereserve = grandhotel.Reservation
                        .Include(x => x.NumChambreNavigation)
                        .Where(x => x.Jour == reservation.Jour)
                        .Select(x => x.NumChambreNavigation)
                        .Where(x=>x.NbLits==reservation.NbPersonnes)
                        .ToList();
                }
                chambredispo = chambre.Except(chambrereserve).ToList();
            }
            catch(Exception ex)
            {

            }
            
            return chambredispo;
        }

        public Chambre DetailDeChambre(int numero)
        {
            Chambre chb = new Chambre();
            try
            {
                short num = (short)numero;
                chb = grandhotel.Chambre.Where(x => x.Numero == num).FirstOrDefault();
            }
            catch(Exception ex)
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
            catch(Exception ex)
            {

            }
           return i;
        }

       
    }
}
