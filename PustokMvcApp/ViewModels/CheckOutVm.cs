namespace MVC_WEB_APP.ViewModels
{
    public class CheckOutVm
    {
        public List<CheckOutItemVm> CheckOutItemVms { get; set; }
        public OrderVm OrderVm { get; set; }
    }

    public class CheckOutItemVm
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public OrderVm OrderVm { get; set; }
    }
    public class OrderVm
    {
       public string TownCity { get; set; }
         public string ZipCode { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
    }
}
