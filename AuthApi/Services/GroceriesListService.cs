using AuthApi.Data;
using AuthApi.Dtos;
using AuthApi.Dtos.Enteties;
using AuthApi.Helpers;
using AuthApi.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Services
{
    public class GroceriesListService : IGroceriesListService
    {
        private readonly AccountContext _dbContext;
        private readonly IMapper _mapper;

        public GroceriesListService(AccountContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OperationResult<GroceriesList>> GetGroceriesListAsync(int Id)
        {
            try
            {
                var list = await _dbContext.GroceriesList.FirstOrDefaultAsync(x => x.Id == Id);
                if (list == null)
                    return OperationResult<GroceriesList>.Failure("GroceriesList not found.");

                return OperationResult<GroceriesList>.Success(list);
            }
            catch (Exception ex)
            {

                throw;
            }
            throw new NotImplementedException();
        }

        public async Task<OperationResult<List<GroceriesList>>> GetAllGroceriesListAsync()
        {

            try
            {
                var list = await _dbContext.GroceriesList.ToListAsync();
                return OperationResult<List<GroceriesList>>.Success(list);
            }
            catch (Exception ex)
            {
                return OperationResult<List<GroceriesList>>.Failure($"Error trying to retrieve lists. {ex.Message}");
            }
        }

        public async Task<OperationResult<GroceriesList>> CreateGroceriesListAsync(GroceriesListDtos groceriesList)
        {
            try
            {
                var list = _mapper.Map<GroceriesList>(groceriesList);
                await _dbContext.GroceriesList.AddAsync(list);
                await _dbContext.SaveChangesAsync();
                return OperationResult<GroceriesList>.Success(list);
            }
            catch (Exception ex)
            {
                return OperationResult<GroceriesList>.Failure($"Error creating list. {ex.Message}");
            }
        }

        public async Task<OperationResult<GroceriesList>> UpdateGroceriesListAsync(int id, GroceriesListDtos groceriesList)
        {
            try
            {
                var existingList = await _dbContext.GroceriesList.FirstOrDefaultAsync(r => r.Id == id);

                if (existingList == null)
                    return OperationResult<GroceriesList>.Failure("List not found.");

                existingList.listName = groceriesList.listName;
                existingList.listOwner = groceriesList.listOwner ?? existingList.listOwner;
                await _dbContext.SaveChangesAsync();

                return OperationResult<GroceriesList>.Success(existingList);
            }
            catch (Exception ex)
            {
                return OperationResult<GroceriesList>.Failure($"Error updating list: {ex.Message}");
            }
        }

        public async Task<OperationResult<bool>> DeleteGroceriesListAsync(int id)
        {
            try
            {
                var list = await _dbContext.GroceriesList.FindAsync(id);
                if (list == null)
                    return OperationResult<bool>.Failure("List not found.");

                _dbContext.GroceriesList.Remove(list);
                await _dbContext.SaveChangesAsync();

                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure($"Error deleting list: {ex.Message}");
            }
        }
    }
}
