using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GrandHotelNirvana
{
    [Route("api/[controller]")]
    public class UtilisateursController : Controller
    {
        Compte cpte = new Compte();

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpPost]
        public void CreateUser([FromBody]string civilite, string nom, string prenom, string email, string motDePasse, string role)
        {
            cpte.AjouterUtilisateur(civilite, nom, prenom, email, motDePasse, role);
        }
        
    }
}
