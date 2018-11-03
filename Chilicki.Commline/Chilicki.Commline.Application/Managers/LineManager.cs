using AutoMapper;
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
        readonly LineValidator _lineValidator;
        readonly DepartureRepository _departureRepository;

        public LineManager(
            LineRepository lineRepository, 
            StopManager stopManager, 
            RouteStopRepository routeStopRepository,
            DepartureRepository departureRepository,
            LineValidator lineValidator)
        {
            _lineRepository = lineRepository;
            _stopManager = stopManager;
            _routeStopRepository = routeStopRepository;
            _lineValidator = lineValidator;
            _departureRepository = departureRepository;
        }

        public LineDTO GetById(long id)
        {
            LineDTO lineDTO = Mapper.Map<Line, LineDTO>(_lineRepository.GetById(id));
            lineDTO.Stops = _stopManager.GetAllForLine(id);
            return lineDTO;
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
                StopsWithoutLines = _stopManager.GetAllNotConnectedToAnyLine(),
            };
        }

        public LineDeparturesDTO GetDeparturesForLine(long lineId)
        {
            return new LineDeparturesDTO()
            {
                Line = GetById(lineId),
                Departures = Mapper.Map<
                        IEnumerable<IEnumerable<Departure>>, 
                        IEnumerable<IEnumerable<DepartureDTO>>>
                    (_departureRepository.GetAllLineDeparturesOrderedByRuns(lineId)),              
            };
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
