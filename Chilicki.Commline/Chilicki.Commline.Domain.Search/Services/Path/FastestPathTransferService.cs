using Chilicki.Commline.Domain.Search.Aggregates;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;

namespace Chilicki.Commline.Domain.Search.Services.Path
{
    public class FastestPathTransferService
    { 
        public bool IsAlreadyTransfer(StopConnection currentConnection)
        {
            return currentConnection.IsTransfer;
        }

        public bool ShouldBeTransfer
            (StopConnection sourceConnection, StopConnection nextConnection)
        {
            return sourceConnection.Line.Id != nextConnection.Line.Id;
        }

        public StopConnection GenerateTransferAsStopConnection
            (StopConnection sourceConnection, StopConnection nextConnection)
        {
            return new StopConnection()
            {
                SourceStop = sourceConnection.DestinationStop,
                DestinationStop = sourceConnection.DestinationStop,
                StartDateTime = sourceConnection.EndDateTime,
                EndDateTime = nextConnection.StartDateTime,
                Line = null,
                IsTransfer = true,
            };
        }

        public bool ShouldBeWaitingOnFirstStop
            (SearchInput search, StopConnection firstConnection)
        {
            return search.StartFullDate != firstConnection.StartDateTime;
        }

        public StopConnection GenerateWaitingAsStopConnection
            (SearchInput search, StopConnection firstConnection)
        {
            return new StopConnection()
            {
                SourceStop = firstConnection.SourceStop,
                DestinationStop = firstConnection.SourceStop,
                StartDateTime = search.StartFullDate,
                EndDateTime = firstConnection.StartDateTime,
                Line = null,
                IsTransfer = true,
            };
        }
    }
}
