using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Application.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Application.Validators
{
    public class LineValidator
    {
        public bool ValidateLine(LineDTO lineDTO)
        {
            if (string.IsNullOrEmpty(lineDTO.Name))
                throw new ArgumentException(ValidationResources.LineNameIsEmpty);
            if (lineDTO.Stops == null || lineDTO.Stops.Count() < 2)
                throw new ArgumentException(ValidationResources.LineStopsLessThanTwo);
            return true;
        }
    }
}
