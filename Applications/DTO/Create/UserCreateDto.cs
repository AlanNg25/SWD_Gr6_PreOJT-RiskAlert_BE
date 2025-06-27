using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.DTO.Create
{
    public class UserCreateDto
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid MajorID { get; set; }
        public string Role { get; set; }
        public int Status { get; set; }
    }
}
