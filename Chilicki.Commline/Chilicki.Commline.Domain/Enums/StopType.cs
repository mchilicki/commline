using Chilicki.Commline.Domain.Enums.Extensions;
using Chilicki.Commline.Domain.Resources;

namespace Chilicki.Commline.Domain.Enums
{
    public enum StopType
    {
        [LocalizedDescription("Shared_BusStop", typeof(CommlineResources))]
        Bus,
        [LocalizedDescription("Shared_TramStop", typeof(CommlineResources))]
        Tram
    }
}
