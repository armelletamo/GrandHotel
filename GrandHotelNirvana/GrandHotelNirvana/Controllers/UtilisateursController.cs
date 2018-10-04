using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using GrandHotelNirvana.Models;

namespace GrandHotelNirvana
{
    [Route("api/[controller]")]
    public class UtilisateursController : Controller
    {
        Compte cpte = new Compte();

        [Route("create-user")]
        [HttpPost]
        public async void CreateUser(Utilisateur user)
        {
           await cpte.AjouterUtilisateur(user);
        }
        
    }
}
