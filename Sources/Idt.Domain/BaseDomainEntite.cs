using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Domain
{
    public class BaseDomainEntite
    {
        public Guid Id { get; set; }
        public DateTime DateDeCreation { get; set; }
        public DateTime DateDeModification { get; set; }
        public Guid? UtilisateurId { get; set; }
    }
}
