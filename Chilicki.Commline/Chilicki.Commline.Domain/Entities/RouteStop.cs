using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chilicki.Commline.Domain.Entities
{
    public class RouteStop
    {
        public long Id { get; set; }
        [Required]
        public int StopIndex { get; set; }
        public virtual ICollection<Departure> Departures { get; set; }
        [Required]
        public virtual Line Line { get; set; }
        [Required]
        public virtual Stop Stop { get; set; }
    }
}
