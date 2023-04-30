using Idt.Data.Context;
using Idt.Domain;
using Idt.Features.Core.Utils;
using Idt.Features.Interfaces.Repertoires;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Data.Repertoires
{
    public class RepertoireDutilisateur : RepertoireGenerique<Utilisateur>, IRepertoireDutilisateur
    {
        private readonly IdtDbContext _context;
        public RepertoireDutilisateur(IdtDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Utilisateur?> LireDetailDunUtilisateur(Guid utilisateurId)
        {
            var utilisateur = await _context.Utilisateurs.Where(x => x.Id == utilisateurId)
               .Include(etd => etd.Adresses)
               .SingleOrDefaultAsync();
            return utilisateur;
        }

        public async Task<Utilisateur?> LireParEmailEtMotDePasse(string email, string password)
        {
            var utilisateur = await _context.Utilisateurs.SingleOrDefaultAsync(x => x.Email == email
              && x.Password == password.CrypterLeMotDePassePourVErification());
            return utilisateur;
        }
    }
}
