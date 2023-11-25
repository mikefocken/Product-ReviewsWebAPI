using System.ComponentModel.DataAnnotations;

namespace Product_ReviewWebAPI.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public ICollection<ReviewDTO>Reviews { get; set; }
        public double AverageRating { get; set; }
    }
}
