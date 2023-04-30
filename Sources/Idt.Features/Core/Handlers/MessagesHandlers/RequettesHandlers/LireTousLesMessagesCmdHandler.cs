using AutoMapper;
using Idt.Features.Core.Commandes.MessagesCommandes.Requettes;
using Idt.Features.Dtos.Messages;
using Idt.Features.Interfaces.Repertoires;
using Idt.Features.Responses;
using MediatR;
using System.Net;

namespace Idt.Features.Core.Handlers.MessagesHandlers.RequettesHandlers
{
    public class LireTousLesMessagesCmdHandler : IRequestHandler<LireTousLesMessagesCmd, ReponseDeRequette<List<MessageDto>>>
    {
        private readonly IPointDaccess _pointDaccess;
        private readonly IMapper _mapper;

        public LireTousLesMessagesCmdHandler(IPointDaccess pointDaccess, IMapper mapper)
        {
            _pointDaccess = pointDaccess;
            _mapper = mapper;
        }

        public async Task<ReponseDeRequette<List<MessageDto>>> Handle(LireTousLesMessagesCmd request, CancellationToken cancellationToken)
        {
            var listMessage = await _pointDaccess.RepertoireDemessage.Lire();
            var listMessageDto = _mapper.Map<List<MessageDto>>(listMessage);

            var success = true;
            var message = "Lecture réussit!!";
            var statusCode = listMessageDto.Any() ? (int)HttpStatusCode.OK : (int)HttpStatusCode.NoContent;

            return new ReponseDeRequette<List<MessageDto>>
            {
                Success = success,
                Data = listMessageDto,
                Message = message,
                StatusCode = statusCode
            };
        }

    }
}