using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandHotelNirvana.Models
{
    public partial class Utilisateur
    {
        public Utilisateur()
        {
            Client = new HashSet<Client>();
        }

        public int Id { get; set; }
        public string Civilite { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string MotDePasse { get; set; }
        public int RoleId { get; set; }
        public int? ClientId { get; set; }

        public Role Role { get; set; }
        public ICollection<Client> Client { get; set; }
    }
}
