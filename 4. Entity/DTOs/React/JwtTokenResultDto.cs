using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.React
{
    public class JwtTokenResultDto
    {
        public Guid UserId { get; set; }

        public string TokenType { get; set; }

        public string AccessToken { get; set; }

        public int ExpiresInSeconds { get; set; }
    }
}
