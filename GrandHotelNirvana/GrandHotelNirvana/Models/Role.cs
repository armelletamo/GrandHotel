using System;
using System.Collections.Generic;

namespace GrandHotelNirvana.Models
{
    public partial class Role
    {
        public Role()
        {
            Utilisateur = new HashSet<Utilisateur>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }

        public ICollection<Utilisateur> Utilisateur { get; set; }
    }
}
