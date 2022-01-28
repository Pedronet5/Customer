using System;

namespace CustomerAccount.Domain.Models.Requests
{
    public class CustomerValidateRequest
    {
        public int CustomerId { get; set; }
        public Guid CardId { get; set; }
        public string Token { get; set; }
        public int CVV { get; set; }
    }
}
