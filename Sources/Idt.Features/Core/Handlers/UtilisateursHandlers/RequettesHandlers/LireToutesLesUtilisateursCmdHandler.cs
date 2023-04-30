using AutoMapper;
using Idt.Features.Core.Commandes.UtilisateursCommandes.Requettes;
using Idt.Features.Dtos.Utilisateurs;
using Idt.Features.Interfaces.Repertoires;
using Idt.Features.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Core.Handlers.UtilisateursHandlers.RequettesHandlers
{
    public class LireToutesLesUtilisateursCmdHandler : IRequestHandler<LireTousLesUtilisateursCmd, ReponseDeRequette<List<UtilisateurDto>>>
    {
        private readonly IPointDaccess _pointDaccess;
        private readonly IMapper _mapper;

        public LireToutesLesUtilisateursCmdHandler(IPointDaccess pointDaccess, IMapper mapper)
        {
            _pointDaccess = pointDaccess;
            _mapper = mapper;
        }

        public async Task<ReponseDeRequette<List<UtilisateurDto>>> Handle(LireTousLesUtilisateursCmd request, CancellationToken cancellationToken)
        {
            var listUtilisateur = await _pointDaccess.RepertoireDutilisateur.Lire();
            var listUtilisateurDto = _mapper.Map<List<UtilisateurDto>>(listUtilisateur);

            var success = true;
            var message = "Lecture réussit!!";
            var statusCode = listUtilisateurDto.Any() ? (int)HttpStatusCode.OK : (int)HttpStatusCode.NoContent;

            return new ReponseDeRequette<List<UtilisateurDto>>
            {
                Success = success,
                Data = listUtilisateurDto,
                Message = message,
                StatusCode = statusCode
            };
        }

    }
}