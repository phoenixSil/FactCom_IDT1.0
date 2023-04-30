﻿using Idt.Features.Dtos.Messages;
using Idt.Features.Dtos.Messages;
using Idt.Features.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Core.Commandes.MessagesCommandes.Commands
{
    public class ModifierUnMessageCmd: IRequest<ReponseDeRequette<MessageDto>>
    {
        public Guid MessageId { get; set; }
        public MessageAModifierDto MessageDto { get; set; }
    }
}
