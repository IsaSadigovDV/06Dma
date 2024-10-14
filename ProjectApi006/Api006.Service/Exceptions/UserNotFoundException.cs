using Api006.Service.Exceptions.BaseExcep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Api006.Service.Exceptions
{
    public class UserNotFoundException : BaseException
    {
        public UserNotFoundException(string msg, HttpStatusCode statuscode = HttpStatusCode.InternalServerError) : base(msg, statuscode)
        {
        }
    }
}
