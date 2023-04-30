using AutoMapper;
using Idt.Features.Core.Commandes.UtilisateursCommandes.Commands;
using Idt.Features.Dtos.Utilisateurs.Validateurs;
using Idt.Features.Dtos.Utilisateurs;
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
    public class ModifierUnUtilisateurCmdHandler : IRequestHandler<ModifierUnUtilisateurCmd, ReponseDeRequette<UtilisateurDto>>
    {
        private readonly IPointDaccess _pointDaccess;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ModifierUnUtilisateurCmdHandler(IPointDaccess pointDaccess, IMediator mediator, IMapper mapper)
        {
            _pointDaccess = pointDaccess;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ReponseDeRequette<UtilisateurDto>> Handle(ModifierUnUtilisateurCmd request, CancellationToken cancellationToken)
        {
            var reponse = new ReponseDeRequette<UtilisateurDto>();
            var utilisateur = await _pointDaccess.RepertoireDutilisateur.Lire(request.UtilisateurId);

            if (utilisateur is null)
            {
                return new ReponseDeRequette<UtilisateurDto>
                {
                    Success = false,
                    Message = "Modification Echec",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            if (request.UtilisateurDto is null)
            {
                return new ReponseDeRequette<UtilisateurDto>
                {
                    Success = false,
                    Message = "Echec de la Modification",
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }

            var validateur = new ValidateurDeLaModificationDutilisateur();
            var resultatValidation = await validateur.ValidateAsync(request.UtilisateurDto, cancellationToken);

            if (!await _pointDaccess.RepertoireDutilisateur.Exists(request.UtilisateurId))
            {
                return new ReponseDeRequette<UtilisateurDto>
                {
                    Success = false,
                    Message = "l'utilisateur a modifier nexiste pas !!!",
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }

            if (!resultatValidation.IsValid)
            {
                return new ReponseDeRequette<UtilisateurDto>
                {
                    Success = false,
                    Message = "Echec de la Modification",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Errors = resultatValidation.Errors.Select(q => q.ErrorMessage).ToList()
                };
            }

            _mapper.Map(request.UtilisateurDto, utilisateur);
            await _pointDaccess.RepertoireDutilisateur.Modifier(utilisateur);
            await _pointDaccess.Enregistrer();

            return new ReponseDeRequette<UtilisateurDto>
            {
                Success = true,
                Message = "ModificationReussit !!!",
                StatusCode = (int)HttpStatusCode.OK,
                Data = _mapper.Map<UtilisateurDto>(utilisateur)
            };
        }


    }
}