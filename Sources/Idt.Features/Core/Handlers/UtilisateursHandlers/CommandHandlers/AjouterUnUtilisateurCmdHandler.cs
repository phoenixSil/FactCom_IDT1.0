
using AutoMapper;
using Idt.Domain;
using Idt.Features.Dtos.Utilisateurs.Validateurs;
using Idt.Features.Dtos.Utilisateurs;
using Idt.Features.Interfaces.Repertoires;
using Idt.Features.Responses;
using MediatR;
using System.Net;
using Idt.Features.Core.Commandes.UtilisateursCommandes.Commands;
using Idt.Features.Core.Utils;

namespace Idt.Features.Core.Handlers.UtilisateursHandlers.CommandHandlers
{
    public class AjouterUnUtilisateurCmdHandler : IRequestHandler<AjouterUnUtilisateurCmd, ReponseDeRequette<UtilisateurDto>>
    {
        private readonly IPointDaccess _pointDaccess;
        private readonly IMapper _mapper;

        public AjouterUnUtilisateurCmdHandler(IMapper mapper, IPointDaccess pointDaccess)
        {
            _pointDaccess = pointDaccess;
            _mapper = mapper;
        }
        public async Task<ReponseDeRequette<UtilisateurDto>> Handle(AjouterUnUtilisateurCmd request, CancellationToken cancellationToken)
        {
            var validateur = new ValidateurDeLaCreationDutilisateurDto();
            var resultatValidation = await validateur.ValidateAsync(request.UtilisateurDto, cancellationToken);

            if (!resultatValidation.IsValid)
            {
                return new ReponseDeRequette<UtilisateurDto>
                {
                    Success = false,
                    Message = "Echec de L'ajout d'un utilisateur",
                    Errors = resultatValidation.Errors.Select(q => q.ErrorMessage).ToList(),
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            var utilisateurACreer = _mapper.Map<Utilisateur>(request.UtilisateurDto);
            utilisateurACreer.Password = UtilisateurUtils.CrypterMotDePasse(utilisateurACreer.Password);
            var result = await _pointDaccess.RepertoireDutilisateur.Ajoutter(utilisateurACreer);
            await _pointDaccess.Enregistrer();

            if (result == null)
            {
                return new ReponseDeRequette<UtilisateurDto>
                {
                    Success = false,
                    Message = "Echec de L'ajout d'un utilisateur",
                    StatusCode = (int)HttpStatusCode.NoContent
                };
            }

            return new ReponseDeRequette<UtilisateurDto>
            {
                Success = true,
                Message = "Ajout d'utilisateur réussit",
                Data = _mapper.Map<UtilisateurDto>(utilisateurACreer),
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
