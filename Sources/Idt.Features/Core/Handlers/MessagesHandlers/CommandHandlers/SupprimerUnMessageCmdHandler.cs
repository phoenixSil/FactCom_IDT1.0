using Idt.Features.Core.Commandes.MessagesCommandes.Commands;
using Idt.Features.Interfaces.Repertoires;
using Idt.Features.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Core.Handlers.MessagesHandlers.CommandHandlers
{
    public class SupprimerUnMessageCmdHandler : IRequestHandler<SupprimerUnMessageCmd, ReponseDeRequette<bool>>
    {
        private readonly IPointDaccess _pointDAccess;
        public SupprimerUnMessageCmdHandler(IPointDaccess pointDAccess)
        {
            _pointDAccess = pointDAccess;
        }
        public async Task<ReponseDeRequette<bool>> Handle(SupprimerUnMessageCmd request, CancellationToken cancellationToken)
        {
            var response = new ReponseDeRequette<bool>();
            var messageExist = await _pointDAccess.RepertoireDemessage.Exists(request.MessageId);

            if (!messageExist)
            {
                return new ReponseDeRequette<bool>
                {
                    Success = false,
                    Message = "Modification Echec",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
            var message = await _pointDAccess.RepertoireDemessage.Lire(request.MessageId);
            var resultat = await _pointDAccess.RepertoireDemessage.Supprimer(message);

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