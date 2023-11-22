using Microsoft.AspNetCore.Mvc;
using Product_ReviewWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Product_ReviewWebAPI.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Product_ReviewWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: api/Products
        [HttpGet]
        public IActionResult Get()
        {
            var products = _context.Products.ToList();
            return Ok(products);

        }

        // GET api/Products
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _context.Products.Find(id);

           if (product == null)
           {
                return NotFound();
           }
           
            return Ok (product);
        }

        // POST api/Products
        [HttpPost]
        public IActionResult Post([FromBody] Product product) 
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return StatusCode(201,product);
        }

        // PUT api/Products
        [HttpPut("{id}")]
        public IActionResult Put (int id, [FromBody] Product product)
        {
            var productFromDb=_context.Products.Find(id);
            if (productFromDb== null)
            {
                return NotFound();
            }
          
            /// update these items for Product
            productFromDb.Name= product.Name;
            productFromDb.Price= product.Price;
            
            _context.Products.Update(productFromDb);
            _context.SaveChanges();

            return Ok(product);

        }

        // DELETE api/Products
     
   [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            var productFromDb=_context.Products.Find(id);
            //var product =_context.Products.FirstOrDefault(product=>product.Id == id);

            if (productFromDb== null)
            {
                return NotFound();
            }

            _context.Products.Remove(productFromDb);
            _context.SaveChanges();
            return Ok(new {message = "Product was deleted",deletedProductId =id});

        }
    }
}
