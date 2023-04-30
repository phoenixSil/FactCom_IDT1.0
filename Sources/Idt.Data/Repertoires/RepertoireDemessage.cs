using Idt.Data.Context;
using Idt.Domain;
using Idt.Features.Interfaces.Repertoires;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Data.Repertoires
{
    public class RepertoireDemessage : RepertoireGenerique<Message>, IRepertoireDemessage
    {
        private readonly IdtDbContext _context;
        public RepertoireDemessage(IdtDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Message> LireDetailDunMessage(Guid messageId)
        {
            var message = await _context.Messages
                    .Include(adr => adr.Utilisateur)
                    .SingleOrDefaultAsync(ms => ms.Id == messageId);
            return message;
        }
    }
}
