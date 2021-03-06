﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandHotelNirvana.Models;

namespace GrandHotelNirvana
{
    public class ClientService : IClientService
    {
        GrandHotelContext grandhotel = new GrandHotelContext();

        private bool alreadyDisposed = false;

        public async Task<bool> AjouterClient(ClientVM client)
        {
            Client clt = new Client();
            bool save = false;
            try
            {
                bool exist = grandhotel.Utilisateur.Any(x => x.Email == client.EmailUtilisateur);
                if (exist)
                {
                    bool emailexist = grandhotel.Email.Any(x => x.Adresse == client.EmailUtilisateur);
                    int id = grandhotel.Utilisateur
                        .Where(x => x.Email == client.EmailUtilisateur)
                        .Select(x => x.Id)
                        .FirstOrDefault();
                    if (!emailexist)
                    {
                        clt.CarteFidelite = client.CarteFidelite;
                        clt.Societe = client.Societe;
                        clt.UtilisateurId = id;
                        Adresse adresse = new Adresse();
                        adresse.Rue = client.Rue;
                        adresse.Complement = client.Complement;
                        adresse.CodePostal = client.CodePostal;
                        adresse.Ville = client.Ville;
                        clt.Adresse = adresse;
                        List<Telephone> tel = new List<Telephone>();
                        tel = client.Telephone;
                        clt.Telephone = tel;
                        Email mail = new Email();
                        mail.Adresse = client.EmailUtilisateur;
                        mail.Pro = client.EmailPro;
                        clt.Email.Add(mail);
                        grandhotel.Client.Add(clt);
                        await grandhotel.SaveChangesAsync();
                        save = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            await MiseAJourUtilisateur(client.EmailUtilisateur, clt.Id);
            return save;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<bool> MiseAJourUtilisateur(string email, int id)
        {
            bool done = false;
            try
            {
                var utilisateur = grandhotel.Utilisateur.Where(x => x.Email == email).FirstOrDefault();
                utilisateur.ClientId = id;
                grandhotel.Utilisateur.Update(utilisateur);
                await grandhotel.SaveChangesAsync();
                done = true;
            }
            catch (Exception ex)
            {

            }
            return done;
        }

        public async Task<bool> ModifierAdresse(Adresse adresse)
        {
            bool done = false;
            int idclient = 0;
            try
            {
                bool exist = grandhotel.Email.Any(x => x.Adresse == adresse.Email);
                if (exist)
                {
                    idclient = grandhotel.Email
                       .Where(x => x.Adresse == adresse.Email)
                       .Select(x => x.IdClient).FirstOrDefault();
                    var ancienneadresse = grandhotel.Adresse.Where(x => x.IdClient == idclient).FirstOrDefault();
                    ancienneadresse.IdClient = idclient;
                    ancienneadresse.Rue = adresse.Rue;
                    ancienneadresse.Ville = adresse.Ville;
                    ancienneadresse.Complement = adresse.Complement;
                    ancienneadresse.CodePostal = adresse.CodePostal;
                    grandhotel.Adresse.Update(ancienneadresse);
                    await grandhotel.SaveChangesAsync();
                    done = true;

                }
            }
            catch (Exception ex)
            {

            }
            return done;
        }

        public async Task<bool> ModifierTelephone(Telephone telephone)
        {
            bool done = false;
            int idclient = 0;
            try
            {
                bool exist = grandhotel.Email.Any(x => x.Adresse == telephone.Email);
                if (exist)
                {
                    idclient = grandhotel.Email
                       .Where(x => x.Adresse == telephone.Email)
                       .Select(x => x.IdClient).FirstOrDefault();
                    var anciennumero = grandhotel.Telephone.Where(x => x.Numero == telephone.AncienNumero && x.IdClient == idclient).FirstOrDefault();
                    anciennumero.Numero = telephone.Numero;
                    anciennumero.Pro = telephone.Pro;
                    anciennumero.CodeType = telephone.CodeType;
                    grandhotel.Telephone.Update(anciennumero);
                    await grandhotel.SaveChangesAsync();
                    done = true;

                }
            }
            catch (Exception ex)
            {

            }
            return done;
        }

        public bool SupprimerTelephhone(string telephone)
        {
            bool done = false;
            try
            {

                var Telephone =  grandhotel.Telephone.Where(x => x.Numero == telephone).FirstOrDefault();
                grandhotel.Telephone.Remove(Telephone);
                grandhotel.SaveChanges();
                done = true;
                     
            }
            catch (Exception ex)
            {

            }
            return done;
        }

        protected virtual void Dispose(bool isDisposing)
        {
            // Don't dispose more than once.
            if (alreadyDisposed)
                return;
            if (isDisposing)
            {
                // elided: free managed resources here.
            }
            alreadyDisposed = true;
        }

        
    }
}
