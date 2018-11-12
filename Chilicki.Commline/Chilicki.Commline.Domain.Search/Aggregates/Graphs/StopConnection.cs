using Chilicki.Commline.Domain.Entities;
using System;

namespace Chilicki.Commline.Domain.Search.Aggregates.Graphs
{
    public class StopConnection
    {
        public StopVertex SourceStop { get; set; }
        public StopVertex DestinationStop { get; set; }
        public Line Line { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsTransfer { get; set; }
    }
}
