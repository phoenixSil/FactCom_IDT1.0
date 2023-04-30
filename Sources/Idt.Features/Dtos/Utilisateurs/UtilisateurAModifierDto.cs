using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Dtos.Utilisateurs
{
    public class UtilisateurAModifierDto: BaseDomainDto, IUtilisateurDto
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string PseudoUtilisateur { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
