using Idt.Features.Core.Commandes.UtilisateursCommandes.Commands;
using Idt.Features.Interfaces.Repertoires;
using Idt.Features.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Core.Handlers.UtilisateursHandlers.CommandHandlers
{
    public class SupprimerUnUtilisateurCmdHandler : IRequestHandler<SupprimerUnUtilisateurCmd, ReponseDeRequette<bool>>
    {
        private readonly IPointDaccess _pointDAccess;
        public SupprimerUnUtilisateurCmdHandler(IPointDaccess pointDAccess)
        {
            _pointDAccess = pointDAccess;
        }
        public async Task<ReponseDeRequette<bool>> Handle(SupprimerUnUtilisateurCmd request, CancellationToken cancellationToken)
        {
            var response = new ReponseDeRequette<bool>();
            var utilisateurExist = await _pointDAccess.RepertoireDutilisateur.Exists(request.UtilisateurId);

            if (!utilisateurExist)
            {
                return new ReponseDeRequette<bool>
                {
                    Success = false,
                    Message = "Modification Echec",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
            var utilisateur = await _pointDAccess.RepertoireDutilisateur.Lire(request.UtilisateurId);
            var resultat = await _pointDAccess.RepertoireDutilisateur.Supprimer(utilisateur);

            if (resultat == true)
            {
                response.Success = true;
                response.Message = "Suppresion Réussit";
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Data = true;
            }
            else
            {
                response.Success = false;
                response.Message = $"Une Erreur Inconnu est Survenue dans le Serveur ";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return response;
        }
    }
}