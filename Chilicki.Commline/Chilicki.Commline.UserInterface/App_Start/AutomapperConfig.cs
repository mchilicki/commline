using AutoMapper;
using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chilicki.Commline.UserInterface.App_Start
{
    public class AutomapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Line, LineDTO>();
                config.CreateMap<LineDTO, Line>()
                    .ForMember(dest => dest.RouteStations, opt => opt.Ignore());

                config.CreateMap<Stop, StopDTO>();
                config.CreateMap<StopDTO, Stop>()
                    .ForMember(dest => dest.RouteStations, opt => opt.Ignore());


            });
        }
    }
}