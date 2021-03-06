using MyProperty.Data.Entities;
using System;
using System.Linq;

namespace MyProperty.Data
{
    public static class MyPropertyExtention
    {
        public static void EnsureSeedDataForContext(this MyPropertyContext context)
        {

            
            if (context.Owners.Any())
            {
                return;
            }

            // prepare owner object for creation
            var owner = new Owner
            {
                OwnerId = Guid.NewGuid(),
                Email = "superadmin@myproperty.com",
                UserName = "superadmin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("property_1234!"),
                Password= "property_1234!",
                Phone="9999999999",
                CreatedOn = DateTime.Now
            };

            context.Owners.Add(owner);
            context.SaveChanges();

            owner.CreatedBy = owner.OwnerId;
            context.SaveChanges();
        }
    }
}
