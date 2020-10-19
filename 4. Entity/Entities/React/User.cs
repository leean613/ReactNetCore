using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.React
{
    public class User : IEntity
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
