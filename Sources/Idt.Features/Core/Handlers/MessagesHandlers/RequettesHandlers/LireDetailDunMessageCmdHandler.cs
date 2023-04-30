using AutoMapper;
using Idt.Features.Core.Commandes.MessagesCommandes.Requettes;
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

namespace Idt.Features.Core.Handlers.MessagesHandlers.RequettesHandlers
{
    public class LireDetailDunMessageCmdHandler : IRequestHandler<LireDetailDunMessageCmd, ReponseDeRequette<MessageDetailDto>>
    {
        private readonly IPointDaccess _pointDaccess;
        private readonly IMapper _mapper;

        public LireDetailDunMessageCmdHandler(IMapper mapper, IPointDaccess pointDaccess)
        {
            _pointDaccess = pointDaccess;
            _mapper = mapper;
        }

        public async Task<ReponseDeRequette<MessageDetailDto>> Handle(LireDetailDunMessageCmd request, CancellationToken cancellationToken)
        {
            var messageRecept = await _pointDaccess.RepertoireDemessage.LireDetailDunMessage(request.MessageId);
            var MessageDetail = _mapper.Map<MessageDetailDto>(messageRecept);

            var success = true;
            var message = "Lecture réussit!!";
            var statusCode = MessageDetail is null ? (int)HttpStatusCode.NotFound : (int)HttpStatusCode.OK;

            return new ReponseDeRequette<MessageDetailDto>
            {
                Success = success,
                Data = MessageDetail,
                Message = message,
                StatusCode = statusCode
            };
        }
    }
}