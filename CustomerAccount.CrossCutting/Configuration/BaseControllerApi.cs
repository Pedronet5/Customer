using CustomerAccount.Domain.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace CustomerAccount.CrossCutting
{

    [ApiController]
    public class BaseControllerApi : ControllerBase
    {
        private readonly INotification _notification;

        public BaseControllerApi(INotification notification)
        {
            _notification = notification;
        }

        protected ActionResult CustomResponse<T>(T result)
        {
            if (IsOperationValid())
                return Ok(result);

            return BadRequest(_notification.GetAll().ToArray());

        }
        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                AddError(error.ErrorMessage);
            }
            return CustomResponse(errors);
        }

        protected bool IsOperationValid() => !_notification.Any();

        protected void AddError(string erro) => _notification.Add(erro);

        protected void ClearErrors() => _notification.Clear();

    }
}
