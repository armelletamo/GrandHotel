using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GrandHotelNirvana.Models;

namespace GrandHotelNirvana
{
    public class Compte : ICompte
    {
        GrandHotelContext grandhotel = new GrandHotelContext();
        Utilisateur newUser = new Utilisateur();
        private bool alreadyDisposed = false;

        public int AjouterUtilisateur(string civilite, string nom, string prenom, string email, string motDePasse, string role)
        {
            try
            {
                string motDePasseEncode = EncodeMD5(motDePasse);
                int roleid = grandhotel.Role.Where(x => x.Nom == role).Select(x => x.Id).FirstOrDefault();

                newUser.Civilite = civilite;
                newUser.Nom = nom;
                newUser.Prenom = prenom;
                newUser.Email = email;
                newUser.MotDePasse = motDePasseEncode;
                newUser.RoleId = roleid;
                grandhotel.Utilisateur.Add(newUser);
                grandhotel.SaveChanges();
            }
            catch(Exception ex)
            {
                
            }
            
            return newUser.Id;
        }

        private string EncodeMD5(string motDePasse)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)    
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(motDePasse);
            encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string    
            return BitConverter.ToString(encodedBytes);
        }

        public Utilisateur Authentifier(string email, string motDePasse)
        {
            
            try
            {
                string motDePasseEncode = EncodeMD5(motDePasse);
                newUser= grandhotel.Utilisateur.FirstOrDefault(u => u.Email == email && u.MotDePasse == motDePasseEncode);
            }
            catch(Exception ex)
            {
                newUser = null;
            }
            return newUser;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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

        public Utilisateur ObtenirUtilisateur(int id)
        {
            return grandhotel.Utilisateur.FirstOrDefault(u => u.Id == id);
        }

        
    }
}
