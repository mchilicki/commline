using Chilicki.Commline.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Chilicki.Commline.Domain.Entities
{
    public class Departure
    {
        public Guid Id { get; set; }
        public int RunIndex { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public virtual DayType DayType { get; set; }
        public bool IsOnNextDay { get; set; }
        public bool IsBetweenDays { get; set; }
        [Required]
        public virtual RouteStop RouteStop { get; set; }
    }
}