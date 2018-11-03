using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Application.Resources;
using Chilicki.Commline.Application.Validators.Base;
using Chilicki.Commline.Domain.Services.Matching;
using Chilicki.Commline.Infrastructure.Repositories;
using System;
using System.Linq;

namespace Chilicki.Commline.Application.Validators
{
    public class LineValidator : IValidator<LineDTO>
    {
        readonly LineRepository _lineRepository;
        readonly StopLineTypesMatchCheckingService _stopLineTypesMatchCheckingService;

        public LineValidator(
            LineRepository lineRepository,
            StopLineTypesMatchCheckingService stopLineTypesMatchCheckingService)
        {
            _lineRepository = lineRepository;
            _stopLineTypesMatchCheckingService = stopLineTypesMatchCheckingService;
        }

        public bool Validate(LineDTO lineDTO)
        {
            if (string.IsNullOrEmpty(lineDTO.Name))
                throw new ArgumentException(ValidationResources.LineNameIsEmpty);      
            foreach (var stopType in lineDTO.Stops.Select(p => p.StopType))
            {
                if (!_stopLineTypesMatchCheckingService.AreStopAndLineTypesMatching
                        (stopType, lineDTO.LineType))
                    throw new ArgumentException(ValidationResources.StopLineTypesDoNotMatch);                
            }
            if (lineDTO.IsCircular)
            {
                if (lineDTO.Stops == null || lineDTO.Stops.Count() < 3)
                    throw new ArgumentException(ValidationResources.CircularLineStopsLessThanThree);
                if (lineDTO.Stops.First().Id != lineDTO.Stops.Last().Id)
                    throw new ArgumentException(ValidationResources.CircularLineMustStartAndEndInTheSameStop);
                if (_lineRepository.GetCountByLineName(lineDTO.Name) >= 1)
                    throw new ArgumentException(ValidationResources.OnlyOneCircularLineCanExist);
            }
            else
            {
                if (lineDTO.Stops == null || lineDTO.Stops.Count() < 2)
                    throw new ArgumentException(ValidationResources.LineStopsLessThanTwo);
                if (_lineRepository.GetCountByLineName(lineDTO.Name) >= 2)
                    throw new ArgumentException(ValidationResources.OnlyTwoOneWaysLinesCanExist);
            }
            return true;
        }
    }
}
