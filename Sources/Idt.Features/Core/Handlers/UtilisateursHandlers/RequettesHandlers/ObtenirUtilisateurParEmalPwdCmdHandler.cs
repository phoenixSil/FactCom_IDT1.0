using AutoMapper;
using Idt.Domain;
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
using Idt.Features.Core.Commandes.UtilisateursCommandes.Requettes;
using Idt.Features.Core.Utils;

namespace Idt.Features.Core.Handlers.UtilisateursHandlers.RequettesHandlers
{
    public class ObtenirUtilisateurParEmalPwdCmdHandler : IRequestHandler<ObtenirUtilisateurParEmalPwdCmd, ReponseDeRequette<LoggedUserDto>>
    {
        private readonly IPointDaccess _pointDaccess;
        private readonly IMapper _mapper;

        public ObtenirUtilisateurParEmalPwdCmdHandler(IMapper mapper, IPointDaccess pointDaccess)
        {
            _pointDaccess = pointDaccess;
            _mapper = mapper;
        }
        public async Task<ReponseDeRequette<LoggedUserDto>> Handle(ObtenirUtilisateurParEmalPwdCmd request, CancellationToken cancellationToken)
        {
            var validateur = new ValidateurDeLaRecuperationDunUtilisateur();
            var resultatValidation = await validateur.ValidateAsync(request.Utilisateur, cancellationToken);

            if (!resultatValidation.IsValid)
            {
                return new ReponseDeRequette<LoggedUserDto>
                {
                    Success = false,
                    Message = "Echec lors de la lecture de lutilisateur ",
                    Errors = resultatValidation.Errors.Select(q => q.ErrorMessage).ToList(),
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            var result = await _pointDaccess.RepertoireDutilisateur.LireParEmailEtMotDePasse(request.Utilisateur.Email, request.Utilisateur.Password);

            if (result == null)
            {
                return new ReponseDeRequette<LoggedUserDto>
                {
                    Success = false,
                    Message = "Erreur suvenue pendant la recuperation ",
                    StatusCode = (int)HttpStatusCode.NoContent
                };
            }

            var loggedUSerDto = _mapper.Map<LoggedUserDto>(result);
            loggedUSerDto.Token = UtilisateurUtils.GenerateUserToken(result);

            return new ReponseDeRequette<LoggedUserDto>
            {
                Success = true,
                Message = "Utilisateur Trouvé",
                Data = loggedUSerDto,
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
