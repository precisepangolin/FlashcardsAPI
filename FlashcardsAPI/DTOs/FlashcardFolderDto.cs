using FlashcardsAPI.Domain;

namespace FlashcardsAPI.DTOs
{
    public class FlashcardFolderDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Flashcard> Flashcards { get; set; } = new List<Flashcard> { };

        public int TotalFlashcards => Flashcards.Count();

        public int MasteredCount => Flashcards.Where(f => f.IsMastered).Count();
    }
}
