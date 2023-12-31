﻿namespace FlashcardsAPI.Domain
{
    public class Flashcard
    {
        public int Id { get; set; }

        public string FrontSide { get; set; } = string.Empty;

        public string BackSide { get; set; } = string.Empty;

        public bool IsMastered { get; set; } = false;

        public int FolderId { get; set; } = 0;
    }
}
