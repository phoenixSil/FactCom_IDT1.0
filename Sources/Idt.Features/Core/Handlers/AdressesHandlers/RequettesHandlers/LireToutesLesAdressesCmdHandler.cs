using AutoMapper;
using Idt.Features.Core.Commandes.AdressesCommandes.Requettes;
using Idt.Features.Dtos.Adresses;
using Idt.Features.Interfaces.Repertoires;
using Idt.Features.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Core.Handlers.AdressesHandlers.RequettesHandlers
{
    public class LireToutesLesAdressesCmdHandler : IRequestHandler<LireToutesLesAdressesCmd, ReponseDeRequette<List<AdresseDto>>>
    {
        private readonly IPointDaccess _pointDaccess;
        private readonly IMapper _mapper;

        public LireToutesLesAdressesCmdHandler(IPointDaccess pointDaccess, IMapper mapper)
        {
            _pointDaccess = pointDaccess;
            _mapper = mapper;
        }

        public async Task<ReponseDeRequette<List<AdresseDto>>> Handle(LireToutesLesAdressesCmd request, CancellationToken cancellationToken)
        {
            var listAdresse = await _pointDaccess.RepertoireDadresse.Lire();
            var listAdresseDto = _mapper.Map<List<AdresseDto>>(listAdresse);

            var success = true;
            var message = "Lecture réussit!!";
            var statusCode = listAdresseDto.Any() ? (int)HttpStatusCode.OK : (int)HttpStatusCode.NoContent;

            return new ReponseDeRequette<List<AdresseDto>>
            {
                Success = success,
                Data = listAdresseDto,
                Message = message,
                StatusCode = statusCode
            };
        }

    }
}