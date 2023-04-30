using Idt.Domain;
using Idt.Features.Core.Commandes.UtilisateursCommandes.Commands;
using Idt.Features.Core.Commandes.UtilisateursCommandes.Requettes;
using Idt.Features.Dtos.Utilisateurs;
using Idt.Features.Interfaces.Services;
using Idt.Features.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Application.Services
{
    public class ServiceDutilisateur : IServiceDutilisateur
    {
        private readonly IMediator _mediator;
        public ServiceDutilisateur(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<ReponseDeRequette<UtilisateurDto>> AjouterUnUtilisateur(UtilisateurACreerDto utilisateurDto)
        {
            var response = await _mediator.Send(new AjouterUnUtilisateurCmd { UtilisateurDto = utilisateurDto });
            return response;
        }

        public async Task<ReponseDeRequette<List<UtilisateurDto>>> LireTousLesUtilisateurs()
        {
            var response = await _mediator.Send(new LireTousLesUtilisateursCmd { });
            return response;
        }

        public async Task<ReponseDeRequette<UtilisateurDetailDto>> LireDetailDUnUtilisateur(Guid utilisateurId)
        {
            var response = await _mediator.Send(new LireDetailDunUtilisateurCmd { UtilisateurId = utilisateurId });
            return response;
        }



        public async Task<ReponseDeRequette<UtilisateurDto>> ModifierUnUtilisateur(Guid utilisateurId, UtilisateurAModifierDto utilisateurDto)
        {
            var response = await _mediator.Send(new ModifierUnUtilisateurCmd { UtilisateurId = utilisateurId, UtilisateurDto = utilisateurDto });
            return response;
        }


        public async Task<ReponseDeRequette<bool>> SupprimerUnUtilisateur(Guid utilisateurId)
        {
            var response = await _mediator.Send(new SupprimerUnUtilisateurCmd { UtilisateurId = utilisateurId });
            return response;
        }

        public async Task<ReponseDeRequette<LoggedUserDto>> ObtenirUtilisateurParMailEtPwd(LoggingUserDto userDto)
        {
            var response = await _mediator.Send(new ObtenirUtilisateurParEmalPwdCmd { Utilisateur = userDto });
            return response;
        }
    }
}
