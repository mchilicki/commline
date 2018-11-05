using Chilicki.Commline.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Domain.Search.Aggregates.Graph
{
    public class StopConnection
    {
        public StopVertex SourceStop { get; set; }
        public StopVertex DestinationStop { get; set; }
        public Line Line { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
