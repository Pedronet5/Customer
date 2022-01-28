using CustomerAccount.Application.Interfaces.Repositories;
using CustomerAccount.CrossCutting;
using CustomerAccount.Domain.Models.Requests;
using CustomerAccount.Domain.Notifications;
using CustomerAccount.Domain.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace CustomerAccount.Controllers
{
    [Route("api/[controller]")]
    public class CustomerCardController : BaseControllerApi
    {
        private readonly ICustomerCardService _customerService;
        public CustomerCardController(ICustomerCardService customerService, INotification notification) : base(notification)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// operation responsible for storing the card 
        /// </summary>
        /// <param name="CustomerPostRequest">The card's data that will be stored</param>
        /// <returns></returns>
        [HttpPost]
        [Route("PostCustomer")]
        [ProducesResponseType(typeof(bool), (int)StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), (int)StatusCodes.Status500InternalServerError)]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(CustomerPostResponse))]
        public async Task<IActionResult> PostCustomerAsync([FromBody] CustomerPostRequest request)
        {
            var result = await _customerService.PostCustomerAsync(request).ConfigureAwait(false);
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(result);
        }

        /// <summary>
        /// operation responsible for validating the card
        /// </summary>
        /// <param name="CustomerValidateRequest">The data of the storaged card</param>
        /// <returns></returns>
        [HttpPost]
        [Route("PostValidateToken/validate")]
        [ProducesResponseType(typeof(bool), (int)StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), (int)StatusCodes.Status500InternalServerError)]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(CustomerValidateResponse))]
        public IActionResult PostValidateToken(CustomerValidateRequest request)
        {
            var response = _customerService.ValidateToken(request);

            return CustomResponse(response);
        }
    }
}
