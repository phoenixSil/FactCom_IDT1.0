using Idt.Features.Dtos.Adresses;
using Idt.Features.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Interfaces.Services
{
    public interface IServiceDadresse
    {
        Task<ReponseDeRequette<List<AdresseDto>>> LireToutesLesAdresses();
        Task<ReponseDeRequette<AdresseDetailDto>> LireDetailDUneAdresse(Guid id);
        Task<ReponseDeRequette<AdresseDto>> ModifierUneAdresse(Guid adresseId, AdresseAModifierDto adresseDto);
        Task<ReponseDeRequette<AdresseDto>> AjouterUneAdresse(AdresseACreerDto adresseDto);
        Task<ReponseDeRequette<bool>> SupprimerUneAdresse(Guid adresseId);
    }
}
