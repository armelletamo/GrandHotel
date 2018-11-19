using System;
using System.Collections.Generic;

namespace GrandHotelNirvana.Models
{
    public partial class Reservation
    {
        public short NumChambre { get; set; }
        public DateTime Jour { get; set; }
        public int IdClient { get; set; }
        public byte NbPersonnes { get; set; }
        public byte HeureArrivee { get; set; }
        public bool? Travail { get; set; }
        public bool? PetitDejeuner { get; set; }
        public int Id { get; set; }

        public Client IdClientNavigation { get; set; }
        public Chambre NumChambreNavigation { get; set; }
    }
}
