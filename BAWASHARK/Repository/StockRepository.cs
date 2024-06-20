using BAWASHARK.Models;
using BAWASHARK.Interfaces;
using BAWASHARK.Data;
using Microsoft.EntityFrameworkCore;

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
            return _context.Stocks.ToListAsync();

        }
    }
}
