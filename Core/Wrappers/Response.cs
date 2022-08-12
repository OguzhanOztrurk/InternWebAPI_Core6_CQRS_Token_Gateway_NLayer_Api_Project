using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Wrappers
{
    public class Response<T> :IResponse
    {
       

        public Response(T data,int errorCode=0, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
            ErrorCode = errorCode;
        }

        



        public int ErrorCode { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}
