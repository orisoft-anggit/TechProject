using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechProject.ClientSide.Model
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}