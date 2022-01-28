using CustomerAccount.Domain.Models.Requests;
using FluentValidation;

namespace CustomerAccount.Domain.Validation
{
    public class CustomerCardRequestValidation : AbstractValidator<CustomerPostRequest>
    {
        public CustomerCardRequestValidation()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("invalid customerId");

            RuleFor(x => x.CVV)
                .GreaterThan(0).WithMessage("invalid CVV");

            RuleFor(x => x.CardNumber)
                .GreaterThan(0).WithMessage("invalid cardNumber");
        }
    }
}
