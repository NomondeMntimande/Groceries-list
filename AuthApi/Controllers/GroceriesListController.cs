using AuthApi.Dtos;
using AuthApi.Dtos.Enteties;
using AuthApi.Helpers;
using AuthApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroceriesListController : ControllerBase
    {
        private readonly IGroceriesListService _groceries;

        public GroceriesListController(IGroceriesListService groceries)
        {
            _groceries = groceries;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroceriesList>>> GetGroceriesList()
        {
            var lists = await _groceries.GetAllGroceriesListAsync();
            return Ok(lists);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroceriesList>> GetGroceriesList(int id)
        {
            var list = await _groceries.GetGroceriesListAsync(id);
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroceriesList(int id, GroceriesListDtos groceriesList)
        {
            if (id != groceriesList.Id)
            {
                return BadRequest();
            }

            var updatedList = await _groceries.UpdateGroceriesListAsync(id, groceriesList);
            if (updatedList == null)
            {
                return NotFound();
            }

            return Ok(updatedList);
        }

        [HttpPost]
        public async Task<ActionResult<GroceriesList>> CreateGroceriesList(GroceriesListDtos groceriesList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var operationResult = await _groceries.CreateGroceriesListAsync(groceriesList);

            if (operationResult.Status == OperationResultStatus.Success)
            {
                return Ok(operationResult.Data);
            }

            return BadRequest(operationResult.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroceriesList(int id)
        {
            var deletedList = await _groceries.DeleteGroceriesListAsync(id);
            if (deletedList == null)
            {
                return NotFound();
            }
            return Ok(deletedList);
        }
    }
}
