using Chilicki.Commline.Domain.Resources;
using System;

namespace Chilicki.Commline.Domain.Enums.Extensions
{
    public static class DayTypeExtensions
    {
        public static string NameOf(this DayType dayType)
        {
            switch (dayType)
            {
                case DayType.Workday:
                    return CommlineResources.Shared_WorkDay;
                case DayType.Free:
                    return CommlineResources.Shared_Free;
                default:
                    throw new ArgumentException(DomainErrors.InvalidDayType);
            }
        }
    }
}
