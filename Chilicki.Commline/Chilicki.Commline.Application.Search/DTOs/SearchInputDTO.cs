using System;

namespace Chilicki.Commline.Application.Search.DTOs
{
    public class SearchInputDTO
    {
        public long StartStopId { get; set; }
        public long DestinationStopId { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime StartDate { get; set; }
    }
}
