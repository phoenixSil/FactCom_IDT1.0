using Idt.Features.Dtos.Adresses;
using Idt.Features.Dtos.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Dtos.Utilisateurs
{
    public class UtilisateurDetailDto: BaseDomainDto
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string PseudoUtilisateur { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<AdresseDto> Adresse { get; set; }
        public List<MessageDto> Messages { get; set; }
    }
}
