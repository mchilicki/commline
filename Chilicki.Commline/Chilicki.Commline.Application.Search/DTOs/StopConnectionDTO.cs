using Chilicki.Commline.Application.DTOs;
using System;

namespace Chilicki.Commline.Application.Search.DTOs
{
    public class StopConnectionDTO
    {
        public StopDTO SourceStop { get; set; }
        public StopDTO DestinationStop { get; set; }
        public LineDTO Line { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsTransfer { get; set; }
    }
}
