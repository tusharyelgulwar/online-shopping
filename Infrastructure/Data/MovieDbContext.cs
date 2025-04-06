using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class MovieDbContext: DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options): base(options)
    {
        
    }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Cast> Casts { get; set; }
    
    // Join / related tables
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<MovieCast> MovieCasts { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Trailer> Trailers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Composite keys
        modelBuilder.Entity<MovieGenre>().HasKey(mg => new { mg.MovieId, mg.GenreId });
        modelBuilder.Entity<MovieCast>().HasKey(mc => new { mc.MovieId, mc.CastId });
        modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
        modelBuilder.Entity<Favorite>().HasKey(f => new { f.MovieId, f.UserId });
        modelBuilder.Entity<Purchase>().HasKey(p => new { p.MovieId, p.UserId });
        modelBuilder.Entity<Review>().HasKey(r => new { r.MovieId, r.UserId });
        
        base.OnModelCreating(modelBuilder);
    }
}