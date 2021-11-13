using Application.Tables.Queries.GetTablesPaged;
using AutoMapper;
using Common.Dto.Tables;
using Common.Models.PagedRequest;
using Domain.Entities;

namespace WebApi.Mappings
{
    public class TableMappingProfile : Profile
    {
        public TableMappingProfile()
        {
            CreateMap<Table, GetTableListDto>();
            CreateMap<Table, GetTableDto>();
            CreateMap<Table, InsertedTableDto>();
            CreateMap<Table, InsertTableDto>();
            CreateMap<Table, UpdatedTableDto>();
            CreateMap<Table, UpdateTableDto>();

            CreateMap<Table, GetTablePagedDto>();
            CreateMap<PaginatedResult<Table>, PaginatedResult<GetTablePagedDto>>()
                .ForMember(x => x.Items,
                    y => y.MapFrom(z => z.Items));


        }

    }
}
