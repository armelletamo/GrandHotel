﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandHotelNirvana.Models
{
    public partial class Client
    {
        public Client()
        {
            Email = new HashSet<Email>();
            Facture = new HashSet<Facture>();
            Reservation = new HashSet<Reservation>();
            Telephone = new HashSet<Telephone>();
        }

        public int Id { get; set; }
        public bool CarteFidelite { get; set; }
        public string Societe { get; set; }
        public int UtilisateurId { get; set; }
        [NotMapped]
        [DisplayName("Email")]
        public string EmailUtilisateur { get; set; }

        public Utilisateur Utilisateur { get; set; }
        public Adresse Adresse { get; set; }
        public ICollection<Email> Email { get; set; }
        public ICollection<Facture> Facture { get; set; }
        public ICollection<Reservation> Reservation { get; set; }
        public ICollection<Telephone> Telephone { get; set; }
    }
}
