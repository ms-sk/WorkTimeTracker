namespace Core.Dtos
{
    public sealed class Day
    {
        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public decimal? Time { get; set; }

        public decimal? Break { get; set; }
        
        public List<TaskDto>? Tasks { get; set; }
    }
}