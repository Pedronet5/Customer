using AutoMapper;
using CustomerAccount.Application.Services;
using CustomerAccount.Domain.Interfaces.Repositories;
using CustomerAccount.Domain.Models.Entities;
using CustomerAccount.Domain.Models.Requests;
using CustomerAccount.Domain.Notifications;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CustomerAccount.Tests.Scenarios.Unit
{
    public class CustomerServiceCardTest
    {
        private readonly Mock<INotification> _notification;
        private readonly Mock<ICustomerCardRepository> _repository;
        private readonly Mock<ILogger<CustomerCardService>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly CustomerCardService _customerCardService;
        public CustomerServiceCardTest()
        {
            _notification = new Mock<INotification>();
            _repository = new Mock<ICustomerCardRepository>();
            _logger = new Mock<ILogger<CustomerCardService>>();
            _mapper = new Mock<IMapper>();
            _customerCardService = new CustomerCardService(_notification.Object, _repository.Object, _mapper.Object, _logger.Object);
        }

        [Fact]
        public async Task Should_Not_Register_Card()
        {
            var CustomerReturn = new CustomerCardEntity();
            _repository.Setup(x => x.PostCustumerAccount(It.IsAny<CustomerPostRequest>())).ReturnsAsync(CustomerReturn);

            var cardToRegister = new CustomerPostRequest();
            var result = await _customerCardService.PostCustomerAsync(cardToRegister);

            result.Should().BeNull();
        }

        [Fact]
        public void Should_Validate_Card_Successfully()
        {

            var cardRegistered = new CustomerCardEntity()
            {
                CardId = Guid.NewGuid(),
                CardNumber = 7418529647524786,
                CreateDate = DateTime.UtcNow.AddMinutes(-5),
                CustomerId = 123,
                CVV = 134
            };

            cardRegistered.GenerateCardToken();

            _repository.Setup(x => x.GetCardByIdAsync(It.IsAny<Guid>())).Returns(cardRegistered);

            var validate = new CustomerValidateRequest()
            {
                CardId = cardRegistered.CardId,
                CustomerId = cardRegistered.CustomerId,
                CVV = cardRegistered.CVV,
                Token = cardRegistered.Token
            };

            var valid = _customerCardService.ValidateToken(validate);

            valid.Should().BeTrue();


        }

        [Fact]
        public void Should_Validate_Fail_Due_Card_Not_Found()
        {

            _repository.Setup(x => x.GetCardByIdAsync(It.IsAny<Guid>())).Returns((CustomerCardEntity)null);

            var validate = new CustomerValidateRequest()
            {
                CardId = Guid.NewGuid(),
                CustomerId = 1,
                CVV = 234,
                Token = "134"
            };

            var valid = _customerCardService.ValidateToken(validate);

            valid.Should().BeFalse();


        }

        [Fact]
        public void Should_Validate_Fail_Due_Invalid_Date()
        {

            var cardRegistered = new CustomerCardEntity()
            {
                CardId = Guid.NewGuid(),
                CardNumber = 7418529647524786,
                CreateDate = DateTime.UtcNow.AddMinutes(-35),
                CustomerId = 123,
                CVV = 134
            };

            cardRegistered.GenerateCardToken();

            _repository.Setup(x => x.GetCardByIdAsync(It.IsAny<Guid>())).Returns(cardRegistered);

            var validate = new CustomerValidateRequest()
            {
                CardId = cardRegistered.CardId,
                CustomerId = cardRegistered.CustomerId,
                CVV = cardRegistered.CVV,
                Token = cardRegistered.Token
            };

            var valid = _customerCardService.ValidateToken(validate);

            valid.Should().BeFalse();
        }


        [Fact]
        public void Should_Validate_Fail_Due_Invalid_CVV()
        {

            var cardRegistered = new CustomerCardEntity()
            {
                CardId = Guid.NewGuid(),
                CardNumber = 7418529647524786,
                CreateDate = DateTime.UtcNow.AddMinutes(-5),
                CustomerId = 123,
                CVV = 134
            };

            cardRegistered.GenerateCardToken();

            _repository.Setup(x => x.GetCardByIdAsync(It.IsAny<Guid>())).Returns(cardRegistered);

            var validate = new CustomerValidateRequest()
            {
                CardId = cardRegistered.CardId,
                CustomerId = cardRegistered.CustomerId,
                CVV = 555,
                Token = cardRegistered.Token
            };

            var valid = _customerCardService.ValidateToken(validate);

            valid.Should().BeFalse();
        }


        [Fact]
        public void Should_Validate_Fail_Due_Invalid_Token()
        {

            var cardRegistered = new CustomerCardEntity()
            {
                CardId = Guid.NewGuid(),
                CardNumber = 7418529647524786,
                CreateDate = DateTime.UtcNow.AddMinutes(5),
                CustomerId = 123,
                CVV = 134
            };

            cardRegistered.GenerateCardToken();

            _repository.Setup(x => x.GetCardByIdAsync(It.IsAny<Guid>())).Returns(cardRegistered);

            var validate = new CustomerValidateRequest()
            {
                CardId = cardRegistered.CardId,
                CustomerId = cardRegistered.CustomerId,
                CVV = cardRegistered.CVV,
                Token = "3"
            };

            var valid = _customerCardService.ValidateToken(validate);

            valid.Should().BeFalse();

        }

        [Fact]
        public void Should_Validate_Fail_Due_Belongs_To_Another_Customer()
        {
            var cardRegistered = new CustomerCardEntity()
            {
                CardId = Guid.NewGuid(),
                CardNumber = 7418529647524786,
                CreateDate = DateTime.UtcNow.AddMinutes(-35),
                CustomerId = 123,
                CVV = 134
            };

            cardRegistered.GenerateCardToken();

            _repository.Setup(x => x.GetCardByIdAsync(It.IsAny<Guid>())).Returns(cardRegistered);

            var validate = new CustomerValidateRequest()
            {
                CardId = cardRegistered.CardId,
                CustomerId = 3,
                CVV = cardRegistered.CVV,
                Token = cardRegistered.Token
            };

            var valid = _customerCardService.ValidateToken(validate);

            valid.Should().BeFalse();

        }
    }
}
