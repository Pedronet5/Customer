using AutoMapper;
using CustomerAccount.Domain.Models.Entities;
using CustomerAccount.Domain.Responses;

namespace CustomerAccount.Domain.ProfileMappings
{
    public class CustumerCardMap : Profile
    {
        public CustumerCardMap()
        {
            CreateMap<CustomerCardEntity, CustomerPostResponse>();
        }
    }
}
