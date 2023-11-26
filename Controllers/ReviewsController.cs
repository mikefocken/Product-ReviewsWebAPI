﻿using Microsoft.AspNetCore.Mvc;
using Product_ReviewWebAPI.Data;
using Product_ReviewWebAPI.DTOs;
using Product_ReviewWebAPI.Models;
using System.Diagnostics.Eventing.Reader;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Product_ReviewWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/Reviews
        [HttpGet]
        public IActionResult Get()
        {
            var reviews = _context.Reviews.ToList();
            return Ok(reviews);
        }

        // GET api/Reviews 
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.Id == id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        //Get api/Reviews/ByProductID/{productId

        [HttpGet("ByProductId/{productId}")]
        public IActionResult GetByProduct(int productId)
        {
            var reviews = _context.Reviews
             .Where(review => review.ProductId == productId)
             .Select(review => new ReviewDTO
             {

                 ReviewId=review.Id,
                 Text = review.Text,
                 Rating = review.Rating,

             })
             .ToList();
            if (reviews.Count ==0) 
            {
                return NotFound("No reviews were found for that specific product.");
            }

            return Ok(reviews);





        }


        // POST api/Reviews
        [HttpPost]
        public IActionResult Post([FromBody] Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return Ok(review);

        }

        // PUT api/Reviews
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Review review)
        {
            var existing=_context.Reviews.FirstOrDefault(r => r.Id == id);
            
            if ( existing == null)
            {
                return NotFound();
            }

            existing.Text= pdatedReview.Text;
            _context.SaveChanges();

            return Ok();

        }

        // DELETE api/Reviews
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var review=_context.Reviews.FirstOrDefault(r=>r.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            _context.SaveChanges();
            return Ok(new { message = "Review was deleted", deletedProductId = id });


        }
    }
}
