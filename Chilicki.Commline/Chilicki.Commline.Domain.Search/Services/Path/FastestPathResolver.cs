using Chilicki.Commline.Domain.Search.Aggregates;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Commline.Domain.Search.Services.Path
{
    public class FastestPathResolver
    {
        readonly FastestPathTransferService _fastestPathTransferService;

        public FastestPathResolver(FastestPathTransferService fastestPathTransferService)
        {
            _fastestPathTransferService = fastestPathTransferService;
        }

        public FastestPath ResolveFastestPath
            (SearchInput search, IEnumerable<StopConnection> vertexFastestConnection)
        {
            var fastestPath = new List<StopConnection>();
            var currentConnection = vertexFastestConnection
                .First(p => p.DestinationStop.Stop.Id == search.DestinationStop.Id);            
            fastestPath.Add(currentConnection);
            while (currentConnection.SourceStop.Stop.Id != search.StartStop.Id)
            {
                var previousConnection = currentConnection;
                var sourceVertex = currentConnection.SourceStop;
                currentConnection = vertexFastestConnection
                    .First(p => p.DestinationStop.Stop.Id == sourceVertex.Stop.Id);
                if (_fastestPathTransferService.ShouldBeTransfer(previousConnection, currentConnection))
                {
                    var transferBeetweenVertices = _fastestPathTransferService
                        .GenerateTransferAsStopConnection(previousConnection, currentConnection);
                    fastestPath.Add(transferBeetweenVertices);
                }
                fastestPath.Add(currentConnection);
            }
            if (_fastestPathTransferService.ShouldBeWaitingOnFirstStop(search, currentConnection))
            {
                var waitingOnFirstStop = _fastestPathTransferService
                    .GenerateWaitingAsStopConnection(search, currentConnection);
                fastestPath.Add(waitingOnFirstStop);
            }
            fastestPath.Reverse();
            return new FastestPath()
            {
                Path = fastestPath,
            };
        } 
    }
}
