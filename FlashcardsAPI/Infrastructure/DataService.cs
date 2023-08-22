using FlashcardsAPI.Domain;
using System.Security.Cryptography.X509Certificates;

namespace FlashcardsAPI.Infrastructure
{
    public class DataService
    {
        public List<FlashcardFolder> UserFolders { get;  }

        public List<FlashcardFolder> PremadeFolders { get; }

        public DataService()
        {
            UserFolders= new List<FlashcardFolder>()
            {
                new FlashcardFolder() { Id =1, Name = "Polski", Flashcards= new List<Flashcard>()
                {
                    new Flashcard() { Id = 1, FrontSide = "Jaki jest, każdy widzi", BackSide = "Koń"},
                    new Flashcard() { Id = 2, FrontSide = "Ma kota", BackSide = "Ola"},
                    // new Flashcard() { Id = 3,FrontSide  = }
                }
                },
                new FlashcardFolder() { Id =2, Name = "Niemiecki", Flashcards= new List<Flashcard>()
                {
                    new Flashcard() { Id = 1, FrontSide = "Motyl", BackSide = "das Schmetterling"},
                    new Flashcard() { Id = 2, FrontSide = "Kot", BackSide = "die Katze"},
                }
                },
                new FlashcardFolder() { Id =3, Name = "Francuski", Flashcards= new List<Flashcard>()
                {
                    new Flashcard() { Id = 1, FrontSide = "Tak", BackSide = "Oui"},
                    new Flashcard() { Id = 2, FrontSide = "Piekarnia", BackSide = "Boulangerie"},
                }
                },
            };

            PremadeFolders = new List<FlashcardFolder>()
            {
                new FlashcardFolder() { Id =1, Name = "Łacina", Flashcards= new List<Flashcard>()
                {
                    new Flashcard() { Id = 1, FrontSide = "Żyj dniem", BackSide = "Carpe diem"},
                    new Flashcard() { Id = 2, FrontSide = "Chłopiec", BackSide = "Puer"},
                }
                },
                new FlashcardFolder() { Id =2, Name = "Japoński", Flashcards= new List<Flashcard>()
                {
                    new Flashcard() { Id = 1, FrontSide = "Jabłko", BackSide = "Ringo"},
                    new Flashcard() { Id = 2, FrontSide = "Budyń", BackSide = "Purin"},
                }
                },
                new FlashcardFolder() { Id =3, Name = "Hiszpański", Flashcards= new List<Flashcard>()
                {
                    new Flashcard() { Id = 1, FrontSide = "Dzisiaj", BackSide = "Hoy"},
                    new Flashcard() { Id = 2, FrontSide = "Sałatka", BackSide = "La ensalada"},
                }
                },
            };

        }
    }
}
