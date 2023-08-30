using FlashcardsAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace FlashcardsAPI.Infrastructure;

public class FlashcardsDbContext : DbContext
{
    public DbSet<FlashcardFolder> Folders => Set<FlashcardFolder>();
    public DbSet<Flashcard> Flashcards => Set<Flashcard>();

    public FlashcardsDbContext(DbContextOptions<FlashcardsDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=Flashcards;Username=milena-codecool;Password=huehuehue");


protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FlashcardFolder>().HasData(new List<FlashcardFolder>
        {
            new FlashcardFolder
            {
                Id = 1, Name = "PierwszyFolder"
            },
            new FlashcardFolder
            {
                Id = 2, Name = "DrugiFolder"
            },
            new FlashcardFolder
            {
                Id = 3, Name = "TrzeciFolder"
            }
        });

        modelBuilder.Entity<Flashcard>().HasData(new List<Flashcard>
        {
            new Flashcard
            {
                Id = 1, FolderId = 1, FrontSide = "Awers1", BackSide = "Rewers1"
            },
            new Flashcard
            {
                Id = 2, FolderId = 1, FrontSide = "Awers2", BackSide = "Rewers2"
            },
            new Flashcard
            {
                Id = 3, FolderId = 2, FrontSide = "Awers3", BackSide = "Rewers3"
            },
            new Flashcard
            {
                Id = 4, FolderId = 3, FrontSide = "Awers4", BackSide = "Rewers4"
            },
        });
    }
}