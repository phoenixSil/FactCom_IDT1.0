using Idt.Features.Core.Commandes.AdressesCommandes.Commands;
using Idt.Features.Core.Commandes.AdressesCommandes.Requettes;
using Idt.Features.Dtos.Adresses;
using Idt.Features.Interfaces.Services;
using Idt.Features.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Application.Services
{
    public class ServiceDadresse : IServiceDadresse
    {
        private readonly IMediator _mediator;
        public ServiceDadresse(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<ReponseDeRequette<AdresseDto>> AjouterUneAdresse(AdresseACreerDto adresseDto)
        {
            var response = await _mediator.Send(new AjouterUneAdresseCmd { AdresseDto = adresseDto });
            return response;
        }

        public async Task<ReponseDeRequette<List<AdresseDto>>> LireToutesLesAdresses()
        {
            var response = await _mediator.Send(new LireToutesLesAdressesCmd { });
            return response;
        }

        public async Task<ReponseDeRequette<AdresseDetailDto>> LireDetailDUneAdresse(Guid adresseId)
        {
            var response = await _mediator.Send(new LireDetailDuneAdresseCmd { AdresseId = adresseId });
            return response;
        }

        public async Task<ReponseDeRequette<AdresseDto>> ModifierUneAdresse(Guid adresseId, AdresseAModifierDto adresseDto)
        {
            var response = await _mediator.Send(new ModifierUneAdresseCmd { AdresseId = adresseId, AdresseDto = adresseDto });
            return response;
        }


        public async Task<ReponseDeRequette<bool>> SupprimerUneAdresse(Guid adresseId)
        {
            var response = await _mediator.Send(new SupprimerUneAdresseCmd { AdresseId = adresseId });
            return response;
        }
    }
}
