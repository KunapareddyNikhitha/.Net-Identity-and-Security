namespace Flipkart.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }   
        public string CreatorId { get; set; }
        public ProductStatus Status { get; set; }
    }
}

namespace Flipkart.Models
{
    public enum ProductStatus
    {
        Submitted,
        Approved,
        Rejected
    }
}
