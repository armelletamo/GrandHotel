using System;
using System.Collections.Generic;

namespace GrandHotelNirvana.Models
{
    public partial class Calendrier
    {
        public Calendrier()
        {
            Reservation = new HashSet<Reservation>();
        }

        public DateTime Jour { get; set; }

        public ICollection<Reservation> Reservation { get; set; }
    }
}
