using Idt.Features.Dtos.Adresses;
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
    public class LireTousLesUtilisateursCmd : IRequest<ReponseDeRequette<List<UtilisateurDto>>>
    {
    }
}
