using AutoMapper;
using CleanArchitectureBlazorServer.Common.Models;
using CleanArchitectureBlazorServer.Common.Requests;
using CleanArchitectureBlazorServer.Common.Responses;
using CleanArchitectureBlazorServer.Common.Wrapper;
using CleanArchitectureBlazorServer.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureBlazorServer.Web.Services
{
    public class AccountHoldersService : IAccountHoldersService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AccountHoldersService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseWrapper<int>> AddAccountHolderAsync(CreateAccountHolder createAccountHolder)
        {
            // Assuming CreateAccountHolder is a DTO, map it to the AccountHolder entity
            var accountHolderEntity = _mapper.Map<AccountHolder>(createAccountHolder);          

            // Add the entity to the DbContext
            var response = await _context.AccountHolders.AddAsync(accountHolderEntity);
            await _context.SaveChangesAsync();

            // Use the ToResponse extension method to convert to ResponseWrapper<int>
            return new ResponseWrapper<int>().Success(response.Entity.Id, "Account holder added successfully.");
        }

        public async Task<ResponseWrapper<int>> DeleteAccountHolderAsync(int id)
        {
            var accountHolder = await _context.AccountHolders.FindAsync(id);

            if (accountHolder == null)
            {
                return new ResponseWrapper<int>().Failed("Account holder not found.");
            }

            _context.AccountHolders.Remove(accountHolder);
            await _context.SaveChangesAsync();

            return new ResponseWrapper<int>().Success(id, "Account holder deleted successfully.");
        }

        public async Task<ResponseWrapper<AccountHolderResponse>> GetAccountHolderAsync(int id)
        {
            var accountHolder = await _context.AccountHolders.FindAsync(id);

            if (accountHolder == null)
            {
                return new ResponseWrapper<AccountHolderResponse>().Failed("Account holder not found.");
            }

            var accountHolderResponse = _mapper.Map<AccountHolderResponse>(accountHolder);

            return new ResponseWrapper<AccountHolderResponse>().Success(accountHolderResponse);
        }

        public async Task<ResponseWrapper<List<AccountHolderResponse>>> GetAllAccountHolderAsync()
        {
            var accountHolders = await _context.AccountHolders.ToListAsync();
            var accountHolderResponses = _mapper.Map<List<AccountHolderResponse>>(accountHolders);

            return new ResponseWrapper<List<AccountHolderResponse>>().Success(accountHolderResponses);
        }
    }
}
