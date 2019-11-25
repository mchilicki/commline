using AutoMapper;
using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Application.Search.DTOs;
using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Domain.Search.Aggregates;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;

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
                    .ForMember(dest => dest.Trips, opt => opt.Ignore());

                config.CreateMap<Stop, StopDTO>()
                    .ForMember(dest => dest.SiteNumber, opt => opt.MapFrom(src => src.StopNumber));
                config.CreateMap<StopDTO, Stop>()
                    .ForMember(dest => dest.Trips, opt => opt.Ignore())
                    .ForMember(dest => dest.StopNumber, opt => opt.MapFrom(src => src.SiteNumber));

                config.CreateMap<Departure, DepartureDTO>();
                config.CreateMap<DepartureDTO, Departure>();

                config.CreateMap<StopConnection, StopConnectionDTO>();
                config.CreateMap<StopVertex, StopDTO>()
                    .ConvertUsing(source => Mapper.Map<Stop, StopDTO>(source.Stop));
                config.CreateMap<FastestPath, FastestPathDTO>()
                    .ForMember(dest => dest.PathDescription, opt => opt.Ignore());
            });
        }
    }
}