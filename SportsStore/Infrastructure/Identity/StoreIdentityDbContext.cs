using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SportsStore.Infrastructure.Identity
{
    public class StoreIdentityDbContext : IdentityDbContext<StoreUser>
    {
        public StoreIdentityDbContext()
            : base("SportsStoreIdentityDb")
        {
            Database.SetInitializer<StoreIdentityDbContext>(new StoreIdentityDbInitializer());
        }

        public static StoreIdentityDbContext Create()
        {
            return new StoreIdentityDbContext();
        }

        //This is similar to the context class I created for the products database, but there are a couple of important differences. First, the class is derived from IdentityDbContext and not DbContext, which is why I don’t need to define any properties to expose the data in the database—everything is provided by the base class. The second difference is that I have defined a Create method. Identity uses a convention of instantiating the classes it needs through static methods that are specified in the configuration file, and the Create method performs that task.
    }
}