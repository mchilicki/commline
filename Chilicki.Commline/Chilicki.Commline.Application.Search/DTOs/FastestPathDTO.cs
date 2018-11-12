using System.Collections.Generic;

namespace Chilicki.Commline.Application.Search.DTOs
{
    public class FastestPathDTO
    {
        public IEnumerable<StopConnectionDTO> Path { get; set; }
    }
}
