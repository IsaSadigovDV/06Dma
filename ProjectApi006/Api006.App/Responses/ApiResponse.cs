namespace Api006.App.Responses
{
    public class ApiResponse
    {
        public int StatusCode {  get; set; }
        public object? Data { get; set; }
        public string? Message {  get; set; }
    }
}
