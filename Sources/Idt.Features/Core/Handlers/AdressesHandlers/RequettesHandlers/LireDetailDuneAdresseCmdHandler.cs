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
    public class LireDetailDuneAdresseCmdHandler : IRequestHandler<LireDetailDuneAdresseCmd, ReponseDeRequette<AdresseDetailDto>>
    {
        private readonly IPointDaccess _pointDaccess;
        private readonly IMapper _mapper;

        public LireDetailDuneAdresseCmdHandler(IMapper mapper, IPointDaccess pointDaccess)
        {
            _pointDaccess = pointDaccess;
            _mapper = mapper;
        }

        public async Task<ReponseDeRequette<AdresseDetailDto>> Handle(LireDetailDuneAdresseCmd request, CancellationToken cancellationToken)
        {
            var adresse = await _pointDaccess.RepertoireDadresse.LireDetailDuneAdresse(request.AdresseId);
            var AdresseDetail = _mapper.Map<AdresseDetailDto>(adresse);

            var success = true;
            var message = "Lecture réussit!!";
            var statusCode = AdresseDetail is null ? (int)HttpStatusCode.NotFound : (int)HttpStatusCode.OK;

            return new ReponseDeRequette<AdresseDetailDto>
            {
                Success = success,
                Data = AdresseDetail,
                Message = message,
                StatusCode = statusCode
            };
        }
    }
}