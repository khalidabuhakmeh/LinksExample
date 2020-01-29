using System.Collections.Generic;
using System.Linq;

namespace LinksExample.Models
{
    public abstract class Representation
    {
        public List<Link> Links { get; set; } 
            = new List<Link>();

        public Representation AddLink(Link link)
        {
            var exists = Links.FirstOrDefault(x => x.Id == link.Id);
            if (exists != null)
            {
                Links.Remove(exists);
            } 
            
            Links.Add(link);
            return this;
        }
    }

    public class Link
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Url { get; set; }
    }
}