using Entities.React;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EntityFrameworkCore.Contexts
{
    public static class DbInitializer
    {
        public static void Initialize(ReactDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            context.Users.Add(new User
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                UserName = "admin",
                Password = EncryptPassword("1"),
                Role = 1,
            });

            context.SaveChanges();
        }

        public static string EncryptPassword(string passsword)
        {
            return Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(passsword)));
        }
    }
}
