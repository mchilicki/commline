using AutoMapper;
using Chilicki.Commline.Application.Correctors;
using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Application.Validators;
using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Repositories;
using System.Collections.Generic;

namespace Chilicki.Commline.Application.Managers
{
    public class DepartureManager
    {
        readonly DepartureRepository _departureRepository;
        readonly DeparturesValidator _departuresValidator;
        readonly LineRepository _lineRepository;
        readonly DepartureRunCorrector _departureRunCorrector;

        public DepartureManager(
            DepartureRepository departureRepository,
            DeparturesValidator departuresValidator,
            LineRepository lineRepository,
            DepartureRunCorrector departureRunCorrector)
        {
            _departureRepository = departureRepository;
            _departuresValidator = departuresValidator;
            _lineRepository = lineRepository;
            _departureRunCorrector = departureRunCorrector;
        }

        public void ChangeLineDepartures(LineDeparturesDTO lineDepartures)
        {
            lineDepartures.Departures = lineDepartures.Departures ?? new List<List<DepartureDTO>>();
            lineDepartures.ReturnDepartures = lineDepartures.ReturnDepartures ?? new List<List<DepartureDTO>>();
            _departuresValidator.Validate(lineDepartures);
            _departureRunCorrector.Correct(lineDepartures);
            ChangeLineDeparturesFor(lineDepartures.Line, lineDepartures.Departures);
            if (lineDepartures.ReturnLine != null)
                ChangeLineDeparturesFor(lineDepartures.ReturnLine, lineDepartures.ReturnDepartures);
        }

        private void ChangeLineDeparturesFor(LineDTO lineDTO, IEnumerable<IEnumerable<DepartureDTO>> departuresDTO)
        {
            Line line = _lineRepository.Find(lineDTO.Id);
            var departures = Mapper.Map
                <IEnumerable<IEnumerable<DepartureDTO>>,
                IEnumerable<IEnumerable<Departure>>>
                (departuresDTO);
            _departureRepository.DeleteAllDeparturesForLine(line);
            _departureRepository.Create(line, departures);
        }
    }
}
