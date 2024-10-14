using System.Net;

namespace Api006.Service.Exceptions.BaseExcep
{
    public class BaseException:Exception
    {
        public HttpStatusCode StatusCode {  get; set; }
        public BaseException(string msg, HttpStatusCode statuscode = HttpStatusCode.InternalServerError)
        {
            StatusCode = statuscode;
        }
    }
}
