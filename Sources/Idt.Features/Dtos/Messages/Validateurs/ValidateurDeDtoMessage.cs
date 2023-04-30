using FluentValidation;
using Idt.Features.Interfaces.Repertoires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Dtos.Messages.Validateurs
{
    public class ValidateurDeDtoDemessage : AbstractValidator<IMessageDto>
    {
        private readonly IPointDaccess _pointDaccess;
        public ValidateurDeDtoDemessage(IPointDaccess pointDaccess)
        {
            _pointDaccess = pointDaccess;

            RuleFor(p => p.Content)
                .NotEmpty();

            RuleFor(p => p.UtilisateurId)
               .NotEmpty()
               .MustAsync(async (id, token) =>
               {
                   var personneExists = await _pointDaccess.RepertoireDutilisateur.Exists(id);
                   return personneExists;
               })
               .WithMessage($" l'utilisateur n'existe pas dans la base de donnees  ");
        }
    }
}
