using AutoMapper;
using CustomerAccount.Domain.ProfileMappings;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerAccount.CrossCutting.Configuration
{
    public static class RegisterAutoMapper
    {
        public static IServiceCollection RegisterMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CustumerCardMap());
            });
            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }
    }
}
