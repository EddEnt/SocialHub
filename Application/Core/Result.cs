using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core
{
    public class Result<T>
    {
        public bool IsSuccessful { get; set; }
        //T in this context is the type of the value that we want to return
        public T Value { get; set; }
        public string Error { get; set; }

        public static Result<T> Success(T value) => new Result<T> 
        { 
            IsSuccessful = true, Value = value 
        };

        public static Result<T> Failure(string error) => new Result<T>
        {
            IsSuccessful = false, Error = error 
        };
    }
}
