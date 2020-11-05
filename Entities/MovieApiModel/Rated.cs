using System.ComponentModel.DataAnnotations;

namespace Entities.MovieApiModel
{
    public class Rated
    {
        [Range(1, 10)]
        public decimal? value { get; set; }
    }
}
