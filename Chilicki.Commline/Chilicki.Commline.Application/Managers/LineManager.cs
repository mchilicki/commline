using AutoMapper;
using Chilicki.Commline.Application.Correctors;
using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Application.Validators;
using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Domain.Factories;
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
        readonly LineFactory _lineFactory;

        public LineManager(
            LineRepository lineRepository, 
            StopManager stopManager, 
            RouteStopRepository routeStopRepository,
            DepartureRepository departureRepository,
            LineValidator lineValidator,
            LineCorrector lineCorrector,
            LineFactory lineFactory)
        {
            _lineRepository = lineRepository;
            _stopManager = stopManager;
            _routeStopRepository = routeStopRepository;
            _lineValidator = lineValidator;
            _departureRepository = departureRepository;
            _lineCorrector = lineCorrector;
            _lineFactory = lineFactory;
        }

        public LineDTO GetById(long id)
        {
            LineDTO lineDTO = Mapper.Map<Line, LineDTO>(_lineRepository.Find(id));
            lineDTO.Stops = _stopManager.GetAllForLine(id);
            return lineDTO;
        }

        public LineDTO GetReturnLine(LineDTO lineDTO)
        {
            Line line = _lineRepository.Find(lineDTO.Id);
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
            var line = Mapper.Map<Line, LineDTO>(_lineRepository.Find(lineId));
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
            _lineValidator.Validate(lineDTOs);
            foreach (var lineDTO in lineDTOs)
            {
                Create(lineDTO);
            }
        }

        public void Create(LineDTO lineDTO)
        {            
            lineDTO = _lineCorrector.Correct(lineDTO);
            Line line = Mapper.Map<LineDTO, Line>(lineDTO);            
            _lineRepository.Add(line);                           
            _routeStopRepository.InsertForLineAndStops(line, 
                Mapper.Map<IEnumerable<StopDTO>, IEnumerable<Stop>>(lineDTO.Stops));
        }

        public void Edit(IEnumerable<LineDTO> lineDTOs)
        {
            _lineValidator.ValidateEdit(lineDTOs);
            foreach (var lineDTO in lineDTOs)
            {
                Edit(lineDTO);
            }
        }

        public void Edit(LineDTO lineDTO)
        {            
            var line = _lineRepository.Find(lineDTO.Id);
            _lineFactory.FillIn(line, lineDTO.Name, lineDTO.Color, 
                line.IsCircular, line.LineType, line.Trips);
            _lineRepository.Update(line);
        }

        public void Remove(IEnumerable<LineDTO> lineDTOs)
        {
            _lineValidator.ValidateRemove(lineDTOs);
            foreach (var lineDTO in lineDTOs)
            {
                Remove(lineDTO);
            }
        }

        public void Remove(LineDTO lineDTO)
        {
            var line = _lineRepository.Find(lineDTO.Id);
            _lineRepository.Remove(line);
        }
    }
}
