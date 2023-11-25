using System.ComponentModel.DataAnnotations;

namespace Product_ReviewWebAPI.DTOs
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
       

    }
}
