using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class UserListDTO
    {
        public IEnumerable<UserDTO> Users { get; set; }
        public int TotalCount { get; set; }
    }
}
