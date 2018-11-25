using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Application.Resources;
using Chilicki.Commline.Application.Validators.Base;
using Chilicki.Commline.Infrastructure.Repositories;
using System;
using System.Collections.Generic;

namespace Chilicki.Commline.Application.Validators
{
    public class StopValidator : IValidator<StopDTO>, IEditValidator<StopDTO>, IRemoveValidator<StopDTO>
    {
        readonly StopRepository _stopRepository;

        public StopValidator(
            StopRepository stopRepository)
        {
            _stopRepository = stopRepository;
        }

        public bool Validate(StopDTO stop)
        {
            if (stop == null)
                throw new ArgumentNullException(nameof(stop));
            if (string.IsNullOrWhiteSpace(stop.Name))
                throw new ArgumentException(ValidationResources.StopNameEmpty);
            if (stop.Latitude == 0 && stop.Longitude == 0)
                throw new ArgumentException(ValidationResources.InvalidCoordinates);
            return true;
        }

        public bool Validate(IEnumerable<StopDTO> stopList)
        {
            foreach (var stop in stopList)
            {
                Validate(stop);
            }
            return true;
        }

        public bool ValidateEdit(StopDTO stop)
        {
            if (stop == null)
                throw new ArgumentNullException(nameof(stop));
            if (string.IsNullOrWhiteSpace(stop.Name))
                throw new ArgumentException(ValidationResources.StopNameEmpty);
            if (stop.Latitude == 0 && stop.Longitude == 0)
                throw new ArgumentException(ValidationResources.InvalidCoordinates);
            if (!_stopRepository.DoesStopWithIdExist(stop.Id))
                throw new ArgumentException(ValidationResources.StopDoesntExist);
            if (_stopRepository.GetById(stop.Id).StopType != stop.StopType &&
                _stopRepository.IsStopConnectedToAnyLine(stop.Id))
                throw new ArgumentException(ValidationResources.StopTypeCannotBeEditedWhenItsInPreviousTypeLine);
            //if (_stopRepository.IsStopConnectedToAnyLine(stop.Id))
            //    throw new ArgumentException(ValidationResources.StopIsConnectedToLine);
            return true;
        }

        public bool ValidateEdit(IEnumerable<StopDTO> stopList)
        {
            foreach (var stop in stopList)
            {
                ValidateEdit(stop);
            }
            return true;
        }

        public bool ValidateRemove(StopDTO stop)
        {
            if (stop == null)
                throw new ArgumentNullException(nameof(stop));
            if (!_stopRepository.DoesStopWithIdExist(stop.Id))
                throw new ArgumentException(ValidationResources.StopDoesntExist);
            if (_stopRepository.IsStopConnectedToAnyLine(stop.Id))
                throw new ArgumentException(ValidationResources.StopIsConnectedToLine);
            return true;
        }

        public bool ValidateRemove(IEnumerable<StopDTO> stopList)
        {
            foreach (var stop in stopList)
            {
                ValidateRemove(stop);
            }
            return true;
        }
    }
}
