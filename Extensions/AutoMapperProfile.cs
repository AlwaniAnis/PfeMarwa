using AutoMapper;
using tracerapi.DTOs;
using tracerapi.Models;

namespace tracerapi.Extensions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Incident, IncidentPostModel>().ReverseMap(); //reverse so the both direction
            CreateMap<Incident, IncidentPutModel>().ReverseMap();
            CreateMap<Intervention, InterventionPostModel>().ReverseMap(); 
            CreateMap<Intervention, InterventionPutModel>().ReverseMap();
            CreateMap<Tache, TachePostModel>().ReverseMap(); 
            CreateMap<Tache, TachePutModel>().ReverseMap();
        }
    }
    
}
