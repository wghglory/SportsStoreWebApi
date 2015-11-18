using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace SportsStore.Infrastructure.Identity
{
    public class StoreUserManager : UserManager<StoreUser>
    {
        //Some of the Members Defined by the UserManager<T> Class
        //Create(user,pass) Find(user,pass) FindByName(name)    IsInRole(user,role) Users

        public static StoreUserManager Create(IdentityFactoryOptions<StoreUserManager> options, IOwinContext context)
        {
            StoreIdentityDbContext dbContext = context.Get<StoreIdentityDbContext>();
            StoreUserManager manager = new StoreUserManager(new UserStore<StoreUser>(dbContext));

            return manager;
        }

        
        public StoreUserManager(IUserStore<StoreUser> store)
            : base(store)
        {
        }
    }
}