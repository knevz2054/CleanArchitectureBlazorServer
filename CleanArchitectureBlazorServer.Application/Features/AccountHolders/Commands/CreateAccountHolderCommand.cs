using CleanArchitectureBlazorServer.Application.Repositories;
using CleanArchitectureBlazorServer.Common.Models;
using CleanArchitectureBlazorServer.Common.Requests;
using CleanArchitectureBlazorServer.Common.Wrapper;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureBlazorServer.Application.Features.AccountHolders.Commands
{
    public class CreateAccountHolderCommand : IRequest<ResponseWrapper<int>>
    {
        public CreateAccountHolder CreateAccountHolder { get; set; }
    }

    public class CreateAccountHolderCommandHandler : IRequestHandler<CreateAccountHolderCommand, ResponseWrapper<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public CreateAccountHolderCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseWrapper<int>> Handle(CreateAccountHolderCommand request, CancellationToken cancellationToken)
        {
            var accountHolder = request.CreateAccountHolder.Adapt<AccountHolder>();

            await _unitOfWork.WriteRepositoryFor<AccountHolder>().AddAsync(accountHolder);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new ResponseWrapper<int>().Success(accountHolder.Id, "Account Holder created successfuly.");
        }
    }
}
