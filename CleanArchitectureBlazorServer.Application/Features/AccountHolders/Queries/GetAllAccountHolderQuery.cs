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
    public class GetAllAccountHolderQuery : IRequest<ResponseWrapper<List<AccountHolderResponse>>>
    {

    }

    public class GetAllAccountHolderQueryHandler : IRequestHandler<GetAllAccountHolderQuery, ResponseWrapper<List<AccountHolderResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllAccountHolderQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseWrapper<List<AccountHolderResponse>>> Handle(GetAllAccountHolderQuery request, CancellationToken cancellationToken)
        {
            var accountHoldersInDb = await _unitOfWork.ReadRepositoryFor<AccountHolder>().GetAllAsync();

            if (accountHoldersInDb.Count > 0)
            {
                return new ResponseWrapper<List<AccountHolderResponse>>().Success(data: accountHoldersInDb.Adapt<List<AccountHolderResponse>>());
            }
            return new ResponseWrapper<List<AccountHolderResponse>>().Failed(message: "No Account Holders were found.");
        }
    }
}
