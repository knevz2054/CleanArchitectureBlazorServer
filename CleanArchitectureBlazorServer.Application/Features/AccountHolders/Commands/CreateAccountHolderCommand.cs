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
             var createAccountHolder = request.CreateAccountHolder;

            // Check for existing account holder
            var existingAccountHolder = _unitOfWork.ReadRepositoryFor<AccountHolder>()
                    .Entities.FirstOrDefault(a => a.FirstName == createAccountHolder.FirstName && a.LastName == createAccountHolder.LastName);
            //.FirstOrDefaultAsync(a => a.FirstName == createAccountHolder.FirstName && a.LastName == createAccountHolder.LastName, cancellationToken);

        if (existingAccountHolder != null)
        {
                //return new ResponseWrapper<int>
                //{
                //    IsSuccessful = false,
                //    Messages = { "An account holder with this name already exists." }
                //};
                return new ResponseWrapper<int>().Failed(message: "An account holder with this name already exists.");
            }

        var accountHolder = createAccountHolder.Adapt<AccountHolder>();

        await _unitOfWork.WriteRepositoryFor<AccountHolder>().AddAsync(accountHolder);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(accountHolder.Id, "Account Holder created successfully.");



            //var accountHolder = request.CreateAccountHolder.Adapt<AccountHolder>();

            //await _unitOfWork.WriteRepositoryFor<AccountHolder>().AddAsync(accountHolder);
            //await _unitOfWork.CommitAsync(cancellationToken);

            //return new ResponseWrapper<int>().Success(accountHolder.Id, "Account Holder created successfuly.");
        }
    }
}
