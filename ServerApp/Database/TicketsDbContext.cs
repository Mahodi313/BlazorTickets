using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace ServerApp.Database
{
    public class TicketsDbContext : DbContext
    {
        public TicketsDbContext()
        {

        }
        public TicketsDbContext(DbContextOptions<TicketsDbContext> options) : base(options)
        {

        }

        public DbSet<ResponseModel> Responses { get; set; }
        public DbSet<TagModel> Tags { get; set; }
        public DbSet<TicketModel> Ticket { get; set; }
        public DbSet<TicketTag> TicketTag { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TicketTag>()
            .HasKey(tt => new { tt.TicketId, tt.TagId });

            // Konfigurera många-till-många relationen mellan TicketModel och TagMo
            modelBuilder.Entity<TicketTag>()
            .HasOne(tt => tt.Ticket)
            .WithMany(t => t.TicketTags)
            .HasForeignKey(tt => tt.TicketId);
            modelBuilder.Entity<TicketTag>()
            .HasOne(tt => tt.Tag)
            .WithMany(t => t.TicketTags)
            .HasForeignKey(tt => tt.TagId);

            // Konfigurera en-till-många-relationen mellan TicketModel och ResponseMode
            modelBuilder.Entity<ResponseModel>()
            .HasOne(r => r.Ticket)
            .WithMany(t => t.Responses)
            .HasForeignKey(r => r.TicketId);
        }

    }
}
