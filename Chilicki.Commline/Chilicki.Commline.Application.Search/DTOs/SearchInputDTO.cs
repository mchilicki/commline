using System;

namespace Chilicki.Commline.Application.Search.DTOs
{
    public class SearchInputDTO
    {
        public Guid StartStopId { get; set; }
        public Guid DestinationStopId { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime StartDate { get; set; }
    }
}
