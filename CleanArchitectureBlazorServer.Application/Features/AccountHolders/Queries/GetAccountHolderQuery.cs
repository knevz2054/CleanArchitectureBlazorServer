using CleanArchitectureBlazorServer.Application.Repositories;
using CleanArchitectureBlazorServer.Common.Models;
using CleanArchitectureBlazorServer.Common.Responses;
using CleanArchitectureBlazorServer.Common.Wrapper;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureBlazorServer.Application.Features.AccountHolders.Queries
{
    public class GetAccountHolderQuery : IRequest<ResponseWrapper<AccountHolderResponse>>
    {
        public int Id { get; set; }
    }

    public class GetAccountHolderQueryHandler : IRequestHandler<GetAccountHolderQuery, ResponseWrapper<AccountHolderResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAccountHolderQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseWrapper<AccountHolderResponse>> Handle(GetAccountHolderQuery request, CancellationToken cancellationToken)
        {
            var accountHolderInDb = await _unitOfWork.ReadRepositoryFor<AccountHolder>().GetAsync(request.Id);

            if (accountHolderInDb is not null)
            {
                return new ResponseWrapper<AccountHolderResponse>().Success(data: accountHolderInDb.Adapt<AccountHolderResponse>());
            }
            return new ResponseWrapper<AccountHolderResponse>().Failed(message: "Account Holder does not exists.");
        }
    }
}
