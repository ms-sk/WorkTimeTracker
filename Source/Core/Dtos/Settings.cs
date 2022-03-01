using Core.Models;

namespace Core.Dtos
{
    public sealed class Settings
    {
        public Settings()
        {
            Filter = Filter.None;
            HoursPerDay = 8.0;
        }

        public Filter Filter { get; set; }

        public double HoursPerDay { get; set; }
    }
}
