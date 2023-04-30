using Idt.Domain;
using Idt.Features.Dtos.Utilisateurs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Interfaces.Repertoires
{
    public interface IRepertoireDutilisateur : IRepertoireGenerique<Utilisateur>
    {
        Task<Utilisateur?> LireDetailDunUtilisateur(Guid utilisateurId);
        Task<Utilisateur?> LireParEmailEtMotDePasse(string email, string password);
    }
}
