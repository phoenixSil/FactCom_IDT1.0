using Idt.Features.Core.Commandes.AdressesCommandes.Commands;
using Idt.Features.Interfaces.Repertoires;
using Idt.Features.Responses;
using MediatR;
using System.Net;

namespace Idt.Features.Core.Handlers.AdressesHandlers.CommandHandlers
{
    public class SupprimerUneAdresseCmdHandler : IRequestHandler<SupprimerUneAdresseCmd, ReponseDeRequette<bool>>
    {
        private readonly IPointDaccess _pointDAccess;
        public SupprimerUneAdresseCmdHandler(IPointDaccess pointDAccess)
        {
            _pointDAccess = pointDAccess;
        }
        public async Task<ReponseDeRequette<bool>> Handle(SupprimerUneAdresseCmd request, CancellationToken cancellationToken)
        {
            var response = new ReponseDeRequette<bool>();
            var adresseExist = await _pointDAccess.RepertoireDadresse.Exists(request.AdresseId);

            if (adresseExist)
            {
                return new ReponseDeRequette<bool>
                {
                    Success = false,
                    Message = "Modification Echec",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
            var adresse = await _pointDAccess.RepertoireDadresse.Lire(request.AdresseId);
            var resultat = await _pointDAccess.RepertoireDadresse.Supprimer(adresse);

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