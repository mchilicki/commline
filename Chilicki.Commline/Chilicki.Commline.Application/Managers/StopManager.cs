using AutoMapper;
using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Application.Validators;
using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Domain.Factories;
using Chilicki.Commline.Infrastructure.Repositories;
using System.Collections.Generic;

namespace Chilicki.Commline.Application.Managers
{
    public class StopManager
    {
        readonly StopRepository _stopRepository;
        readonly MixedRepository _mixedRepository;
        readonly StopValidator _stopValidator;
        readonly StopFactory _stopFactory;

        public StopManager(
            StopRepository stopRepository, 
            MixedRepository mixedRepository,
            StopValidator stopValidator,
            StopFactory stopFactory)
        {
            _stopRepository = stopRepository;
            _mixedRepository = mixedRepository;
            _stopValidator = stopValidator;
            _stopFactory = stopFactory;
        }

        public StopDTO GetById(long id)
        {
            var stopDTO = Mapper.Map<Stop, StopDTO>(_stopRepository.Find(id));
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
            var stop = Mapper.Map<StopDTO, Stop>(stopDTO);
            stop.StopNumber = _stopRepository.GetNextStopNumberForStopName(stopDTO.Name);
            _stopRepository.Add(stop);
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
            var stop = _stopRepository.Find(stopDTO.Id);
            int newStationNumber = _stopRepository.GetNextStopNumberForStopName(stopDTO.Name);
            stop = _stopFactory.FillIn(stop, stopDTO.Name, stopDTO.Latitude, 
                stopDTO.Longitude, stopDTO.StopType, newStationNumber);
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
            var stop = _stopRepository.Find(stopDTO.Id);
            _stopRepository.Remove(stop);
        }
    }
}
