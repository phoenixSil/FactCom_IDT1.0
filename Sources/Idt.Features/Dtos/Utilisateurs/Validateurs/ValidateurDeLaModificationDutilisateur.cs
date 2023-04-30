using FluentValidation;

namespace Idt.Features.Dtos.Utilisateurs.Validateurs
{
    public class ValidateurDeLaModificationDutilisateur : AbstractValidator<UtilisateurAModifierDto>
    {
        public ValidateurDeLaModificationDutilisateur()
        {

            RuleFor(p => p.Id).NotNull()
                .NotEmpty()
                .WithMessage("Id doit pas etre null");

            Include(new ValidateurDeDtoDutilisateur());
        }
    }
}
