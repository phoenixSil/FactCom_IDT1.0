using FluentValidation;
using Idt.Features.Interfaces.Repertoires;

namespace Idt.Features.Dtos.Messages.Validateurs
{
    public class ValidateurDeLaCreationDemessageDto: AbstractValidator<MessageACreerDto>
    {
        private readonly IPointDaccess _pointDaccess;
        public ValidateurDeLaCreationDemessageDto(IPointDaccess pointDaccess)
        {
            _pointDaccess = pointDaccess;
            Include(new ValidateurDeDtoDemessage(_pointDaccess));
        }
    }
}
