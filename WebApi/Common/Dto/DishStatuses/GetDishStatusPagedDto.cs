using System.Collections.Generic;
using Common.Dto.Dishes;

namespace Common.Dto.DishStatuses
{
	public class GetDishStatusPagedDto
	{
        public int Id { get; set; }
        public string DishStatusName { get; set; }
        public ICollection<GetDishDto> Dishes { get; set; }
	}
}
