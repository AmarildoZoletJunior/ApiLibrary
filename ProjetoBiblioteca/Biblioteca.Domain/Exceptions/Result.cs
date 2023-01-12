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
        private Result(bool Ok,string? errorMensagem)
        {
            this.Ok = Ok;
            ErrorMensagem = errorMensagem;
        }

        public static Result OK()
        {
           return new(true, default);
        }
        public static Result Failure(string error)
        {
            return new(false, error);
        }
    }
}
