using AutoMapper;
using Idt.Features.Core.Commandes.AdressesCommandes.Commands;
using Idt.Features.Dtos.Adresses;
using Idt.Features.Dtos.Adresses.Validateurs;
using Idt.Features.Interfaces.Repertoires;
using Idt.Features.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Core.Handlers.AdressesHandlers.CommandHandlers
{
    public class ModifierUneAdresseCmdHandler : IRequestHandler<ModifierUneAdresseCmd, ReponseDeRequette<AdresseDto>>
    {
        private readonly IPointDaccess _pointDaccess;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ModifierUneAdresseCmdHandler(IPointDaccess pointDaccess, IMediator mediator, IMapper mapper)
        {
            _pointDaccess = pointDaccess;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ReponseDeRequette<AdresseDto>> Handle(ModifierUneAdresseCmd request, CancellationToken cancellationToken)
        {
            var reponse = new ReponseDeRequette<AdresseDto>();
            var adresse = await _pointDaccess.RepertoireDadresse.Lire(request.AdresseId);

            if (adresse is null)
            {
                return new ReponseDeRequette<AdresseDto>
                {
                    Success = false,
                    Message = "Modification Echec",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            if (request.AdresseDto is null)
            {
                return new ReponseDeRequette<AdresseDto>
                {
                    Success = false,
                    Message = "Echec de la Modification",
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }

            var validateur = new ValidateurDeLaModificationDadresseDto(_pointDaccess);
            var resultatValidation = await validateur.ValidateAsync(request.AdresseDto, cancellationToken);

            if (!await _pointDaccess.RepertoireDadresse.Exists(request.AdresseId))
            {
                return new ReponseDeRequette<AdresseDto>
                {
                    Success = false,
                    Message = "l'adresse a modifier nexiste pas !!!",
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }

            if (!resultatValidation.IsValid)
            {
                return new ReponseDeRequette<AdresseDto>
                {
                    Success = false,
                    Message = "Echec de la Modification",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Errors = resultatValidation.Errors.Select(q => q.ErrorMessage).ToList()
                };
            }

            _mapper.Map(request.AdresseDto, adresse);
            await _pointDaccess.RepertoireDadresse.Modifier(adresse);
            await _pointDaccess.Enregistrer();

            return new ReponseDeRequette<AdresseDto>
            {
                Success = true,
                Message = "ModificationReussit !!!",
                StatusCode = (int)HttpStatusCode.OK,
                Data = _mapper.Map<AdresseDto>(adresse)
            };
        }


    }
}