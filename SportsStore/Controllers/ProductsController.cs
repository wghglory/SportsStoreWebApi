using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class ProductsController : ApiController
    {
        private IRepository Repository { get; set; }
        public ProductsController()
        {
            //Repository = new ProductRepository();
            Repository =
                (IRepository)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IRepository));
        }

        public IEnumerable<Product> GetProducts()
        {
            return Repository.Products;
        }

        //way 1:
        //public Product GetProduct(int id)
        //{
        //    Product result = Repository.Products.FirstOrDefault(p => p.Id == id);
        //    if (result == null)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);
        //    }

        //    return result;
        //}

        //way 2:
        public IHttpActionResult GetProduct(int id)
        {
            Product result = Repository.Products.FirstOrDefault(p => p.Id == id);
            return result == null ? (IHttpActionResult)BadRequest("no product found") : Ok(result);
        }

        [Authorize(Roles = "Administrators")]
        public async Task<IHttpActionResult> PostProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await Repository.SaveProductAsync(product);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        //async action delete successfully, return nothing: status code 204.
        [Authorize(Roles = "Administrators")]
        public async Task DeleteProduct(int id)
        {
            await Repository.DeleteProductAsync(id);
        }

    }
}
