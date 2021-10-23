using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Dto.Tables
{
	public class InsertTableDto
	{
        [StringLength(500, MinimumLength = 0, ErrorMessage = "Invalid TableDescription length")]
        public string TableDescription { get; set; }
        public int WaiterId { get; set; }
        public int TableStatusId { get; set; }
    }
}
