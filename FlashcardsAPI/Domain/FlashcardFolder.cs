using System.Linq;

namespace FlashcardsAPI.Domain
{
    public class FlashcardFolder
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Flashcard> Flashcards { get; set; } = new List<Flashcard> { };

        public int TotalFlashcards => Flashcards.Count();

        public int MasteredCount => Flashcards.Where(f => f.IsMastered).Count();

    }
}
