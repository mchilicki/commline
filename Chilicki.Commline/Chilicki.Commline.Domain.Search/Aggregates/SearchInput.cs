using Chilicki.Commline.Domain.Entities;
using System;

namespace Chilicki.Commline.Domain.Search.Aggregates
{
    public class SearchInput
    {
        public Stop StartStop { get; set; }
        public Stop DestinationStop { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime StartFullDate
        {
            get
            {
                return StartDate + StartTime;
            }
        }
    }
}
