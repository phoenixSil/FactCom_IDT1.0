using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Interfaces.Repertoires
{
    public interface IPointDaccess : IDisposable
    {
        IRepertoireDadresse RepertoireDadresse { get; }
        IRepertoireDutilisateur RepertoireDutilisateur { get; }
        IRepertoireDemessage RepertoireDemessage { get; }
        Task Enregistrer();
    }
}
