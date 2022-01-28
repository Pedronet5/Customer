using CustomerAccount.Domain.Models.Requests;
using CustomerAccount.Domain.Responses;
using System.Threading.Tasks;

namespace CustomerAccount.Application.Interfaces.Repositories
{
    public interface ICustomerCardService
    {
        Task<CustomerPostResponse> PostCustomerAsync(CustomerPostRequest request);
        bool ValidateToken(CustomerValidateRequest request);
    }
}
