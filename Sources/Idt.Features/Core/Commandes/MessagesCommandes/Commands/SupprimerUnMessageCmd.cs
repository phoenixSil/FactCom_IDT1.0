using Idt.Features.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Core.Commandes.MessagesCommandes.Commands
{
    public class SupprimerUnMessageCmd : IRequest<ReponseDeRequette<bool>>
    {
        public Guid MessageId { get; set; }
    }
}
