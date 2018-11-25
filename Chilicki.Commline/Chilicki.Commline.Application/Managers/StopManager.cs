using AutoMapper;
using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Application.Validators;
using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Repositories;
using System.Collections.Generic;

namespace Chilicki.Commline.Application.Managers
{
    public class StopManager
    {
        readonly StopRepository _stopRepository;
        readonly MixedRepository _mixedRepository;
        readonly StopValidator _stopValidator;

        public StopManager(
            StopRepository stopRepository, 
            MixedRepository mixedRepository,
            StopValidator stopValidator)
        {
            _stopRepository = stopRepository;
            _mixedRepository = mixedRepository;
            _stopValidator = stopValidator;
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

        public IEnumerable<StopDTO> GetAllConnectedToAnyLine()
        {
            var stopsDTOs = Mapper.Map<IEnumerable<Stop>, IEnumerable<StopDTO>>
                (_stopRepository.GetAllConnectedToAnyLine());
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
            _stopValidator.Validate(stopDTOs);
            foreach (var stopDTO in stopDTOs)
            {
                Create(stopDTO);
            }
        }

        public void Create(StopDTO stopDTO)
        {
            Stop stop = Mapper.Map<StopDTO, Stop>(stopDTO);
            stop.StopNumber = _stopRepository.GetNextStopNumberForStopName(stopDTO.Name);
            _stopRepository.Insert(stop);
        }

        public void Edit(IEnumerable<StopDTO> stopDTOs)
        {
            _stopValidator.ValidateEdit(stopDTOs);
            foreach (var stopDTO in stopDTOs)
            {
                Edit(stopDTO);
            }
        }

        public void Edit(StopDTO stopDTO)
        {            
            Stop stop = _stopRepository.GetById(stopDTO.Id);
            stop.Name = stopDTO.Name;
            stop.Latitude = stopDTO.Latitude;
            stop.Longitude = stopDTO.Longitude;
            stop.StopType = stopDTO.StopType;
            stop.StopNumber = _stopRepository.GetNextStopNumberForStopName(stopDTO.Name);
            _stopRepository.Update(stop);
        }

        public void Remove(IEnumerable<StopDTO> stopDTOs)
        {
            _stopValidator.ValidateRemove(stopDTOs);
            foreach (var stopDTO in stopDTOs)
            {
                Remove(stopDTO);
            }
        }

        public void Remove(StopDTO stopDTO)
        {
            Stop stop = _stopRepository.GetById(stopDTO.Id);
            _stopRepository.Remove(stop);
        }
    }
}
