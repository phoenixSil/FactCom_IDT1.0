using FluentValidation;
using Idt.Features.Interfaces.Repertoires;

namespace Idt.Features.Dtos.Adresses.Validateurs
{
    public class ValidateurDeDtoDadresse : AbstractValidator<IAdresseDto>
    {
        private readonly IPointDaccess _pointDaccess;

        public ValidateurDeDtoDadresse(IPointDaccess pointDaccess)
        {
            _pointDaccess = pointDaccess;

            RuleFor(p => p.Pays)
                .NotEmpty()
                .MinimumLength(3).WithMessage("Le nom du pays est au trop court")
                .MaximumLength(100).WithMessage("le nom du pays ne doit pas exceder les 100 caracteres");

            RuleFor(p => p.Ville)
                .NotEmpty()
                .MinimumLength(3).WithMessage("Le nom de la ville est au trop court")
                .MaximumLength(100).WithMessage("le nom de la ville ne doit pas exceder les 100 caracteres");

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
