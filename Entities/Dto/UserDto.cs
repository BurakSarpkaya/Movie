using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public byte[] PassWordHash { get; set; }
        public byte[] PassWordSalt { get; set; }
    }
}
