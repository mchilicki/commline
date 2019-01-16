using AutoMapper;
using Chilicki.Commline.Application.DTOs;
using Chilicki.Commline.Application.Search.DTOs;
using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Domain.Search.Aggregates;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;

namespace Chilicki.Commline.UserInterface
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Line, LineDTO>();
            CreateMap<LineDTO, Line>()
                .ForMember(dest => dest.RouteStops, opt => opt.Ignore());

            CreateMap<Stop, StopDTO>()
            .ForMember(dest => dest.SiteNumber, opt => opt.MapFrom(src => src.StopNumber));
            CreateMap<StopDTO, Stop>()
                .ForMember(dest => dest.RouteStops, opt => opt.Ignore())
                .ForMember(dest => dest.StopNumber, opt => opt.MapFrom(src => src.SiteNumber));

            CreateMap<Departure, DepartureDTO>();
            CreateMap<DepartureDTO, Departure>();

            CreateMap<RouteStop, RouteStopDTO>();
            CreateMap<RouteStopDTO, RouteStop>();

            CreateMap<StopConnection, StopConnectionDTO>();
            CreateMap<StopVertex, StopDTO>()
                .ConvertUsing(source => Mapper.Map<Stop, StopDTO>(source.Stop));
            CreateMap<FastestPath, FastestPathDTO>()
                .ForMember(dest => dest.PathDescription, opt => opt.Ignore());           
        }
    }
}
