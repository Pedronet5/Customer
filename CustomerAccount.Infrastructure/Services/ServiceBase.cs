using CustomerAccount.Domain.Notifications;
using FluentValidation.Results;

namespace CustomerAccount.Infrastructure.Services
{
    public class ServiceBase
    {
        protected readonly INotification _notification;

        public ServiceBase(INotification notification)
        {
            _notification = notification;
        }

        protected void AddErrors(ValidationResult validationResult)
        {
            foreach (var item in validationResult.Errors)
            {
                AddError(item.ErrorMessage);
            }
        }

        protected void AddError(string error)
        {
            _notification.Add(error);
        }
    }
}
