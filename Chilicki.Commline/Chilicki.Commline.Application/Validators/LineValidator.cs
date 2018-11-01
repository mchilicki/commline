using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Application.Resources;
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
            return true;
        }
    }
}
