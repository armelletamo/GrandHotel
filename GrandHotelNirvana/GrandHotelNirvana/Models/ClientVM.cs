using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandHotelNirvana.Models
{
    public class ClientVM
    {
        public bool CarteFidelite { get; set; }
        public string Societe { get; set; }
        public int UtilisateurId { get; set; }
        public bool EmailPro { get; set; }
        public string EmailUtilisateur { get; set; }
        public string Rue { get; set; }
        public string Complement { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public bool TelPro { get; set; }
        public string CodeType { get; set; }
        public string NumeroTel { get; set; }
    }
}
