using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandHotelNirvana.Models
{
    public partial class Telephone
    {
        public int Id { get; set; }
        [NotMapped]
        public string AncienNumero { get; set; }
        [NotMapped]
        public string Email { get; set; }
        public string Numero { get; set; }        
        public int IdClient { get; set; }
        public string CodeType { get; set; }
        public bool Pro { get; set; }

        public Client IdClientNavigation { get; set; }
    }
}
