using GrandHotelNirvana.Models;
using System;
using System.Threading.Tasks;

namespace GrandHotelNirvana
{
    public interface ICompteService : IDisposable
    {

         Task<int> AjouterUtilisateur(Utilisateur user);
        Utilisateur Authentifier(string email, string motDePasse);
        Utilisateur ObtenirUtilisateur(int id);
    }
}
