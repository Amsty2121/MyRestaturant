using System.ComponentModel.DataAnnotations;

namespace Common.Dto.TableStatuses
{
    public class InsertTableStatusDto
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Invalid TableStatusName length")]
        public string TableStatusName { get; set; }
    }
}