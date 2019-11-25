using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chilicki.Commline.Domain.Entities
{
    public class Trip
    {
        public long Id { get; set; }
        [Required]
        public virtual ICollection<Departure> Departures { get; set; }
        [Required]
        public virtual Line Line { get; set; }        
    }
}
