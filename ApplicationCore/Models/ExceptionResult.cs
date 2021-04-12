using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class ExceptionResult
    {
        public string StatusCode { get; set; }
        public string Description { get; set; }

        public ExceptionResult()
        {

        }

        public ExceptionResult(string statusCode, string description)
        {
            StatusCode = statusCode;
            Description = description;
        }
    }
}
