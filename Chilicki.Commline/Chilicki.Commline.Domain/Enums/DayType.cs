using Chilicki.Commline.Domain.Enums.Extensions;
using Chilicki.Commline.Domain.Resources;

namespace Chilicki.Commline.Domain.Enums
{
    public enum DayType
    {
        [LocalizedDescription("Shared_WorkDay", typeof(CommlineResources))]
        Workday,
        [LocalizedDescription("Shared_Free", typeof(CommlineResources))]
        Free
    }
}
