using FluentValidation;
using Idt.Features.Interfaces.Repertoires;

namespace Idt.Features.Dtos.Adresses.Validateurs
{
    public class ValidateurDeLaCreationDadresseDto : AbstractValidator<AdresseACreerDto>
    {
        private readonly IPointDaccess _pointDaccess;
        public ValidateurDeLaCreationDadresseDto(IPointDaccess pointDaccess)
        {
            _pointDaccess = pointDaccess;
            Include(new ValidateurDeDtoDadresse(_pointDaccess));
        }
    }
}
