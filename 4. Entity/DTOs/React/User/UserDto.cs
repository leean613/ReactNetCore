using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.React
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public int Role { get; set; }
    }
}
