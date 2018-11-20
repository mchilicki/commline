using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Domain.Search.Aggregates;
using Chilicki.Commline.Domain.Search.Aggregates.Descriptions;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using Chilicki.Commline.Domain.Search.Resources;
using Chilicki.Commline.Domain.Search.Services.Path;
using Chilicki.Commline.Domain.Search.ValueObjects.Descriptions;
using Chilicki.Commline.Domain.Services.Lines;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Commline.Domain.Search.Services.Descriptions
{
    public class FastestPathDescriptionWriter
    {
        readonly FastestPathTimeCalculator _timeCalculator;
        readonly LineDirectionService _lineDirectionService;

        const string HOUR_FILTER = @"hh\:mm";

        public FastestPathDescriptionWriter(
            FastestPathTimeCalculator timeCalculator,
            LineDirectionService lineDirectionService)
        {
            _timeCalculator = timeCalculator;
            _lineDirectionService = lineDirectionService;
        }

        public FastestPathDescription WriteDescription
            (SearchInput search, FastestPath fastestPath)
        {
            var description = new FastestPathDescription();
            int travelTime = _timeCalculator.CalculateTravelTime(fastestPath);
            description.DescriptionRows.Add(
                WriteHeader(search.StartStop, search.DestinationStop, travelTime));
            foreach(var connection in fastestPath.FlattenPath)
            {
                if (!connection.IsTransfer)
                {
                    description.DescriptionRows = description.DescriptionRows.Concat(WriteLine(connection)).ToList();
                }
                else
                {
                    description.DescriptionRows.Add(WriteWaiting(connection));                    
                }                
            }
            description.DescriptionRows = description.DescriptionRows.Reverse().ToList();
            return description;
        }

        private DescriptionRow WriteHeader
            (Stop sourceStop, Stop destinationStop, int travelTimeInMinutes)
        {
            var header = new DescriptionRow
            {
                First = $"{DescriptionResources.From} {sourceStop.Name}",
                Second = $"{DescriptionResources.To} {destinationStop.Name}",
                Third = $"{travelTimeInMinutes.ToString()} {DescriptionResources.Minutes}",
            };
            return header;
        }

        private IEnumerable<DescriptionRow> WriteLine(StopConnection connection)
        {
            var descriptionRows = new List<DescriptionRow>();
            var departureRow = new DescriptionRow
            {
                First = connection.StartTime.ToString(HOUR_FILTER),
                Second = connection.SourceStop.Stop.Name
            };
            var lineRow = new DescriptionRow
            {
                First = $"{connection.Line.LineType} {connection.Line.Name}",
                Second = $"{DescriptionResources.Direction}: {_lineDirectionService.GetDirectionStop(connection.Line).Name}",
                Third = $"{_timeCalculator.CalculateConnectionTime(connection).ToString()} " +
                    $"{DescriptionResources.Minutes}"
            };
            var arrivalRow = new DescriptionRow
            {
                First = connection.EndTime.ToString(HOUR_FILTER),
                Second = connection.DestinationStop.Stop.Name
            };
            descriptionRows.Add(departureRow);
            descriptionRows.Add(lineRow);
            descriptionRows.Add(arrivalRow);
            return descriptionRows;
        }

        private DescriptionRow WriteWaiting(StopConnection connection)
        {
            return new DescriptionRow
            {
                First = DescriptionResources.Waiting,
                Third = $"{_timeCalculator.CalculateConnectionTime(connection).ToString()} " +
                                $"{ DescriptionResources.Minutes}"
            };
        }

    }
}
