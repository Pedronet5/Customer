using CustomerAccount.Domain.Models.Requests;
using CustomerAccount.Domain.Responses;
using CustomerAccount.Tests.Fixtures;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CustomerAccount.Tests.Scenarios.Integrated
{
    public class CustomerTest
    {
        private readonly TestContext _testContext;

        public CustomerTest()
        {
            _testContext = new TestContext();
        }

        [Fact]
        public async Task Card_Post_Returns_BadRequest()
        {
            var response = await InvokeCreateCard(0, 0, 0);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Card_Post_Returns_OK()
        {
            var response = await InvokeCreateCard(134, 5361513396360376, new Random().Next(100, 1000));

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var cardSaved = JsonConvert.DeserializeObject<CustomerPostResponse>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(cardSaved);

            cardSaved.CardId.Should().NotBeEmpty();
            cardSaved.Token.Should().NotBeEmpty();

        }

        [Fact]
        public async Task Valid_Post_Returns_Validate_True()
        {
            var cvv = new Random().Next(100, 1000);
            var customerId = new Random().Next(100, 1000);
            var cardNumber = 5361513396360376;

            var response = await InvokeCreateCard(customerId, cardNumber, cvv);
            var card = JsonConvert.DeserializeObject<CustomerPostResponse>(await response.Content.ReadAsStringAsync());


            var responseValidate = await InvokeValidateCard(cvv, customerId, card);
            responseValidate.StatusCode.Should().Be(HttpStatusCode.OK);
            var validated = JsonConvert.DeserializeObject<bool>(await responseValidate.Content.ReadAsStringAsync());

            Assert.True(validated);

        }

        [Fact]
        public async Task Valid_Post_Returns_Validate_False()
        {
            var cvv = new Random().Next(100, 1000);
            var customerId = new Random().Next(100, 1000);
            var cardNumber = 5361513396360377;

            var response = await InvokeCreateCard(customerId, cardNumber, cvv);
            var card = JsonConvert.DeserializeObject<CustomerPostResponse>(await response.Content.ReadAsStringAsync());


            var responseValidate = await InvokeValidateCard(123, customerId, card);
            responseValidate.StatusCode.Should().Be(HttpStatusCode.OK);
            var validated = JsonConvert.DeserializeObject<bool>(await responseValidate.Content.ReadAsStringAsync());

            Assert.False(validated);

        }

        private async Task<HttpResponseMessage> InvokeValidateCard(int cvv, int customerId, CustomerPostResponse card)
        {
            var request = new CustomerValidateRequest()
            {
                CustomerId = customerId,
                CardId = card.CardId,
                CVV = cvv,
                Token = card.Token
            };

            var json = JsonConvert.SerializeObject(request);

            var responseValidate = await _testContext.Client.PostAsync($"/api/CustomerCard/PostValidateToken/validate", new StringContent(json, Encoding.UTF8, "application/json"));
            return responseValidate;
        }

        private async Task<HttpResponseMessage> InvokeCreateCard(int customerId, long cardNumber, int cvv)
        {
            var request = new CustomerPostRequest()
            {
                CustomerId = customerId,
                CardNumber = cardNumber,
                CVV = cvv,
            };

            var json = JsonConvert.SerializeObject(request);

            var response = await _testContext.Client.PostAsync($"/api/CustomerCard/PostCustomer", new StringContent(json, Encoding.UTF8, "application/json"));
            return response;
        }
    }
}
