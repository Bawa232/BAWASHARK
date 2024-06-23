using BAWASHARK.Interfaces;
using BAWASHARK.Mappers;
using BAWASHARK.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BAWASHARK.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var comments = await _commentRepo.GetAllAsync();
            
            var commentDto = comments.Select(s => s.ToCommentDto());

            return Ok(commentDto);
        }

        [HttpGet("id")]

        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("stockId")]

        public async Task<IActionResult> Create(int stockId, [FromBody] CreateCommentDto commentDto)
        {
            if (!await _stockRepo.StockExistsAsync(stockId))
            {
                return BadRequest("stock does not exist");
            }

            if (commentDto == null)
            {
                return BadRequest();
            }

            var commentModel = commentDto.ToCommentFromCreate(stockId);

            await _commentRepo.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto);    
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCommentRequestDto commentDto)
        {
            var commentModel = commentDto.ToCommentFromUpdate();
            var existsComment = await _commentRepo.UpdateAsync(id, commentModel);

            if (existsComment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(existsComment);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteComment = _commentRepo.DeleteAsync(id);
           
            if (deleteComment == null)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
