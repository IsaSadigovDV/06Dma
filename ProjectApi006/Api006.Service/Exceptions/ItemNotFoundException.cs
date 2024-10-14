using Api006.Service.Exceptions.BaseExcep;
using System.Net;

namespace Api006.Service.Exceptions
{
    public class ItemNotFoundException : BaseException
    {
        public ItemNotFoundException(string msg, HttpStatusCode statuscode = HttpStatusCode.NotFound) : base(msg, statuscode)
        {
        }
    }
}
