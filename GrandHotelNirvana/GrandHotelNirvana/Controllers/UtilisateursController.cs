using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using GrandHotelNirvana.Models;

namespace GrandHotelNirvana
{
    [Route("api/[controller]")]
    public class UtilisateursController : Controller
    {
        CompteService cpte = new CompteService();

        [Route("create-user")]
        [HttpPost]
        public async void CreateUser(Utilisateur user)
        {
            if (ModelState.IsValid)
            {
                await cpte.AjouterUtilisateur(user);
            }
           
        }
        [Route("connect-user")]
        [HttpGet]
        public  Utilisateur ConnectUser(string email, string mdp)
        {
            
            return cpte.Authentifier(email, mdp);
        }

        [Route("reset-password")]
        [HttpPost]
        public async void ResetPassword(string email, [FromBody]string mdp)
        {
            if (ModelState.IsValid)
            {
                await cpte.ChangerMotDePasse( email,  mdp);
            }

        }
    }
}
