﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.React.User
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public string UserName { get; set; }

        public int Role { get; set; }
    }
}
