using FlashcardsAPI.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using FlashcardsAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FlashcardsAPI.Controllers;

[ApiController]
[Route("api/cards/{folderId:int}/cards")]
public class FlashcardController : ControllerBase
{
    private readonly FlashcardsDbContext _dbContext;

    public FlashcardController(FlashcardsDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    [HttpGet("{cardId:int}")]
    public async Task<ActionResult<FlashcardDto>> GetCard(int folderId, int cardId)
    {
        var cards = _dbContext.Flashcards
            .Where(f => f.Id == folderId && f.Id == cardId);
        if (!await cards.AnyAsync())
        {
            return NotFound();
        }

        var cardDto = await cards
            .Select(p => new FlashcardDto()
            {
                Id = p.Id, BackSide= p.BackSide, 
                FrontSide= p.FrontSide, 
                FolderId= folderId, 
                IsMastered= p.IsMastered
            })
            .FirstOrDefaultAsync();

        if (cardDto is null)
        {
            return NotFound();
        }

        return Ok(cardDto);
    }

} 

