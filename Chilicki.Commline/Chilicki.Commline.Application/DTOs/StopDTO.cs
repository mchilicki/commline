using Chilicki.Commline.Domain.Enums;
using System;

namespace Chilicki.Commline.Application.DTOs
{
    public class StopDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int SiteNumber { get; set; }
        public StopType StopType { get; set; }
    }
}
