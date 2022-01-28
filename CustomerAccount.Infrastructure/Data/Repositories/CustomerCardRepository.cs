using CustomerAccount.Domain.Interfaces.Repositories;
using CustomerAccount.Domain.Models.Entities;
using CustomerAccount.Domain.Models.Requests;
using CustomerAccount.Domain.Notifications;
using CustomerAccount.Infrastructure.Data.InMemory;
using CustomerAccount.Infrastructure.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAccount.Infrastructure.Data.Repositories
{
    public class CustomerCardRepository : ServiceBase, ICustomerCardRepository
    {
        private readonly InMemoryDbContext _context;
        private readonly INotification _notification;

        public CustomerCardRepository(InMemoryDbContext inMemoryDbContext, INotification notification) : base(notification)
        {
            _context = inMemoryDbContext;
            _notification = notification;
        }

        public async Task<CustomerCardEntity> PostCustumerAccount(CustomerPostRequest request)
        {
            var custumer = _context.CustumerAccount.Find(request.CustomerId);

            if (custumer == null)
            {
                var entity = new CustomerCardEntity(request.CustomerId, request.CardNumber, request.CVV);

                _context.CustumerAccount.Add(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            else
            {
                AddError("Customer already exists!");
            }

            return null;
        }

        public CustomerCardEntity GetCardByIdAsync(Guid token)
        {
            return _context.CustumerAccount.Where(x => x.CardId == token).SingleOrDefault();
        }

        public async Task<CustomerCardEntity> GetCustumerAccount(int CardId)
        {
            var custumer = await _context.CustumerAccount.FindAsync(CardId);
            return custumer;
        }
    }
}
