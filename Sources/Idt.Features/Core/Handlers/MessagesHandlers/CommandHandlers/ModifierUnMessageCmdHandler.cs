using AutoMapper;
using Idt.Features.Core.Commandes.MessagesCommandes.Commands;
using Idt.Features.Dtos.Messages.Validateurs;
using Idt.Features.Dtos.Messages;
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
    public class ModifierUnMessageCmdHandler : IRequestHandler<ModifierUnMessageCmd, ReponseDeRequette<MessageDto>>
    {
        private readonly IPointDaccess _pointDaccess;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ModifierUnMessageCmdHandler(IPointDaccess pointDaccess, IMediator mediator, IMapper mapper)
        {
            _pointDaccess = pointDaccess;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ReponseDeRequette<MessageDto>> Handle(ModifierUnMessageCmd request, CancellationToken cancellationToken)
        {
            var reponse = new ReponseDeRequette<MessageDto>();
            var message = await _pointDaccess.RepertoireDemessage.Lire(request.MessageId);

            if (message is null)
            {
                return new ReponseDeRequette<MessageDto>
                {
                    Success = false,
                    Message = "Modification Echec",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            if (request.MessageDto is null)
            {
                return new ReponseDeRequette<MessageDto>
                {
                    Success = false,
                    Message = "Echec de la Modification",
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }

            var validateur = new ValidateurDeLaModificationDemessage();
            var resultatValidation = await validateur.ValidateAsync(request.MessageDto, cancellationToken);

            if (!await _pointDaccess.RepertoireDemessage.Exists(request.MessageId))
            {
                return new ReponseDeRequette<MessageDto>
                {
                    Success = false,
                    Message = "l'message a modifier nexiste pas !!!",
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }

            if (!resultatValidation.IsValid)
            {
                return new ReponseDeRequette<MessageDto>
                {
                    Success = false,
                    Message = "Echec de la Modification",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Errors = resultatValidation.Errors.Select(q => q.ErrorMessage).ToList()
                };
            }

            _mapper.Map(request.MessageDto, message);
            await _pointDaccess.RepertoireDemessage.Modifier(message);
            await _pointDaccess.Enregistrer();

            return new ReponseDeRequette<MessageDto>
            {
                Success = true,
                Message = "ModificationReussit !!!",
                StatusCode = (int)HttpStatusCode.OK,
                Data = _mapper.Map<MessageDto>(message)
            };
        }


    }
}