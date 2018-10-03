using Microsoft.AspNetCore.Mvc;
using GrandHotelNirvana.Models;
using System;
using System.Net.Http;
using System.Net;

namespace GrandHotelNirvana
{
    public class UtilisateursController : Controller
    {
        Compte cpte = new Compte();
        [HttpPost]
        public void CreateUser(Utilisateur user, string role)
        {
            if (ModelState.IsValid)
            {
                // Do something with the product (not shown).
                cpte.AjouterUtilisateur(user.Civilite, user.Nom, user.Prenom, user.Email, user.MotDePasse, role);
                
            }
            else
            {
                
            }
           
        }
        
    }
}
