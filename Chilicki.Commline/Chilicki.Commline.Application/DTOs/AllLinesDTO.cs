using System.Collections.Generic;

namespace Chilicki.Commline.Application.DTOs
{
    public class AllLinesDTO
    {
        public IEnumerable<LineDTO> Lines { get; set; }
        public IEnumerable<StopDTO> StopsWithoutLines { get; set; }
    }
}
