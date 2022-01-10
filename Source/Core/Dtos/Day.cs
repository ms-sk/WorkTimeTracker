namespace Dtos
{
    public sealed class Day
    {
        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public decimal? Time { get; set; }

        public decimal? Break { get; set; }

        public override string ToString()
        {
            var start = Start.HasValue ? Start.Value.ToShortDateString() : string.Empty;
            var startTime = Start.HasValue ? Start.Value.ToShortTimeString() : string.Empty;
            var end = End.HasValue ? End.Value.ToShortTimeString() : "<no end found.>";
            var workTime = Time.HasValue ? Time.ToString() : "0.0";
            return $"{start} - {startTime} - {end} - {workTime}";
        }
    }
}