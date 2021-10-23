using System.Collections.Generic;

namespace Common.Dto.Tables
{
	public class InsertedTableDto
	{
		public int Id { get; set; }
        public string TableDescription { get; set; }
        public int WaiterId { get; set; }
        public int TableStatusId { get; set; }
    }
}
