using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Domain
{
    public class Message: BaseDomainEntite
    {
        public string Content { get; set; }
        public Guid UtilisateurId { get; set; }
        public bool EstLue { get; set; }
        public Utilisateur Utilisateur { get; set; }
    }
}
