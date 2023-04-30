using Idt.Features.Dtos.Utilisateurs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Dtos.Adresses
{
    public class AdresseACreerDto: IAdresseDto
    {
        public string Pays { get; set; }
        public string Region { get; set; }
        public string Ville { get; set; }
        public int Telephone { get; set; }
        public int Cellulaire { get; set; }
        public bool EstAdressePrincipale { get; set; }
        public Guid UtilisateurId { get; set; }
    }
}
