using Microsoft.AspNetCore.Mvc;
using FlashcardsAPI.Infrastructure;
using FlashcardsAPI.Domain;
using FlashcardsAPI.DTOs;

namespace FlashcardsAPI.Controllers;

[ApiController]
[Route("api/folders")]

public class FlashcardFolderController : ControllerBase
{
    private readonly DataService _dataService;
    public FlashcardFolderController(DataService dataService)
    {
        _dataService = dataService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<FlashcardFolderDto>> GetFolders() {
        var userFolders = _dataService.UserFolders.Select(c => new FlashcardFolderDto()
        {
            Id = c.Id,
            Name = c.Name,
            Flashcards= c.Flashcards,
        });
        var premadeFolders = _dataService.PremadeFolders.Select(c => new FlashcardFolderDto()
        {
            Id = c.Id,
            Name = c.Name,
            Flashcards = c.Flashcards,
        });
        var folders = new List<IEnumerable<FlashcardFolderDto>>()
        {
            userFolders, premadeFolders
        };

        return Ok(folders);
    }
    
    
}
