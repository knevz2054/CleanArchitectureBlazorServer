using CleanArchitectureBlazorServer.Application.Repositories;
using CleanArchitectureBlazorServer.Common.Models;
using CleanArchitectureBlazorServer.Common.Requests;
using CleanArchitectureBlazorServer.Common.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureBlazorServer.Application.Features.AccountHolders.Commands
{

    public class UpdateAccountHolderCommand : IRequest<ResponseWrapper<int>>
    {
        public UpdateAccountHolder UpdateAccountHolder { get; set; }
    }

    public class UpdateAccountHolderCommandHandler : IRequestHandler<UpdateAccountHolderCommand, ResponseWrapper<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public UpdateAccountHolderCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseWrapper<int>> Handle(UpdateAccountHolderCommand request, CancellationToken cancellationToken)
        {
            var accountHolderInDb = await _unitOfWork.ReadRepositoryFor<AccountHolder>().GetAsync(request.UpdateAccountHolder.Id);
            if (accountHolderInDb is not null)
            {
                var updateAccountHolder = accountHolderInDb.Update(request.UpdateAccountHolder.FirstName, request.UpdateAccountHolder.LastName,
                    request.UpdateAccountHolder.DateOfBirth, request.UpdateAccountHolder.ContactNumber, request.UpdateAccountHolder.Email);

                await _unitOfWork.WriteRepositoryFor<AccountHolder>().UpdateAsync(updateAccountHolder);
                await _unitOfWork.CommitAsync(cancellationToken);

                return new ResponseWrapper<int>().Success(updateAccountHolder.Id, "Account Holder updated successfuly.");
            }

            return new ResponseWrapper<int>().Failed("Account Holder does not exists.");
        }
    }
}
