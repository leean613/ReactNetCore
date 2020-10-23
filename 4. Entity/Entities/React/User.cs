using Entities.Interfaces;
using System;

namespace Entities.React
{
    public class User : IEntity
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int Role { get; set; }
    }
}
