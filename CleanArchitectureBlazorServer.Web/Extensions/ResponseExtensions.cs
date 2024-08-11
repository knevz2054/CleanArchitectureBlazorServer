using CleanArchitectureBlazorServer.Common.Wrapper;
using System.Text.Json.Serialization;
using System.Text.Json;
using CleanArchitectureBlazorServer.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureBlazorServer.Web.Extensions
{
    public static class ResponseExtensions
    {
        //internal static async Task<ResponseWrapper<T>> ToResponse<T>(this HttpResponseMessage responseMessage)
        //{
        //    var responseAsString = await responseMessage.Content.ReadAsStringAsync();
        //    var responseObject = JsonSerializer.Deserialize<ResponseWrapper<T>>(responseAsString, new JsonSerializerOptions
        //    {
        //        PropertyNameCaseInsensitive = true,
        //        ReferenceHandler = ReferenceHandler.Preserve
        //    });

        //    return responseObject;
        //}

        //internal static async Task<ResponseWrapper<List<T>>> ToResponse<T>(this ApplicationDbContext dbContext, IQueryable<T> query, string successMessage = null) where T : class
        //{
        //    // Execute the query and get the data
        //    var data = await query.ToListAsync();

        //    // Return a new instance of ResponseWrapper<List<T>>
        //    return new ResponseWrapper<List<T>>
        //    {
        //        IsSuccessful = true,
        //        Data = data,
        //        Messages = new List<string> { successMessage ?? "Operation completed successfully." }
        //    };
        //}
    }
}
