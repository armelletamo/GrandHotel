using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GrandHotelNirvana.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        [Required]
        public string Civilite { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        public string MotDePasse { get; set; }
        
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        
        public Role Role { get; set; }
        
    }
}
