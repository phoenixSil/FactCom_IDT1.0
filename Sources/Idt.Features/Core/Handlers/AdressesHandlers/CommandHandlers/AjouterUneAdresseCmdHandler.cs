using AutoMapper;
using Idt.Domain;
using Idt.Features.Core.Commandes.AdressesCommandes.Commands;
using Idt.Features.Dtos.Adresses;
using Idt.Features.Dtos.Adresses.Validateurs;
using Idt.Features.Interfaces.Repertoires;
using Idt.Features.Responses;
using MediatR;
using System.Net;

namespace Idt.Features.Core.Handlers.AdressesHandlers.CommandHandlers
{
    public class AjouterUneAdresseCmdHandler : IRequestHandler<AjouterUneAdresseCmd, ReponseDeRequette<AdresseDto>>
    {
        private readonly IPointDaccess _pointDaccess;
        private readonly IMapper _mapper;

        public AjouterUneAdresseCmdHandler(IMapper mapper, IPointDaccess pointDaccess)
        {
            _pointDaccess = pointDaccess;
            _mapper = mapper;
        }
        public async Task<ReponseDeRequette<AdresseDto>> Handle(AjouterUneAdresseCmd request, CancellationToken cancellationToken)
        {
            var validateur = new ValidateurDeLaCreationDadresseDto(_pointDaccess);
            var resultatValidation = await validateur.ValidateAsync(request.AdresseDto, cancellationToken);

            if (!resultatValidation.IsValid)
            {
                return new ReponseDeRequette<AdresseDto>
                {
                    Success = false,
                    Message = "Echec de L'ajout d'une adresse",
                    Errors = resultatValidation.Errors.Select(q => q.ErrorMessage).ToList(),
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            var adresseACreer = _mapper.Map<Adresse>(request.AdresseDto);
            var result = await _pointDaccess.RepertoireDadresse.Ajoutter(adresseACreer);
            await _pointDaccess.Enregistrer();

            if (result == null)
            {
                return new ReponseDeRequette<AdresseDto>
                {
                    Success = false,
                    Message = "Echec de L'ajout d'une adresse",
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

            return new ReponseDeRequette<AdresseDto>
            {
                Success = true,
                Message = "Ajout d'adresse réussit",
                Data = _mapper.Map<AdresseDto>(adresseACreer),
                StatusCode = (int)HttpStatusCode.OK
            };
        }

    }
}
