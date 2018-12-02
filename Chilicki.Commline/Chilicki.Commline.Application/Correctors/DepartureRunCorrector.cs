using Chilicki.Commline.Application.Correctors.Base;
using Chilicki.Commline.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                departureRun.Last().IsBetweenDays = true;
                bool shouldRestBeTrue = false;
                foreach(var departure in departureRun)
                {
                    if (lastDeparture != null)
                    {
                        if (!shouldRestBeTrue)
                        {
                            if (lastDeparture.DepartureTime < departure.DepartureTime)
                            {
                                lastDeparture.IsBetweenDays = false;
                            }
                            else
                            {
                                shouldRestBeTrue = true;
                            }
                        }
                        else
                        {
                            lastDeparture.IsBetweenDays = true;
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
                .Where(p => p.IsBetweenDays == true)
                .Count() > 0;
        }
    }
}
