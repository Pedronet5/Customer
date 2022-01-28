using AutoMapper;
using CustomerAccount.Application.Interfaces.Repositories;
using CustomerAccount.Domain.Interfaces.Repositories;
using CustomerAccount.Domain.Models.Entities;
using CustomerAccount.Domain.Models.Requests;
using CustomerAccount.Domain.Notifications;
using CustomerAccount.Domain.Responses;
using CustomerAccount.Domain.Validation;
using CustomerAccount.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CustomerAccount.Application.Services
{
    public class CustomerCardService : ServiceBase, ICustomerCardService
    {
        private readonly ICustomerCardRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerCardService> _logger;

        public CustomerCardService(INotification notification, ICustomerCardRepository customerRepository, IMapper mapper, ILogger<CustomerCardService> logger) : base(notification)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<CustomerPostResponse> PostCustomerAsync(CustomerPostRequest request)
        {
            var validation = new CustomerCardRequestValidation().Validate(request);

            if (!validation.IsValid)
            {
                AddErrors(validation);
                return null;
            }

            var response = _mapper.Map<CustomerPostResponse>(await _customerRepository.PostCustumerAccount(request));
            return response;
        }

        public bool ValidateToken(CustomerValidateRequest request)
        {
            var card = _customerRepository.GetCardByIdAsync(request.CardId);

            if (CardNotFound(card)) return false;

            if (CardBelongsToAnotherCustomer(request, card)) return false;

            if (CardWasIssuedLongTimeAgo(card)) return false;

            if (TokenOrCVVInvalid(request, card)) return false;

            _logger.LogInformation($"Card number {card.CardNumber}");

            return true;
        }

        private static bool CardWasIssuedLongTimeAgo(CustomerCardEntity entity)
        {
            return (DateTime.UtcNow - entity.CreateDate).Minutes >= 30;
        }

        private static bool TokenOrCVVInvalid(CustomerValidateRequest request, CustomerCardEntity entity)
        {
            entity.GenerateCardToken();
            return (entity.Token != request.Token) || (entity.CVV != request.CVV);
        }

        private static bool CardBelongsToAnotherCustomer(CustomerValidateRequest request, CustomerCardEntity entity)
        {
            return request.CustomerId != entity.CustomerId;
        }

        private static bool CardNotFound(CustomerCardEntity entity)
        {
            return entity == null;
        }
    }
}
