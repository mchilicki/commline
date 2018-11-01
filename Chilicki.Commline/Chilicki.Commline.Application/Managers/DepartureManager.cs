using AutoMapper;
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

        public DepartureManager(
            DepartureRepository departureRepository,
            DeparturesValidator departuresValidator,
            LineRepository lineRepository)
        {
            _departureRepository = departureRepository;
            _departuresValidator = departuresValidator;
            _lineRepository = lineRepository;
        }

        public void ChangeLineDepartures(LineDeparturesDTO lineDepartures)
        {
            _departuresValidator.Validate(lineDepartures);
            Line line = _lineRepository.GetById(lineDepartures.Line.Id);
            var departures = Mapper.Map
                <IEnumerable<IEnumerable<DepartureDTO>>,
                IEnumerable<IEnumerable<Departure>>>
                (lineDepartures.Departures);
            _departureRepository.DeleteAllDeparturesForLine(line);
            _departureRepository.Create(line, departures);
        }
    }
}
