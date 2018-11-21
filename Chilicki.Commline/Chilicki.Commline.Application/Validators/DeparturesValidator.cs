using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Application.Resources;
using Chilicki.Commline.Application.Validators.Base;
using System;
using System.Collections.Generic;
using System.Linq;

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
                ValidateDeparturesStructure(dto.Line, dto.Departures);
                ValidateDeparturesTimes(dto.Departures);
                if (dto.ReturnLine != null)
                {
                    ValidateDeparturesStructure(dto.Line, dto.ReturnDepartures);
                    ValidateDeparturesTimes(dto.ReturnDepartures);
                }
            }            
            return true;
        }

        public bool Validate(IEnumerable<LineDeparturesDTO> departureList)
        {
            foreach (var departure in departureList)
            {
                Validate(departure);
            }
            return true;
        }

        private bool ValidateDeparturesStructure(LineDTO line, IEnumerable<IEnumerable<DepartureDTO>> departureRuns)
        {
            if (departureRuns.Count() <= 0)
                throw new ArgumentException(ValidationResources.NoDepartures);
            int stopsCount = line.Stops.Count();
            foreach (var departureRun in departureRuns)
            {
                if (departureRun.Count() != stopsCount)
                    throw new ArgumentException(ValidationResources.StopsInDeparturesDoNotMatch);
                foreach (var departure in departureRun)
                {
                    if (departure == null || departure.DepartureTime.Equals(TimeSpan.Zero))
                        throw new ArgumentException(ValidationResources.DepartureEmpty);
                }
            }
            return true;
        }

        private bool ValidateDeparturesTimes(IEnumerable<IEnumerable<DepartureDTO>> departureRuns)
        {
            foreach (var departureRun in departureRuns)
            {
                DepartureDTO previousDeparture = null;
                foreach (var departure in departureRun)
                {
                    if (previousDeparture != null)
                    {
                        if (previousDeparture.DepartureTime >= departure.DepartureTime)
                            throw new ArgumentException(ValidationResources.DeparturesTimesNotCorrect);
                    }
                    previousDeparture = departure;
                }
            }
            return true;
        }
    }
}
