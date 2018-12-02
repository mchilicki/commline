using Chilicki.Commline.Domain.Enums;
using System;

namespace Chilicki.Commline.Application.DTOs
{
    public class DepartureDTO
    {
        public long Id { get; set; }
        public int RunIndex { get; set; }
        public bool IsNextDay { get; set; }
        public bool IsBetweenDays { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public virtual DayType DayType { get; set; }
    }
}
