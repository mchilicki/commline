using Chilicki.Commline.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Domain.Aggregates.Search
{
    public class SearchInput
    {
        public Stop StartStop { get; set; }
        public Stop DestinationStop { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime StartDate { get; set; }
    }
}
