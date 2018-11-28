using AutoMapper;
using Chilicki.Commline.Application.Correctors;
using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Application.Validators;
using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Repositories;
using System.Collections.Generic;

namespace Chilicki.Commline.Application.Managers
{
    public class LineManager
    {
        readonly LineRepository _lineRepository;
        readonly StopManager _stopManager;
        readonly RouteStopRepository _routeStopRepository;        
        readonly DepartureRepository _departureRepository;
        readonly LineValidator _lineValidator;
        readonly LineCorrector _lineCorrector;

        public LineManager(
            LineRepository lineRepository, 
            StopManager stopManager, 
            RouteStopRepository routeStopRepository,
            DepartureRepository departureRepository,
            LineValidator lineValidator,
            LineCorrector lineCorrector)
        {
            _lineRepository = lineRepository;
            _stopManager = stopManager;
            _routeStopRepository = routeStopRepository;
            _lineValidator = lineValidator;
            _departureRepository = departureRepository;
            _lineCorrector = lineCorrector;
        }

        public LineDTO GetById(long id)
        {
            LineDTO lineDTO = Mapper.Map<Line, LineDTO>(_lineRepository.GetById(id));
            lineDTO.Stops = _stopManager.GetAllForLine(id);
            return lineDTO;
        }

        public LineDTO GetReturnLine(LineDTO lineDTO)
        {
            Line line = _lineRepository.GetById(lineDTO.Id);
            LineDTO returnLineDTO = Mapper.Map<Line, LineDTO>(_lineRepository.GetReturnLine(line));
            if (returnLineDTO != null)
                returnLineDTO.Stops = _stopManager.GetAllForLine(returnLineDTO.Id);
            return returnLineDTO;
        }

        public IEnumerable<LineDTO> GetAll()
        {
            var lineDTOs = Mapper.Map<IEnumerable<Line>, IEnumerable<LineDTO>>
                (_lineRepository.GetAll());
            foreach (var lineDTO in lineDTOs)
            {
                lineDTO.Stops = _stopManager.GetAllForLine(lineDTO);
            }
            return lineDTOs;
        }        

        public AllLinesDTO GetEverything()
        {
            return new AllLinesDTO()
            {
                Lines = GetAll(),
                Stops = _stopManager.GetAll(),
            };
        }

        public LineDeparturesDTO GetDeparturesForLine(long lineId)
        {
            var line = Mapper.Map<Line, LineDTO>(_lineRepository.GetById(lineId));
            var returnLine = GetReturnLine(line);
            IEnumerable<IEnumerable<DepartureDTO>> returnDepartures = null;
            if (returnLine != null)
                returnDepartures = Mapper.Map<
                        IEnumerable<IEnumerable<Departure>>,
                        IEnumerable<IEnumerable<DepartureDTO>>>
                    (_departureRepository.GetAllLineDeparturesOrderedByRuns(returnLine.Id));
            return new LineDeparturesDTO()
            {
                Line = GetById(line.Id),
                Departures = Mapper.Map<
                        IEnumerable<IEnumerable<Departure>>,
                        IEnumerable<IEnumerable<DepartureDTO>>>
                    (_departureRepository.GetAllLineDeparturesOrderedByRuns(line.Id)),
                ReturnLine = returnLine,
                ReturnDepartures = returnDepartures,
            };
        }

        public IEnumerable<LineDTO> GetAllWithoutReturnLines()
        {
            return Mapper.Map<IEnumerable<Line>, IEnumerable<LineDTO>>
                (_lineRepository.GetAllWithoutReturnLines());
        }

        public void Create(IEnumerable<LineDTO> lineDTOs)
        {
            foreach (var lineDTO in lineDTOs)
            {
                Create(lineDTO);
            }
        }

        public void Create(LineDTO lineDTO)
        {
            _lineValidator.Validate(lineDTO);
            lineDTO = _lineCorrector.CorrectLine(lineDTO);
            Line line = Mapper.Map<LineDTO, Line>(lineDTO);            
            _lineRepository.Insert(line);                           
            _routeStopRepository.InsertForLineAndStops(line, 
                Mapper.Map<IEnumerable<StopDTO>, IEnumerable<Stop>>(lineDTO.Stops));
        }

        public void Edit(LineDTO lineDTO)
        {
            _lineValidator.Validate(lineDTO);
            Line line = Mapper.Map<LineDTO, Line>(lineDTO);
            _lineRepository.Update(line);
        }        
    }
}
