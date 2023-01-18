using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Exceptions
{
    public class Result
    {

        public string ErrorMensagem { get;}
        public bool Ok { get; }
        public string Message { get; }
        private Result(bool Ok,string? errorMensagem, string? message)
        {
            this.Ok = Ok;
            ErrorMensagem = errorMensagem;
            Message = message;
        }

        public static Result OKMessage(string message)
        {
           return new(true, default,message);
        }
        public static Result OK()
        {
            return new(true, default, default);
        }
        public static Result Failure(string error)
        {
            return new(false, error,default);
        }
    }
}
