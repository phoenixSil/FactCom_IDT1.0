using Idt.Features.Dtos.Utilisateurs;
using Idt.Features.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Interfaces.Services
{
    public interface IServiceDutilisateur
    {
        Task<ReponseDeRequette<List<UtilisateurDto>>> LireTousLesUtilisateurs();
        Task<ReponseDeRequette<UtilisateurDetailDto>> LireDetailDUnUtilisateur(Guid id);
        Task<ReponseDeRequette<UtilisateurDto>> ModifierUnUtilisateur(Guid utilisateurId, UtilisateurAModifierDto utilisateurDto);
        Task<ReponseDeRequette<UtilisateurDto>> AjouterUnUtilisateur(UtilisateurACreerDto utilisateurDto);
        Task<ReponseDeRequette<bool>> SupprimerUnUtilisateur(Guid utilisateurId);
        Task<ReponseDeRequette<LoggedUserDto>> ObtenirUtilisateurParMailEtPwd(LoggingUserDto userDto);
    }
}
