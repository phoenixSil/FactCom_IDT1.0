using Idt.Data.Context;
using Idt.Features.Interfaces.Repertoires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Data.Repertoires
{
    public class PointDaccess : IPointDaccess
    {
        private readonly IdtDbContext _context;
        private IRepertoireDutilisateur _repertoireUtilisateur;
        private IRepertoireDadresse _repertoireDadresse;
        private IRepertoireDemessage _repertoireDemessage;

        public PointDaccess(IdtDbContext context)
        {
            _context = context;
        }

        public async Task Enregistrer()
        {
            await _context.SaveChangesAsync();
        }

        public IRepertoireDutilisateur RepertoireDutilisateur => _repertoireUtilisateur ??= new RepertoireDutilisateur(_context);
        public IRepertoireDadresse RepertoireDadresse => _repertoireDadresse ??= new RepertoireDadresse(_context);
        public IRepertoireDemessage RepertoireDemessage => _repertoireDemessage ??= new RepertoireDemessage(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
