namespace tracerapi.DTOs
{
    public class RS<T> where T : class
    {
        public List<T> data { get; set; }
        public int total { get; set; }
    }
}
