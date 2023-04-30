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
    public class LireDetailDunUtilisateurCmdHandler : IRequestHandler<LireDetailDunUtilisateurCmd, ReponseDeRequette<UtilisateurDetailDto>>
    {
        private readonly IPointDaccess _pointDaccess;
        private readonly IMapper _mapper;

        public LireDetailDunUtilisateurCmdHandler(IMapper mapper, IPointDaccess pointDaccess)
        {
            _pointDaccess = pointDaccess;
            _mapper = mapper;
        }

        public async Task<ReponseDeRequette<UtilisateurDetailDto>> Handle(LireDetailDunUtilisateurCmd request, CancellationToken cancellationToken)
        {
            var utilisateur = await _pointDaccess.RepertoireDutilisateur.LireDetailDunUtilisateur(request.UtilisateurId);
            var UtilisateurDetail = _mapper.Map<UtilisateurDetailDto>(utilisateur);

            var success = true;
            var message = "Lecture réussit!!";
            var statusCode = UtilisateurDetail is null ? (int)HttpStatusCode.NotFound : (int)HttpStatusCode.OK;

            return new ReponseDeRequette<UtilisateurDetailDto>
            {
                Success = success,
                Data = UtilisateurDetail,
                Message = message,
                StatusCode = statusCode
            };
        }
    }
}