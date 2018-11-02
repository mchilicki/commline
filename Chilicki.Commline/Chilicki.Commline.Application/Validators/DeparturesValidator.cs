using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Application.Resources;
using Chilicki.Commline.Application.Validators.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Application.Validators
{
    public class DeparturesValidator : IValidator<LineDeparturesDTO>
    {
        public bool Validate(LineDeparturesDTO dto)
        {
            if (dto == null || dto.Line == null)
                throw new ArgumentNullException(nameof(dto));
            if (dto.Departures != null)
            {
                if (dto.Departures.Count() <= 0)
                    throw new ArgumentException(ValidationResources.NoDepartures);
                int stopsCount = dto.Line.Stops.Count();
                foreach (var departureRun in dto.Departures)
                {
                    if (departureRun.Count() != stopsCount)
                        throw new ArgumentException(ValidationResources.StopsInDeparturesDoNotMatch);
                    foreach (var departure in departureRun)
                    {
                        if (departure == null || departure.DepartureTime.Equals(TimeSpan.Zero))
                            throw new ArgumentException(ValidationResources.DepartureEmpty);
                    }
                }
            }            
            return true;
        }
    }
}
