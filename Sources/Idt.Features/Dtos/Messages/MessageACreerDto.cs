using Idt.Features.Dtos.Utilisateurs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Dtos.Messages
{
    public class MessageACreerDto: IMessageDto
    {
        public string Content { get; set; }
        public Guid UtilisateurId { get; set; }
        public bool EstLue { get; set; } = false;

    }
}
