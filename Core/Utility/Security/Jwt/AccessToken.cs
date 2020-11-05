using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utility.Security.Jwt
{
    public class AccessToken
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
