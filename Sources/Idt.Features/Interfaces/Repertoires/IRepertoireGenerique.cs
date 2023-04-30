using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Interfaces.Repertoires
{
    public interface IRepertoireGenerique<T> where T : class
    {
        public Task<IEnumerable<T>> Lire();
        public Task<T> Lire(Guid id);
        public Task<T> Ajoutter(T entite);
        public Task<T> Modifier(T entite);
        public Task<bool> Supprimer(T entite);
        public Task<bool> Exists(Guid id);
    }
}
