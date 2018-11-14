using Chilicki.Commline.Domain.Search.Aggregates;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using Chilicki.Commline.Domain.Search.Factories.StopConnections;
using Chilicki.Commline.Domain.Services.Lines;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Commline.Domain.Search.Services.Path
{
    public class FastestPathResolver
    {
        readonly FastestPathTransferService _fastestPathTransferService;
        readonly StopConnectionCloner _cloner;
        readonly LineDirectionService _lineDirectionService;

        public FastestPathResolver(
            FastestPathTransferService fastestPathTransferService,
            StopConnectionCloner cloner,
            LineDirectionService lineDirectionService)
        {
            _fastestPathTransferService = fastestPathTransferService;
            _cloner = cloner;
            _lineDirectionService = lineDirectionService;
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
                var nextConnection = currentConnection;
                var sourceVertex = currentConnection.SourceStop;
                currentConnection = vertexFastestConnection
                    .First(p => p.DestinationStop.Stop.Id == sourceVertex.Stop.Id);
                if (_fastestPathTransferService.ShouldBeTransfer(currentConnection, nextConnection))
                {
                    var transferBeetweenVertices = _fastestPathTransferService
                        .GenerateTransferAsStopConnection(currentConnection, nextConnection);
                    fastestPath.Add(transferBeetweenVertices);
                }
                fastestPath.Add(currentConnection);
            }
            fastestPath.Reverse();
            return new FastestPath()
            {
                Path = fastestPath,
                FlattenPath = FlattenFastestPath(fastestPath),
            };
        } 

        private IEnumerable<StopConnection> FlattenFastestPath
            (IList<StopConnection> fastestPath)
        {
            var flattenPath = new List<StopConnection>();
            foreach (var currentConnection  in fastestPath)
            {
                if (flattenPath.Count() > 0)
                {
                    if (!currentConnection.IsTransfer && !flattenPath.Last().IsTransfer)
                    {
                        var currentLine = currentConnection.Line;
                        var lastAddedLine = flattenPath.Last().Line;
                        if (currentLine.Id == lastAddedLine.Id &&
                            _lineDirectionService.GetDirectionStop(currentLine).Id 
                                == _lineDirectionService.GetDirectionStop(lastAddedLine).Id)
                        {
                            var lastAddedConnection = flattenPath.Last();
                            lastAddedConnection.DestinationStop = currentConnection.DestinationStop;
                            lastAddedConnection.EndTime = currentConnection.EndTime;
                        }
                        else
                        {
                            flattenPath.Add(_cloner.CloneFrom(currentConnection));
                        }                        
                    }
                    else
                    {
                        flattenPath.Add(_cloner.CloneFrom(currentConnection));
                    }
                }
                else
                {
                    flattenPath.Add(_cloner.CloneFrom(currentConnection));
                }
            }
            return flattenPath;
        }
    }
}
