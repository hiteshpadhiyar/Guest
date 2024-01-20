namespace Guest.Models
{
    public class APIResponse
    {
        public int statusCode { get; set; }
        public object error { get; set; }
        public object result { get; set; }
        public string message { get; set; }
    }
}
