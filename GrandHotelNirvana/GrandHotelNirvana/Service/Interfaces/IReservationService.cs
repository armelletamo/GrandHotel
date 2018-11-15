using GrandHotelNirvana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandHotelNirvana
{
    public interface IReservationService : IDisposable
    {
        List<ChambreVM> ListeDeChambre(Reservation reservation);
        ChambreVM DetailDeChambre(int id);
        Task<int> AjouterReservation(Reservation reserv);
    }
}
