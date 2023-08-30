using AutoMapper;
using FlashcardsAPI.Domain;
using FlashcardsAPI.DTOs;

namespace Contacts.WebAPI.Configurations.Mapper;

public class FlashcardsProfile : Profile
{
    public FlashcardsProfile()
    {
        CreateMap<Flashcard, FlashcardDto>();
        CreateMap<FlashcardFolder, FlashcardFolderDto>();
    }
}