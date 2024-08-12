using AutoMapper;
using CleanArchitectureBlazorServer.Common.Models;
using CleanArchitectureBlazorServer.Common.Requests;
using CleanArchitectureBlazorServer.Common.Responses;

namespace CleanArchitectureBlazorServer.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAccountHolder, AccountHolder>();
            CreateMap<AccountHolder, AccountHolderResponse>();

            CreateMap<AccountHolderResponse, UpdateAccountHolder>();
        }
    }
}
