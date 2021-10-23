using System.Collections.Generic;


namespace Common.Dto.OrderStatuses
{
    public class GetOrderStatusPagedDto
    {
        public int Id { get; set; }
        public string OrderStatusName { get; set; }
        //public ICollection<GetOrderDto> Orders { get; set; }
    }
}