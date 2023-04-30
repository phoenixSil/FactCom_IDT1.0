using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Dtos.Utilisateurs.Validateurs
{
    public class ValidateurDeLaRecuperationDunUtilisateur : AbstractValidator<LoggingUserDto>
    {
        public ValidateurDeLaRecuperationDunUtilisateur()
        {
            RuleFor(x => x.Email)
                .NotEmpty();

            RuleFor(x => x.Password)
               .NotEmpty()
               .MinimumLength(8)
               .MaximumLength(120);
        }
    }  
}
