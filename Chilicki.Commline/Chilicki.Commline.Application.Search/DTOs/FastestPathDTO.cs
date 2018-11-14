using Chilicki.Commline.Domain.Search.Aggregates.Descriptions;
using System.Collections.Generic;

namespace Chilicki.Commline.Application.Search.DTOs
{
    public class FastestPathDTO
    {
        public IEnumerable<StopConnectionDTO> Path { get; set; }
        public IEnumerable<StopConnectionDTO> FlattenPath { get; set; }
        public FastestPathDescription PathDescription { get; set; }

    }
}
