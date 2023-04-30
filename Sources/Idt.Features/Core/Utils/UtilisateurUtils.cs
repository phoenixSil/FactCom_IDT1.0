using Idt.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Core.Utils
{
    public static class UtilisateurUtils
    {
        public static string GenerateUserToken(Utilisateur utilisateur)
        {
            return $"{utilisateur.Nom} - {utilisateur.Prenom}";
        }

        public static string CrypterMotDePasse(string motDePasse)
        {
            // Convertir le mot de passe en tableau de bytes
            byte[] bytesMotDePasse = Encoding.UTF8.GetBytes(motDePasse);

            // Créer un objet SHA256
            SHA256 sha256 = SHA256.Create();

            // Calculer le hachage du mot de passe
            byte[] hashMotDePasse = sha256.ComputeHash(bytesMotDePasse);

            // Convertir le hachage en string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashMotDePasse.Length; i++)
            {
                sb.Append(hashMotDePasse[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public static bool VerifierMotDePasse(string motDePasse, string hashMotDePasse)
        {
            // Crypter le mot de passe à vérifier
            string hashMotDePasseAVerifier = CrypterMotDePasse(motDePasse);

            // Comparer les deux hash
            return hashMotDePasseAVerifier == hashMotDePasse;
        }

        public static string CrypterLeMotDePassePourVErification(this string motDePasse)
        {
            return CrypterMotDePasse(motDePasse);
        }
    }
}
