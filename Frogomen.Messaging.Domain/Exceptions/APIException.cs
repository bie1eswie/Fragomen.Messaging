using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frogomen.Messaging.Domain.Exceptions
{
    public class APIException : Exception
    {
        public APIException(int statusCode, string details)
        {
            Details = details;
            StatusCode = statusCode;
        }
        public int StatusCode { get; set; }
        public string Details { get; set; }
    }
}
