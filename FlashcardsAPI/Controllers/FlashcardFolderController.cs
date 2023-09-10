using Microsoft.AspNetCore.Mvc;
using FlashcardsAPI.Infrastructure;
using FlashcardsAPI.Domain;
using FlashcardsAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FlashcardsAPI.Controllers;

[ApiController]
[Route("api/folders")]

public class FlashcardFolderController : ControllerBase
{
    private readonly FlashcardsDbContext _dbContext;

    public FlashcardFolderController(FlashcardsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public ActionResult<IEnumerable<FlashcardFolderDto>> GetFolders() {
        var folders = _dbContext.Folders.Select(c => new FlashcardFolderDto()
        {
            Id = c.Id,
            Name = c.Name,
            Flashcards= c.Flashcards
        });



        foreach (var folder in folders)
        {            var cards = _dbContext.Flashcards
    .Where(f => f.Id == folder.Id).ToList();
            folder.Flashcards.AddRange(cards);
        }

        return Ok(folders);
    }

}
