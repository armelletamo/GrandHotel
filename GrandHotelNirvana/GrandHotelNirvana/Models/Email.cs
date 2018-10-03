using System;
using System.Collections.Generic;

namespace GrandHotelNirvana.Models
{
    public partial class Email
    {
        public string Adresse { get; set; }
        public int IdClient { get; set; }
        public bool Pro { get; set; }

        public Client IdClientNavigation { get; set; }
    }
}
