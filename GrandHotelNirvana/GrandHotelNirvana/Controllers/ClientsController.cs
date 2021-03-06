﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandHotelNirvana.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrandHotelNirvana.Controllers
{
    [Produces("application/json")]
    [Route("api/Clients")]
    public class ClientsController : Controller
    {
        ClientService clientserv = new ClientService();
        // POST: api/Clients
        [Route("Add-Client")]
        [HttpPost]
        public async Task<bool> AjouterClient([FromBody]ClientVM client)
        {
            bool done = false;

            if (ModelState.IsValid)
            {
                done = await clientserv.AjouterClient(client);
            }
            return done;
        }

        [Route("Modify-ClientAdress")]
        [HttpPost]
        public async Task<bool> ModifierAdresse([FromBody]Adresse adresse)
        { 
            bool done = false;

            if (ModelState.IsValid)
            {
                done = await clientserv.ModifierAdresse(adresse);
            }
            return done;
        }

        [Route("Modify-PhoneNumber")]
        [HttpPost]
        public async Task<bool> ModifierNumerotel([FromBody]Telephone telephone)
        {
            bool done = false;

            if (ModelState.IsValid)
            {
                done = await clientserv.ModifierTelephone(telephone);
            }
            return done;
        }

        [Route("Delete-PhoneNumber")]
        [HttpPost]
        public bool SupprimerTelephone(string telephone)
        {
            bool done = false;

            if (ModelState.IsValid)
            {
                done =  clientserv.SupprimerTelephhone(telephone);
            }
            return done;
        }
    }
}
