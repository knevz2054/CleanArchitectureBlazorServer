using AutoMapper;
using CleanArchitectureBlazorServer.Application.Features.AccountHolders.Commands;
using CleanArchitectureBlazorServer.Application.Features.AccountHolders.Queries;
using CleanArchitectureBlazorServer.Application.Repositories;
using CleanArchitectureBlazorServer.Common.Models;
using CleanArchitectureBlazorServer.Common.Requests;
using CleanArchitectureBlazorServer.Common.Responses;
using CleanArchitectureBlazorServer.Common.Wrapper;
using CleanArchitectureBlazorServer.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureBlazorServer.Web.Services
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

        //public async Task<ResponseWrapper<int>> DeleteAccountHolderAsync(int id)
        //{           
        //    //var readRepo = _unitOfWork.ReadRepositoryFor<AccountHolder>();
        //    //var accountHolder = await readRepo.GetAsync(id);

        //    //if (accountHolder == null)
        //    //{
        //    //    return new ResponseWrapper<int>().Failed("Account holder not found.");
        //    //}

        //    //var writeRepo = _unitOfWork.WriteRepositoryFor<AccountHolder>();
        //    //await writeRepo.DeleteAsync(accountHolder);
        //    //await _unitOfWork.CommitAsync(CancellationToken.None);

        //    //return new ResponseWrapper<int>().Success(id, "Account holder deleted successfully.");


        //}

        //public async Task<ResponseWrapper<AccountHolderResponse>> GetAccountHolderAsync(int id)
        //{            

        //    var readRepo = _unitOfWork.ReadRepositoryFor<AccountHolder>();
        //    var accountHolder = await readRepo.GetAsync(id);

        //    if (accountHolder == null)
        //    {
        //        return new ResponseWrapper<AccountHolderResponse>().Failed("Account holder not found.");
        //    }

        //    var accountHolderResponse = _mapper.Map<AccountHolderResponse>(accountHolder);

        //    return new ResponseWrapper<AccountHolderResponse>().Success(accountHolderResponse);
        //}

        public async Task<ResponseWrapper<List<AccountHolderResponse>>> GetAllAccountHolderAsync()
        {
            //var readRepo = _unitOfWork.ReadRepositoryFor<AccountHolder>();
            //var accountHolders = await readRepo.GetAllAsync();
            //var accountHolderResponses = _mapper.Map<List<AccountHolderResponse>>(accountHolders);

            //return new ResponseWrapper<List<AccountHolderResponse>>().Success(accountHolderResponses);
                        

            var query = new GetAllAccountHolderQuery();
            var result = await _mediator.Send(query);
            return result;

        }
    }
}
