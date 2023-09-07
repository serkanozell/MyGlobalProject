namespace MyGlobalProject.Application.Dto.OrderItemDtos
{
    public class OrderItemSummaryDTO
    {
        public List<CreateOrderItemDTO> CreateOrderItems { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime OrderCreateDate { get; set; }
        public decimal TotalPrice
        {
            get { return CreateOrderItems.Sum(x => x.Price * x.Quantity); }
        }
    }
}
