using Chilicki.Commline.Domain.Search.ValueObjects.Descriptions;
using System.Collections.Generic;

namespace Chilicki.Commline.Domain.Search.Aggregates.Descriptions
{
    public class FastestPathDescription
    {
        public IList<DescriptionRow> DescriptionRows { get; set; } = new List<DescriptionRow>();
    }
}
