using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Application.DTOs.Search
{
    public class SearchInputDTO
    {
        public long StartStopId { get; set; }
        public long DestinationStopId { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime StartDate { get; set; }
    }
}
