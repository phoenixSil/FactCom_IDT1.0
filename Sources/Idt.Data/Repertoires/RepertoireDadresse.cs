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
    public class RepertoireDadresse : RepertoireGenerique<Adresse>, IRepertoireDadresse
    {
        private readonly IdtDbContext _context;
        public RepertoireDadresse(IdtDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Adresse?> LireDetailDuneAdresse(Guid adresseId)
        {
            var adresse = await _context.Adresses
                    .Include(adr => adr.Utilisateur)
                    .SingleOrDefaultAsync(adr => adr.Id == adresseId);
            return adresse;
        }
    }
}
