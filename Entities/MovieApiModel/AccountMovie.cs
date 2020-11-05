namespace Entities.MovieApiModel
{
    public class AccountMovie
    {
        public int id { get; set; }
        public Rated rated { get; set; }
        public string favorite { get; set; }
        public string watchlist { get; set; }
    }
}
