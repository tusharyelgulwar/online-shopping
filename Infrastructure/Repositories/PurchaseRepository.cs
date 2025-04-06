using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.Repositories;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace Infrastructure.Repositories
{
    public class PurchaseRepository : BaseRepository<Purchase>, IPurchaseRepository
    {
        private readonly MovieDbContext _context;
        private readonly IConfiguration _configuration;

        public PurchaseRepository(MovieDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Purchase>> GetPurchasesByUserAsync(int userId)
        {
            return await _context.Purchases
                .Include(p => p.Movie)
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        
        public async Task<bool> IsMoviePurchasedByUserAsync(int movieId, int userId)
        {
            return await _context.Purchases
                .AnyAsync(p => p.MovieId == movieId && p.UserId == userId);
        }
        
        public async Task<IEnumerable<TopMovieModel>> GetAllMoviesSortedByPurchasesAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("MovieVAFeb2024"));

            var sql = @"
                SELECT 
                    m.Id AS MovieId,
                    m.Title,
                    m.PosterUrl,
                    COUNT(*) AS TotalPurchases
                FROM Purchases p
                INNER JOIN Movies m ON m.Id = p.MovieId
                WHERE (@FromDate IS NULL OR p.PurchaseDateTime >= @FromDate)
                  AND (@ToDate IS NULL OR p.PurchaseDateTime <= @ToDate)
                GROUP BY m.Id, m.Title, m.PosterUrl
                ORDER BY TotalPurchases DESC;
            ";

            var result = await connection.QueryAsync<TopMovieModel>(sql, new { FromDate = fromDate, ToDate = toDate });
            return result;
        }

    }
}