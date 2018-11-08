using Chilicki.Commline.Domain.Search.Aggregates;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using Chilicki.Commline.Domain.Search.Services.Base;
using System;
using System.Collections.Generic;

namespace Chilicki.Commline.Domain.Search.Services
{
    public class DijkstraConnectionSearchEngine : IConnectionSearchEngine
    {
        public IEnumerable<StopConnection> SearchConnections(SearchInput search, StopGraph graph)
        {
            throw new NotImplementedException();
        }
    }
}
