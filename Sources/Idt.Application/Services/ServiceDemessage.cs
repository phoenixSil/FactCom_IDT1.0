using Idt.Features.Core.Commandes.MessagesCommandes.Commands;
using Idt.Features.Core.Commandes.MessagesCommandes.Requettes;
using Idt.Features.Dtos.Messages;
using Idt.Features.Interfaces.Services;
using Idt.Features.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Application.Services
{
    public class ServiceDemessage : IServiceDemessage
    {
        private readonly IMediator _mediator;
        public ServiceDemessage(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<ReponseDeRequette<MessageDto>> AjouterUnMessage(MessageACreerDto messageDto)
        {
            var response = await _mediator.Send(new AjouterUnMessageCmd { MessageDto = messageDto });
            return response;
        }

        public async Task<ReponseDeRequette<List<MessageDto>>> LireTousLesMessages()
        {
            var response = await _mediator.Send(new LireTousLesMessagesCmd { });
            return response;
        }

        public async Task<ReponseDeRequette<MessageDetailDto>> LireDetailDUnMessage(Guid messageId)
        {
            var response = await _mediator.Send(new LireDetailDunMessageCmd { MessageId = messageId });
            return response;
        }



        public async Task<ReponseDeRequette<MessageDto>> ModifierUnMessage(Guid messageId, MessageAModifierDto messageDto)
        {
            var response = await _mediator.Send(new ModifierUnMessageCmd { MessageId = messageId, MessageDto = messageDto });
            return response;
        }


        public async Task<ReponseDeRequette<bool>> SupprimerUnMessage(Guid messageId)
        {
            var response = await _mediator.Send(new SupprimerUnMessageCmd { MessageId = messageId });
            return response;
        }
    }
}