using AutoMapper;
using CleanArchitectureBlazorServer.Application.Features.AccountHolders.Commands;
using CleanArchitectureBlazorServer.Application.Features.AccountHolders.Queries;
using CleanArchitectureBlazorServer.Common.Requests;
using CleanArchitectureBlazorServer.Common.Responses;
using CleanArchitectureBlazorServer.Common.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureBlazorServer.Services.AccountHoldersServices
{
    public class AccountHoldersService : IAccountHoldersService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public AccountHoldersService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<ResponseWrapper<int>> AddAccountHolderAsync(CreateAccountHolder createAccountHolder)
        {
            var command = new CreateAccountHolderCommand()
            {
                CreateAccountHolder = createAccountHolder
            };

            var result = await _mediator.Send(command);
            return result;
        }

        public async Task<ResponseWrapper<int>> DeleteAccountHolderAsync(int id)
        {
            var delete = new DeleteAccountHolderCommand()
            {
                Id = id
            };
            var result = await _mediator.Send(delete);
            return result;
        }

        public async Task<ResponseWrapper<AccountHolderResponse>> GetAccountHolderAsync(int id)
        {
            var query = new GetAccountHolderQuery()
            {
                Id = id
            };
            var result = await _mediator.Send(query);
            return result; ;
        }

        public async Task<ResponseWrapper<List<AccountHolderResponse>>> GetAllAccountHolderAsync()
        {
            var query = new GetAllAccountHolderQuery();
            var result = await _mediator.Send(query);
            return result;
        }

        public async Task<ResponseWrapper<int>> UpdateAccountHolderAsync(UpdateAccountHolder updateAccountHolder)
        {
            var command = new UpdateAccountHolderCommand()
            {
                UpdateAccountHolder = updateAccountHolder
            };

            var result = await _mediator.Send(command);
            return result;
        }
    }
}
