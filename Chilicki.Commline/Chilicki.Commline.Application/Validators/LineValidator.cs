using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Application.Resources;
using Chilicki.Commline.Application.Validators.Base;
using System;
using System.Linq;

namespace Chilicki.Commline.Application.Validators
{
    public class LineValidator : IValidator<LineDTO>
    {
        public bool Validate(LineDTO lineDTO)
        {
            if (string.IsNullOrEmpty(lineDTO.Name))
                throw new ArgumentException(ValidationResources.LineNameIsEmpty);
            if (lineDTO.Stops == null || lineDTO.Stops.Count() < 2)
                throw new ArgumentException(ValidationResources.LineStopsLessThanTwo);
            if (lineDTO.IsCircular)
            {
                if (lineDTO.Stops.Count() < 3)
                    throw new ArgumentException(ValidationResources.CircularLineStopsLessThanThree);
                if (lineDTO.Stops.First().Id != lineDTO.Stops.Last().Id)
                    throw new ArgumentException(ValidationResources.CircularLineMustStartAndEndInTheSameStop);
            }
            return true;
        }
    }
}
