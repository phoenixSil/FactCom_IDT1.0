using Idt.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Interfaces.Repertoires
{
    public interface IRepertoireDemessage : IRepertoireGenerique<Message>
    {
        public Task<Message?> LireDetailDunMessage(Guid messageId);
    }
}
