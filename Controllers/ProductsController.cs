using Microsoft.AspNetCore.Mvc;
using Product_ReviewWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Product_ReviewWebAPI.Models;


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
            var movies = _context.Products.ToList();


            return Ok();

        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/Products
        [HttpPost]
        public IActionResult Post([FromBody] Product product) 
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }

        // PUT api/Products
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
