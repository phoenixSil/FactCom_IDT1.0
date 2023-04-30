using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Dtos.Adresses
{
    public class AdresseDto
    {
        public string Pays { get; set; }
        public string Region { get; set; }
        public string Ville { get; set; }
        public int Telephone { get; set; }
        public int Cellulaire { get; set; }
    }
}
