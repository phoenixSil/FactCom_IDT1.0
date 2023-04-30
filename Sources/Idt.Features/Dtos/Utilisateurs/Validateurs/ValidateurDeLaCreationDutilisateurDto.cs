using FluentValidation;
using Idt.Features.Dtos.Utilisateurs.Validateurs;
using Idt.Features.Dtos.Utilisateurs;
using Idt.Features.Interfaces.Repertoires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Dtos.Utilisateurs.Validateurs
{
    public class ValidateurDeLaCreationDutilisateurDto : AbstractValidator<UtilisateurACreerDto>
    {
        public ValidateurDeLaCreationDutilisateurDto()
        {
            Include(new ValidateurDeDtoDutilisateur());
        }
    }
}
