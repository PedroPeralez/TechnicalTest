using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TT.Models.BusinessEntities;

namespace TT.Context
{
    public class TTContext : DbContext
    {
        // This method allows for the context to be used as static, eliminating the need
        // to instantiate every time we need to use it
        public static TTContext SharedTTContext; 

        public TTContext() : base()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TTContext>());
        }

        //More database sets can be added in the future if more entities were added to the models folder
        public DbSet<User> Users { get; set; }
    }
}