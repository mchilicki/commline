using AutoMapper;
using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Domain.Entities;

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
                    .ForMember(dest => dest.RouteStops, opt => opt.Ignore());

                config.CreateMap<Stop, StopDTO>();
                config.CreateMap<StopDTO, Stop>()
                    .ForMember(dest => dest.RouteStops, opt => opt.Ignore());


            });
        }
    }
}