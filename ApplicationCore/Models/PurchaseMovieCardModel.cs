namespace ApplicationCore.Models
{
    public class PurchaseMovieCardModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public DateTime PurchaseDateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid PurchaseNumber { get; set; }
    }
}