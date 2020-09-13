using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtTokenDemo.Api.Models
{
    public class TokenResult
    {
        public string AccessToken { get; set; }
        public int ExpireInSeconds { get; set; }
        public string UserId { get; set; }
    }
}
