using Chilicki.Commline.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
