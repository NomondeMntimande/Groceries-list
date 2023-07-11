using AuthApi.Dtos;
using AuthApi.Dtos.Enteties;
using AuthApi.Helpers;

namespace AuthApi.Services.Interfaces
{
    public interface IGroceriesListService
    {
        Task<OperationResult<GroceriesList>> GetGroceriesListAsync(int Id);
        Task<OperationResult<List<GroceriesList>>> GetAllGroceriesListAsync();
        Task<OperationResult<GroceriesList>> CreateGroceriesListAsync(GroceriesListDtos groceriesList);
        Task<OperationResult<GroceriesList>> UpdateGroceriesListAsync(int id, GroceriesListDtos groceriesList);
        Task<OperationResult<bool>> DeleteGroceriesListAsync(int id);
    }
}
