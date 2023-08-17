namespace FlashcardsAPI.DTOs
{
    public class FlashcardDto
    {
        public int Id { get; set; }

        public string FrontSide { get; set; } = string.Empty;

        public string BackSide { get; set; } = string.Empty;

        public bool IsMastered { get; set; } = false;
    }
}
