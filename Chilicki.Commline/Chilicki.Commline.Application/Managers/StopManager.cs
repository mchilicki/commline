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
    public class StopManager
    {
        readonly StopRepository _stopRepository;

        public StopManager(StopRepository stopRepository)
        {
            _stopRepository = stopRepository;
        }

        public StopDTO GetById(long id)
        {
            var stopDTO = Mapper.Map<Stop, StopDTO>(_stopRepository.GetById(id));
            return stopDTO;
        }

        public IEnumerable<StopDTO> GetAll()
        {
            var stopDTOs = Mapper.Map<IEnumerable<Stop>, IEnumerable<StopDTO>>(_stopRepository.GetAll());
            return stopDTOs;
        }

        public IEnumerable<StopDTO> GetAllForLine(long id)
        {
            var stopDTOs = Mapper.Map<IEnumerable<Stop>, IEnumerable<StopDTO>>(_stopRepository.GetAllForLineId(id));
            return stopDTOs;
        }

        public IEnumerable<StopDTO> GetAllForLine(LineDTO lineDTO)
        {
            return GetAllForLine(lineDTO.Id);
        }

        public void Create(StopDTO stopDTO)
        {
            Stop stop = Mapper.Map<StopDTO, Stop>(stopDTO);
            _stopRepository.Insert(stop);
        }

        public void Edit(StopDTO stopDTO)
        {
            Stop stop = Mapper.Map<StopDTO, Stop>(stopDTO);
            _stopRepository.Update(stop);
        }
    }
}
