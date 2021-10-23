using System.Collections.Generic;

namespace Common.Dto.Tables
{
	public class GetTableDto
	{
		public int Id { get; set; }
        public string TableDescription { get; set; }
		public int WaiterId { get; set; }
        public string WaiterName { get; set; }
		public int TableStatusId { get; set; }
        public string TableStatusName { get; set; }
		public ICollection<int> OrdersId { get; set; }

	}
}
