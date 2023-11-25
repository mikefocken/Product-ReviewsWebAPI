using Microsoft.AspNetCore.Mvc;
using Product_ReviewWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Product_ReviewWebAPI.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ZstdSharp.Unsafe;
using Product_ReviewWebAPI.DTOs;


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


        // GET: api/Products?maxPrice=20
        [HttpGet]
        public IActionResult Get([FromQuery] string? maxprice)
        {
            var products = _context.Products.ToList();

            if (!string.IsNullOrEmpty(maxprice) && float.TryParse(maxprice, out float parsePrice))
            {
                products = products.Where(product => product.Price <= parsePrice).ToList();
                return Ok(products);
            }
                       
            return BadRequest("Invalid price format");
        }
        

        // GET api/Product
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _context.Products.Include(product=>product.Reviews).FirstOrDefault(product=>product.Id ==id);

           if (product == null)
           {
                return NotFound();
           }
 
            var productDTO = new ProductDTO
            {
                ProductId= product.Id,
                Name=product.Name,
                Price=product.Price,
                Reviews=product.Reviews?.Select(review => new ReviewDTO
                { 
                    Text=review.Text,
                    Rating=review.Rating,
                })? .ToList() ?? new List <ReviewDTO>(),
              AverageRating =product.Reviews?.Average(review=>review.Rating)??0.0
            };
            
            productDTO.AverageRating=Math.Round(productDTO.AverageRating,2);
            return Ok (productDTO);
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
