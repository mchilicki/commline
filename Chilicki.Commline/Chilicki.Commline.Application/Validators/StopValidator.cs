using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Application.Resources;
using Chilicki.Commline.Application.Validators.Base;
using System;
using System.Collections.Generic;

namespace Chilicki.Commline.Application.Validators
{
    public class StopValidator : IValidator<StopDTO>
    {
        public bool Validate(StopDTO stop)
        {
            if (stop == null)
                throw new ArgumentNullException(nameof(stop));
            if (string.IsNullOrWhiteSpace(stop.Name))
                throw new ArgumentException(ValidationResources.StopNameEmpty);
            if (stop.Latitude == 0 && stop.Longitude == 0)
                throw new ArgumentException(ValidationResources.InvalidCoordinates);
            return true;
        }

        public bool Validate(IEnumerable<StopDTO> stopList)
        {
            foreach (var stop in stopList)
            {
                Validate(stop);
            }
            return true;
        }
    }
}
