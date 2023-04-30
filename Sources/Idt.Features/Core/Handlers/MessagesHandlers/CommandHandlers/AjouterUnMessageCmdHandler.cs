
using AutoMapper;
using Idt.Domain;
using Idt.Features.Dtos.Messages.Validateurs;
using Idt.Features.Dtos.Messages;
using Idt.Features.Interfaces.Repertoires;
using Idt.Features.Responses;
using MediatR;
using System.Net;
using Idt.Features.Core.Commandes.MessagesCommandes.Commands;

namespace Idt.Features.Core.Handlers.MessagesHandlers.CommandHandlers
{
    public class AjouterUnMessageCmdHandler : IRequestHandler<AjouterUnMessageCmd, ReponseDeRequette<MessageDto>>
    {
        private readonly IPointDaccess _pointDaccess;
        private readonly IMapper _mapper;

        public AjouterUnMessageCmdHandler(IMapper mapper, IPointDaccess pointDaccess)
        {
            _pointDaccess = pointDaccess;
            _mapper = mapper;
        }
        public async Task<ReponseDeRequette<MessageDto>> Handle(AjouterUnMessageCmd request, CancellationToken cancellationToken)
        {
            var validateur = new ValidateurDeLaCreationDemessageDto(_pointDaccess);
            var resultatValidation = await validateur.ValidateAsync(request.MessageDto, cancellationToken);

            if (!resultatValidation.IsValid)
            {
                return new ReponseDeRequette<MessageDto>
                {
                    Success = false,
                    Message = "Echec de L'ajout d'un message",
                    Errors = resultatValidation.Errors.Select(q => q.ErrorMessage).ToList(),
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            var messageACreer = _mapper.Map<Message>(request.MessageDto);
            var result = await _pointDaccess.RepertoireDemessage.Ajoutter(messageACreer);
            await _pointDaccess.Enregistrer();

            if (result == null)
            {
                return new ReponseDeRequette<MessageDto>
                {
                    Success = false,
                    Message = "Echec de L'ajout d'un message",
                    StatusCode = (int)HttpStatusCode.NoContent
                };
            }

            return new ReponseDeRequette<MessageDto>
            {
                Success = true,
                Message = "Ajout d'message réussit",
                Data = _mapper.Map<MessageDto>(messageACreer),
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
