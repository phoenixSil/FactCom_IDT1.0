using Idt.Features.Dtos.Adresses;
using Idt.Features.Dtos.Messages;
using Idt.Features.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Core.Commandes.MessagesCommandes.Requettes
{
    public class LireTousLesMessagesCmd : IRequest<ReponseDeRequette<List<MessageDto>>>
    {
    }
}
