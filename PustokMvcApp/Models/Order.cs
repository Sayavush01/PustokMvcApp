using Microsoft.Identity.Client;

namespace MVC_WEB_APP.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string TownCity { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }

        public OrderStatus Status { get; set; }
            public Order()
            {
                OrderItems = new List<OrderItem>();
                Status = OrderStatus.Pending;
        }
    }
    public enum OrderStatus
    {
        Pending,
        Completed,
        Cancelled
    }
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public int Count { get; set; }
        public Guid OrderId { get; set; }
        public decimal Price { get; set; }
        public Order Order { get; set; }
      
}
}
