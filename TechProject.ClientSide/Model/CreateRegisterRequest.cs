using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechProject.ClientSide.Model
{
    public class CreateRegisterRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}