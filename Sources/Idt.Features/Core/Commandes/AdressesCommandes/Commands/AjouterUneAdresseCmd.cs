using Idt.Features.Dtos.Adresses;
using Idt.Features.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Core.Commandes.AdressesCommandes.Commands
{
    public class AjouterUneAdresseCmd: IRequest<ReponseDeRequette<AdresseDto>>
    {
        public AdresseACreerDto AdresseDto { get; set; }
    }
}
