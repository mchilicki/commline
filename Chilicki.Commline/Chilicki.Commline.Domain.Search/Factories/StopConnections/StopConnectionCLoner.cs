using Chilicki.Commline.Domain.Search.Aggregates.Graphs;

namespace Chilicki.Commline.Domain.Search.Factories.StopConnections
{
    public class StopConnectionCloner
    {
        public StopConnection CloneFrom(StopConnection stopConnection)
        {
            return new StopConnection
            {
                DestinationStop = stopConnection.DestinationStop,
                EndTime = stopConnection.EndTime,
                IsTransfer = stopConnection.IsTransfer,
                Line = stopConnection.Line,
                SourceStop = stopConnection.SourceStop,
                StartTime = stopConnection.StartTime
            };
        }
    }
}
