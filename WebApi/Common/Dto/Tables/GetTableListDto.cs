namespace Common.Dto.Tables
{
    public class GetTableListDto
    {
        public int Id { get; set; }
        public string TableDescription { get; set; }
        public int WaiterId { get; set; }
        public int TableStatusId { get; set; }
    }
}
