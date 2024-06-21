using BAWASHARK.Interfaces;
using BAWASHARK.Mappers;
using BAWASHARK.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BAWASHARK.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StockController :  ControllerBase
    {
        private readonly IStockRepository _stockRepo;
        public StockController(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }
        
        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepo.GetAllAsync();

            var stockDto = stocks.Select(s => s.ToStockDto());

            return Ok(stocks);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);

            return Ok(stock.ToStockDto());
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stock = stockDto.ToStockFromCreatedDto();

            await _stockRepo.CreateAsync(stock);

            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());

        }
        [HttpPut("id")]

        public async Task<IActionResult> Update(int id, UpdateStockDto updateDto)
        {
            var stock = await _stockRepo.UpdateAsync(id, updateDto);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var stock = await _stockRepo.DeleteAsync(id);

            if (stock == null)
            {
                return BadRequest();
            }

            return Ok();
            
        }



    }
}
