namespace Api006.Service.Responses
{
    public class ApiResponse
    {
        public int StatusCode {  get; set; }
        public object? Data { get; set; }
        public string? Message {  get; set; }
    }
}
