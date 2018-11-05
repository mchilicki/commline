using Chilicki.Commline.Domain.Search.Aggregates;
using Chilicki.Commline.Domain.Search.Aggregates.Graph;
using System.Collections.Generic;

namespace Chilicki.Commline.Domain.Search.Services.Base
{
    public interface IConnectionSearchEngine
    {
        IEnumerable<StopConnection> SearchConnections(SearchInput search, StopGraph graph);
    }
}
