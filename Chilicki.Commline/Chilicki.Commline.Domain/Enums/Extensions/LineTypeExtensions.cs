using Chilicki.Commline.Domain.Resources;
using System;

namespace Chilicki.Commline.Domain.Enums.Extensions
{
    public static class LineTypeExtensions
    {
        public static string NameOf(this LineType lineType)
        {
            switch (lineType)
            {
                case LineType.Bus:
                    return CommlineResources.Shared_Bus;
                case LineType.Tram:
                    return CommlineResources.Shared_Tram;
                default:
                    throw new ArgumentException(DomainErrors.InvalidLineType);
            }
        }
    }
}
