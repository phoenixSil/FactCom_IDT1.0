using Idt.Features.Dtos.Messages;
using Idt.Features.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Interfaces.Services
{
    public interface IServiceDemessage
    {
        Task<ReponseDeRequette<List<MessageDto>>> LireTousLesMessages();
        Task<ReponseDeRequette<MessageDetailDto>> LireDetailDUnMessage(Guid id);
        Task<ReponseDeRequette<MessageDto>> ModifierUnMessage(Guid messageId, MessageAModifierDto messageDto);
        Task<ReponseDeRequette<MessageDto>> AjouterUnMessage(MessageACreerDto messageDto);
        Task<ReponseDeRequette<bool>> SupprimerUnMessage(Guid messageId);
    }
}
