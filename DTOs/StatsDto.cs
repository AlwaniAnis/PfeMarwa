namespace tracerapi.DTOs
{
    public class StatsDto
    {
        public int ClosedTachesCount { get; set; }=0;
        public int ClosedIncidentsCount { get; set; } = 0;
        public int ClosedinterventionsCount { get; set; } = 0;
        public int OpenedTachesCount { get; set; } = 0;
        public int OpenedIncidentsCount { get; set; } = 0;
        public int OpenedinterventionsCount { get; set; } = 0;
    }
}
