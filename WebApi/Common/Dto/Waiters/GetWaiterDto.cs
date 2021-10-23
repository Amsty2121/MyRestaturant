using System.Collections.Generic;

namespace Common.Dto.Waiters
{
	public class GetWaiterDto
	{
		public int Id { get; set; }
        public string FirstName { get; set; }
		public string LastName { get; set; }
        public ICollection<int> TablesId { get; set; }
        public ICollection<int> OrdersId { get; set; }
	}
}
