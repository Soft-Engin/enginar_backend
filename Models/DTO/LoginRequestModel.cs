using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class LoginRequestModel
    {
        public string Identifier { get; set; }
        public string Password { get; set; }
    }
}
