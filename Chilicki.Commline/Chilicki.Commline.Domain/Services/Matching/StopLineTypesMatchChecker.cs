using Chilicki.Commline.Domain.Enums;
using Chilicki.Commline.Domain.Resources;
using System;

namespace Chilicki.Commline.Domain.Services.Matching
{
    public class StopLineTypesMatchChecker
    {
        public bool AreStopAndLineTypesMatching(StopType stopType, LineType lineType)
        {
            if (stopType == StopType.Bus)
            {
                return (lineType == LineType.Bus);
            }
            if (stopType == StopType.Tram)
            {
                return (lineType == LineType.Tram);
            }
            throw new ArgumentException(DomainErrors.InvalidStopType);
        }
    }
}
