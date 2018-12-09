using Chilicki.Commline.Application.Correctors.Base;
using Chilicki.Commline.Application.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Commline.Application.Correctors
{
    public class DepartureRunCorrector : ICorrector<LineDeparturesDTO>
    {
        public LineDeparturesDTO Correct(LineDeparturesDTO departures)
        {
            foreach (var departureRun in departures.Departures)
            {
                CorrectRun(departureRun);
            }
            foreach (var departureRun in departures.ReturnDepartures)
            {
                CorrectRun(departureRun);
            }
            return departures;
        }

        private IEnumerable<DepartureDTO> CorrectRun(IEnumerable<DepartureDTO> departureRun)
        {
            if (ShoulRunBeCorrected(departureRun))
            {
                DepartureDTO lastDeparture = null;
                departureRun.Last().IsNextDay = true;
                bool shouldRestBeOnNextDay = false;
                foreach(var departure in departureRun)
                {
                    if (lastDeparture != null)
                    {
                        if (!shouldRestBeOnNextDay)
                        {
                            if (lastDeparture.DepartureTime < departure.DepartureTime)
                            {
                                lastDeparture.IsNextDay = false;
                            }
                            else
                            {
                                lastDeparture.IsNextDay = false;
                                lastDeparture.IsBetweenDays = true;
                                shouldRestBeOnNextDay = true;
                            }
                        }
                        else
                        {
                            lastDeparture.IsNextDay = true;
                        }                        
                    }
                    lastDeparture = departure;
                }                
            }
            return departureRun;
        }

        private bool ShoulRunBeCorrected(IEnumerable<DepartureDTO> departureRun)
        {
            return departureRun
                .Where(p => p.IsNextDay == true)
                .Count() > 0;
        }
    }
}
