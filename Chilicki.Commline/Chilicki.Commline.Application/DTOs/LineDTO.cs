using Chilicki.Commline.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Chilicki.Commline.Application.DTOs
{
    public class LineDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsCircular { get; set; }
        public string Color { get; set; }
        public LineType LineType { get; set; }
        public IEnumerable<StopDTO> Stops { get; set; }
    }
}
