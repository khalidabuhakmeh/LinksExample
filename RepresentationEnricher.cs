using System.Collections.Generic;
using System.Threading.Tasks;
using LinksExample.Enrichers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LinksExample
{
    public class RepresentationEnricher : IAsyncResultFilter
    {
        private readonly IEnumerable<IEnricher> enrichers;

        public RepresentationEnricher(IEnumerable<IEnricher> enrichers)
        {
            this.enrichers = enrichers;
        }
        
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ObjectResult result)
            {
                var value = result.Value;
                foreach (var enricher in enrichers)
                {
                    if (await enricher.Match(value))
                    {
                        await enricher.Process(value);
                    }
                }
            }

            await next();
        }
    }
}