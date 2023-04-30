using Idt.Features.Dtos.Utilisateurs;
using Idt.Features.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Core.Commandes.UtilisateursCommandes.Requettes
{
    public class ObtenirUtilisateurParEmalPwdCmd : IRequest<ReponseDeRequette<LoggedUserDto>>
    {
        public LoggingUserDto Utilisateur { get; set; }
    }
}
