using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LinksExample.Models;

namespace LinksExample.Enrichers
{
    public abstract class Enricher<T> 
        : IEnricher where T : Representation
    {
        public virtual Task<bool> Match(object target) => Task.FromResult(target is T);
        public Task Process(object representation) => Process(representation as T);
        public abstract Task Process(T representation);
    }

    public interface IEnricher
    {
        Task<bool> Match(object target);
        Task Process(object representation);
    }
}