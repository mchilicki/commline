using AutoMapper;
using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Application.Managers
{
    public class LineManager
    {
        readonly LineRepository _lineRepository;
        readonly StopManager _stopManager;
        readonly RouteStopRepository _RouteStopRepository;

        public LineManager(LineRepository LineRepository, StopManager stopManager, 
            RouteStopRepository RouteStopRepository)
        {
            _lineRepository = LineRepository;
            _stopManager = stopManager;
            _RouteStopRepository = RouteStopRepository;
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

        public void Create(LineDTO lineDTO)
        {
            Line line = Mapper.Map<LineDTO, Line>(lineDTO);
            _lineRepository.Insert(line);
            _RouteStopRepository.InsertForLineAndStops(line, 
                Mapper.Map<IEnumerable<StopDTO>, IEnumerable<Stop>>(lineDTO.Stops));
        }

        public void Edit(LineDTO lineDTO)
        {
            Line line = Mapper.Map<LineDTO, Line>(lineDTO);
            _lineRepository.Update(line);
        }        
    }
}
