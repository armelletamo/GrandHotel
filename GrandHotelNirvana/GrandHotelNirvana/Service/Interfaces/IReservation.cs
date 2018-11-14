using GrandHotelNirvana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandHotelNirvana
{
    public interface IReservation :IDisposable
    {
        List<TarifChambre> ListeDeChambre(Reservation reservation);
        Chambre DetailDeChambre(int id);
        Task<int> AjouterReservation(Reservation reserv);
    }
}
