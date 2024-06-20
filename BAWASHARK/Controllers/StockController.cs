using BAWASHARK.Data;
using BAWASHARK.Data.DTOs;
using BAWASHARK.Interfaces;
using BAWASHARK.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BAWASHARK.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StockController :  ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
            _context = context;
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
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreatedDto();
            await _context.Stocks.AddAsync(stockModel);

            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id });

        }
        [HttpPut("id")]

        public async Task<IActionResult> Update(int id, UpdateStockDto updateDto)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }
            stock.Symbol = updateDto.Symbol;
            stock.CompanyName = updateDto.CompanyName;
            stock.Purchase = updateDto.Purchase;
            stock.LastDiv = updateDto.LastDiv;
            stock.Industry = updateDto.Industry;
            stock.MarketCap = updateDto.MarketCap;

            await _context.SaveChangesAsync();

            return Ok(stock.ToStockDto());
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return BadRequest();
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return Ok();
            
        }



    }
}
