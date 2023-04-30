using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Dtos.Utilisateurs.Validateurs
{
    public class ValidateurDeDtoDutilisateur : AbstractValidator<IUtilisateurDto>
    {
        public ValidateurDeDtoDutilisateur()
        {
            RuleFor(x => x.Nom)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(100)
                .WithMessage("le Nom que vous avez entrer est incorrect ");

            RuleFor(x => x.Prenom)
               .NotEmpty()
               .MinimumLength(4)
               .MaximumLength(100)
               .WithMessage("le Nom que vous avez entrer est incorrect ");
        }
    }
}
