using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Domain
{
    public class Adresse: BaseDomainEntite
    {
        public string Pays { get; set; }
        public string Region { get; set; }
        public string Ville { get; set; }
        public int Telephone { get; set; }
        public int Cellulaire { get; set; }
        public bool EstAdressePrincipale { get; set; }
        public Guid UtilisateurId { get; set; }
        public Utilisateur Utilisateur { get; set; }
    }
}
