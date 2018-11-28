using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Application.Resources;
using Chilicki.Commline.Application.Validators.Base;
using Chilicki.Commline.Domain.Services.Matching;
using Chilicki.Commline.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Commline.Application.Validators
{
    public class LineValidator : IValidator<LineDTO>, IEditValidator<LineDTO>, IRemoveValidator<LineDTO>
    {
        readonly LineRepository _lineRepository;
        readonly StopLineTypesMatchChecker _stopLineTypesMatchChecker;

        public LineValidator(
            LineRepository lineRepository,
            StopLineTypesMatchChecker stopLineTypesMatchChecker)
        {
            _lineRepository = lineRepository;
            _stopLineTypesMatchChecker = stopLineTypesMatchChecker;
        }

        public bool Validate(LineDTO line)
        {
            if (line == null)
                throw new ArgumentException(ValidationResources.LineIsEmpty);
            if (string.IsNullOrEmpty(line.Name))
                throw new ArgumentException(ValidationResources.LineNameIsEmpty);      
            foreach (var stopType in line.Stops.Select(p => p.StopType))
            {
                if (!_stopLineTypesMatchChecker.AreStopAndLineTypesMatching
                        (stopType, line.LineType))
                    throw new ArgumentException(ValidationResources.StopLineTypesDoNotMatch);                
            }
            if (line.IsCircular)
            {
                if (line.Stops == null || line.Stops.Count() < 3)
                    throw new ArgumentException(ValidationResources.CircularLineStopsLessThanThree);
                if (line.Stops.First().Id != line.Stops.Last().Id)
                    throw new ArgumentException(ValidationResources.CircularLineMustStartAndEndInTheSameStop);
                if (_lineRepository.GetCountByLineName(line.Name) >= 1)
                    throw new ArgumentException(ValidationResources.OnlyOneCircularLineCanExist);
            }
            else
            {
                if (line.Stops == null || line.Stops.Count() < 2)
                    throw new ArgumentException(ValidationResources.LineStopsLessThanTwo);
                if (_lineRepository.GetCountByLineName(line.Name) >= 2)
                    throw new ArgumentException(ValidationResources.OnlyTwoOneWaysLinesCanExist);
            }
            return true;
        }

        public bool Validate(IEnumerable<LineDTO> lineList)
        {
            foreach (var line in lineList)
            {
                Validate(line);
            }
            return true;
        }

        public bool ValidateEdit(LineDTO line)
        {
            Validate(line);
            if (!_lineRepository.DoesLineWithIdExist(line.Id))
                throw new ArgumentException(ValidationResources.LineDoesntExist);
            return true;
        }

        public bool ValidateEdit(IEnumerable<LineDTO> lineList)
        {
            foreach (var line in lineList)
            {
                ValidateEdit(line);
            }
            return true;
        }

        public bool ValidateRemove(LineDTO line)
        {
            if (line == null)
                throw new ArgumentException(ValidationResources.LineIsEmpty);
            if (!_lineRepository.DoesLineWithIdExist(line.Id))
                throw new ArgumentException(ValidationResources.LineDoesntExist);
            return true;
        }

        public bool ValidateRemove(IEnumerable<LineDTO> lineList)
        {
            foreach (var line in lineList)
            {
                ValidateRemove(line);
            }
            return true;
        }
    }
}
