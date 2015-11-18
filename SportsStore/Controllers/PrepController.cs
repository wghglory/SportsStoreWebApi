using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SportsStore.Infrastructure.Identity;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    //this controller and Index.cshtml are to test whether ProductRepository works
    //also test asp.net identity works.
    public class PrepController : Controller
    {

        IRepository repo;

        public PrepController()
        {
            repo = new ProductRepository();
        }
        // GET: Prep
        public ActionResult Index()
        {
            return View(repo.Products);
        }

        [Authorize(Roles = "Administrators")] //identity
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await repo.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Administrators")] //identity
        public async Task<ActionResult> SaveProduct(Product product)
        {
            await repo.SaveProductAsync(product);
            return RedirectToAction("Index");
        }

        public ActionResult Orders()
        {
            return View(repo.Orders);
        }

        public async Task<ActionResult> DeleteOrder(int id)
        {
            await repo.DeleteOrderAsync(id);
            return RedirectToAction("Orders");
        }

        public async Task<ActionResult> SaveOrder(Order order)
        {
            await repo.SaveOrderAsync(order);
            return RedirectToAction("Orders");
        }

        //identity

        public async Task<ActionResult> Signin()
        {
            IAuthenticationManager authMgr = HttpContext.GetOwinContext().Authentication;
            StoreUserManager userMgr = HttpContext.GetOwinContext().GetUserManager<StoreUserManager>();

            StoreUser user = await userMgr.FindAsync("Admin", "secret");
            authMgr.SignIn(await userMgr.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie));
            return RedirectToAction("Index");
        }

        public ActionResult Signout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index");
        }

    }
}