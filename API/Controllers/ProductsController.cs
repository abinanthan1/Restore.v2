using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(Storecontext context) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<Product>>>GetProducts()
        {
             return await context.Products.ToListAsync();
        }


        [HttpGet("{id}")] //api/products/2
         public async Task<ActionResult<Product>> GetProduct (int id)
        {
            var product = await context.Products.FindAsync(id);

            if (product == null) return NotFound();

            return product;
        }

    }
}
