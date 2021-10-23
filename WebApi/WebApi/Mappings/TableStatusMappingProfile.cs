using AutoMapper;
using Common.Dto.TableStatuses;
using Domain.Entities;

namespace WebApi.Mappings
{
    public class TableStatusMappingProfile : Profile
    {
        public TableStatusMappingProfile()
        {
            CreateMap<TableStatus, GetTableStatusesListDto>();
            CreateMap<TableStatus, GetTableStatusDto>();
            CreateMap<TableStatus, InsertedTableStatusDto>();
        }
    }
}