using BAWASHARK.Models;
using BAWASHARK.Interfaces;
using BAWASHARK.Data;
using Microsoft.EntityFrameworkCore;
using BAWASHARK.Mappers;
using BAWASHARK.Models.DTOs;

namespace BAWASHARK.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public Task<List<Stock>> GetAllAsync()
        {
            return _context.Stocks.Include(c => c.Comments).ToListAsync();

        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            _context.SaveChangesAsync();

            return stockModel;
        }
        public async Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto)
        {
            var existsStock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

            if (existsStock == null)
            {
                return null;
            }

            existsStock.Symbol = stockDto.Symbol;
            existsStock.CompanyName = stockDto.CompanyName;
            existsStock.Purchase = stockDto.Purchase;
            existsStock.LastDiv = stockDto.LastDiv;
            existsStock.Industry = stockDto.Industry;
            existsStock.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();

            return (existsStock);
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

            if (stock == null)
            {
                return null;
            }

            _context.Stocks.Remove(stock);

            await _context.SaveChangesAsync();

            return (stock);

        }
    }
}
