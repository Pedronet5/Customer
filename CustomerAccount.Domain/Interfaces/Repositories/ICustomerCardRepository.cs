using CustomerAccount.Domain.Models.Entities;
using CustomerAccount.Domain.Models.Requests;
using System;
using System.Threading.Tasks;

namespace CustomerAccount.Domain.Interfaces.Repositories
{
    public interface ICustomerCardRepository
    {
        Task<CustomerCardEntity> PostCustumerAccount(CustomerPostRequest request);
        Task<CustomerCardEntity> GetCustumerAccount(int CardId);
        CustomerCardEntity GetCardByIdAsync(Guid token);
    }
}
