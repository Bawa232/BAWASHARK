﻿using BAWASHARK.Data;
using BAWASHARK.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace BAWASHARK.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StockController :  ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }
        
        [HttpGet]

        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList()
                .Select(s => s.ToStockDto());

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var stock = _context.Stocks.Find(id).ToStockDto();

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }

    }
}
