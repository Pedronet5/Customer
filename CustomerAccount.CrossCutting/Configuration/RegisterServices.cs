using CustomerAccount.Application.Interfaces.Repositories;
using CustomerAccount.Application.Services;
using CustomerAccount.Domain.Interfaces.Repositories;
using CustomerAccount.Domain.Notifications;
using CustomerAccount.Infrastructure.Data.InMemory;
using CustomerAccount.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerAccount.CrossCutting.Configuration
{
    public static class RegisterServices
    {
        public static IServiceCollection RegisterServicesDependecyInjection(this IServiceCollection services)
        {
            services.AddDbContext<InMemoryDbContext>(options => options.UseInMemoryDatabase(databaseName: "CustumerAccount"));
            services.AddScoped(typeof(InMemoryDbContext));
            services.AddScoped<ICustomerCardRepository, CustomerCardRepository>();
            services.AddScoped<ICustomerCardService, CustomerCardService>();
            services.AddScoped<INotification, Notification>();

            return services;
        }
    }
}
