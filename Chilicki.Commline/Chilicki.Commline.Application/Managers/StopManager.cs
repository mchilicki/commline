using AutoMapper;
using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Repositories;
using System.Collections.Generic;

namespace Chilicki.Commline.Application.Managers
{
    public class StopManager
    {
        readonly StopRepository _stopRepository;
        readonly MixedRepository _mixedRepository;

        public StopManager(StopRepository stopRepository, MixedRepository mixedRepository)
        {
            _stopRepository = stopRepository;
            _mixedRepository = mixedRepository;
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

        public IEnumerable<StopDTO> GetAllNotConnectedToAnyLine()
        {
            var stopsDTOs = Mapper.Map<IEnumerable<Stop>, IEnumerable<StopDTO>>
                (_stopRepository.GetAllNotConnectedToAnyLine());
            return stopsDTOs;
        }

        public IEnumerable<StopDTO> GetAllForLine(long id)
        {
            var stopDTOs = Mapper.Map<IEnumerable<Stop>, IEnumerable<StopDTO>>(_mixedRepository.GetAllStopsForLineId(id));
            return stopDTOs;
        }

        public IEnumerable<StopDTO> GetAllForLine(LineDTO lineDTO)
        {
            return GetAllForLine(lineDTO.Id);
        }

        public void Create(IEnumerable<StopDTO> stopDTOs)
        {
            foreach (var stopDTO in stopDTOs)
            {
                Create(stopDTO);
            }
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
