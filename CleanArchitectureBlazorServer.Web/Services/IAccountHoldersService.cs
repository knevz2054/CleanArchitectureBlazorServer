using CleanArchitectureBlazorServer.Common.Requests;
using CleanArchitectureBlazorServer.Common.Responses;
using CleanArchitectureBlazorServer.Common.Wrapper;

namespace CleanArchitectureBlazorServer.Web.Services
{
    public interface IAccountHoldersService
    {
        Task<ResponseWrapper<int>> AddAccountHolderAsync(CreateAccountHolder createAccountHolder);
        //Task<ResponseWrapper<int>> UpdateAccountHolderAsync(UpdateAccountHolder updateAccountHolder);
        //Task<ResponseWrapper<int>> DeleteAccountHolderAsync(int id);
        //Task<ResponseWrapper<AccountHolderResponse>> GetAccountHolderAsync(int id);
        Task<ResponseWrapper<List<AccountHolderResponse>>> GetAllAccountHolderAsync();
    }
}
