using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace StockMap.Models
{
    public partial class StockMapDbContext : DbContext
    {
        public StockMapDbContext()
            : base("name=StockMapDbContext")
        {
        }

        public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<StockChip> StockChips { get; set; }
        public virtual DbSet<StockFound> StockFounds { get; set; }
        public virtual DbSet<StockTech> StockTeches { get; set; }
        public virtual DbSet<StockTrade> StockTrades { get; set; }
        public virtual DbSet<StockYesterday> StockYesterdays { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favorite>()
                .Property(e => e.StockId)
                .IsUnicode(false);

            modelBuilder.Entity<Stock>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<Stock>()
                .HasMany(e => e.Favorites)
                .WithRequired(e => e.Stock)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stock>()
                .HasOptional(e => e.StockChip)
                .WithRequired(e => e.Stock);

            modelBuilder.Entity<Stock>()
                .HasOptional(e => e.StockFound)
                .WithRequired(e => e.Stock);

            modelBuilder.Entity<Stock>()
                .HasOptional(e => e.StockTech)
                .WithRequired(e => e.Stock);

            modelBuilder.Entity<Stock>()
                .HasOptional(e => e.StockTrade)
                .WithRequired(e => e.Stock);

            modelBuilder.Entity<Stock>()
                .HasOptional(e => e.StockYesterday)
                .WithRequired(e => e.Stock);

            modelBuilder.Entity<StockChip>()
                .Property(e => e.StockId)
                .IsUnicode(false);

            modelBuilder.Entity<StockFound>()
                .Property(e => e.StockId)
                .IsUnicode(false);

            modelBuilder.Entity<StockFound>()
                .Property(e => e.ClosingPrice)
                .HasPrecision(16, 2);

            modelBuilder.Entity<StockFound>()
                .Property(e => e.PERatio)
                .HasPrecision(16, 2);

            modelBuilder.Entity<StockFound>()
                .Property(e => e.Dividend)
                .HasPrecision(16, 2);

            modelBuilder.Entity<StockTech>()
                .Property(e => e.StockId)
                .IsUnicode(false);

            modelBuilder.Entity<StockTech>()
                .Property(e => e.Stock5MA)
                .HasPrecision(16, 2);

            modelBuilder.Entity<StockTech>()
                .Property(e => e.Stock10MA)
                .HasPrecision(16, 2);

            modelBuilder.Entity<StockTech>()
                .Property(e => e.Stock20MA)
                .HasPrecision(16, 2);

            modelBuilder.Entity<StockTech>()
                .Property(e => e.Stock60MA)
                .HasPrecision(16, 2);

            modelBuilder.Entity<StockTrade>()
                .Property(e => e.StockId)
                .IsUnicode(false);

            modelBuilder.Entity<StockTrade>()
                .Property(e => e.Price)
                .HasPrecision(16, 2);

            modelBuilder.Entity<StockYesterday>()
                .Property(e => e.StockId)
                .IsUnicode(false);

            modelBuilder.Entity<StockYesterday>()
                .Property(e => e.OpenPrice)
                .HasPrecision(16, 2);

            modelBuilder.Entity<StockYesterday>()
                .Property(e => e.HighestPrice)
                .HasPrecision(16, 2);

            modelBuilder.Entity<StockYesterday>()
                .Property(e => e.LowestPrice)
                .HasPrecision(16, 2);

            modelBuilder.Entity<StockYesterday>()
                .Property(e => e.ClosingPrice)
                .HasPrecision(16, 2);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Favorites)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
