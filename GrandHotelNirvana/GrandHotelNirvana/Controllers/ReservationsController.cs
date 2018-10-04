using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandHotelNirvana.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrandHotelNirvana.Controllers
{
    

    [Produces("application/json")]
    [Route("api/Reservations")]
    public class ReservationsController : Controller
    {
        Reservations reservations = new Reservations();
        List<Chambre> ChambreLibre = new List<Chambre>();
        Chambre chb = new Chambre();

        [Route("Available-Rooms")]
        [HttpGet]
        public List<Chambre> ListOfRoom(Reservation reserv)
        {
            if (ModelState.IsValid)
            {
                 ChambreLibre= reservations.ListeDeChambre(reserv);
            }
            return ChambreLibre;
        }

        // GET: api/Reservations/5
        [Route("Details-Chambre")]
        [HttpGet]
        public Chambre DetailChambre(int numero)
        {
            chb = reservations.DetailDeChambre(numero);
            return chb;
        }

        // POST: api/Reservations
        [Route("Save-Reservation")]
        [HttpPost]
        public async Task<int> SaveReservation(Reservation reserv)
        {
            int i=0;
            if (ModelState.IsValid)
            {
                i = await reservations.AjouterReservation(reserv);
            }
            return i;
        }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
