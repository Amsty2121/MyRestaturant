﻿using Application.Tables.Commands.UpdateTable;
using Application.Tables.Queries.GetTableById;
using Application.Tables.Queries.GetTableList;
using AutoMapper;
using Common.Dto.Tables;
using Domain.Entities;

namespace WebApi.Mappings
{
    public class TableMappingProfile : Profile
    {
        public TableMappingProfile()
        {
            CreateMap<TablesWithStatusesAndWaiters, GetTableListDto>();
            CreateMap<Table, GetTableDto>();
            CreateMap<TableWithStatusWaiterAndOrders, GetTableDto>();
            CreateMap<Table, InsertedTableDto>();
            CreateMap<Table, InsertTableDto>();
            CreateMap<TableUpdating, UpdatedTableDto>();
            CreateMap<Table, UpdateTableDto>();
        }

    }
}
