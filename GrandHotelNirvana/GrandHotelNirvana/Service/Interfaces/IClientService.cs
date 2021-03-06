﻿using GrandHotelNirvana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandHotelNirvana
{
    public interface IClientService : IDisposable
    {
        Task<bool> AjouterClient(ClientVM client);

        Task<bool> ModifierAdresse(Adresse adresse);

        Task<bool> ModifierTelephone(Telephone telephone);

        Task<bool> MiseAJourUtilisateur(string email, int id);

        bool SupprimerTelephhone(string telephone);


    }
}
