using GrandHotelNirvana.Models;
using System;

namespace GrandHotelNirvana
{
    public interface ICompte : IDisposable
    {

        int AjouterUtilisateur(string civilite, string nom, string prenom, string email, string motDePasse, string role);
        Utilisateur Authentifier(string email, string motDePasse);
        Utilisateur ObtenirUtilisateur(int id);
    }
}
