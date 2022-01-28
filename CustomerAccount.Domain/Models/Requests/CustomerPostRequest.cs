namespace CustomerAccount.Domain.Models.Requests
{
    public class CustomerPostRequest
    {
        public int CustomerId { get; set; }

        public long CardNumber { get; set; }

        public int CVV { get; set; }
    }
}
