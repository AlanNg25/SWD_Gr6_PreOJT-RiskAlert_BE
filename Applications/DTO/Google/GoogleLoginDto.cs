using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.DTO.Google
{
    public class GoogleLoginDto
    {
        public string IdToken { get; set; } = string.Empty; // token Google trả về cho client
    }
}
