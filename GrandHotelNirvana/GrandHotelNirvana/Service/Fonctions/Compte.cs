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

        public  async Task<int> AjouterUtilisateur(Utilisateur user)
        {
            int id = 0;
            try
            {
                bool exist = grandhotel.Utilisateur.Any(x => x.Email == user.Email);
                if (!exist)
                {
                    string motDePasseEncode = EncodeMD5(user.MotDePasse);
                    int roleid = grandhotel.Role.Where(x => x.Nom == user.roles).Select(x => x.Id).FirstOrDefault();
                    user.MotDePasse = motDePasseEncode;
                    user.RoleId = roleid;
                    grandhotel.Utilisateur.Add(user);
                    await grandhotel.SaveChangesAsync();

                }
                else
                {
                    return id;
                }
                
            }
            catch(Exception ex)
            {
                
            }
             id =  user.Id;
            return id ;
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
