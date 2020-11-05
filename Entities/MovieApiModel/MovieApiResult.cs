using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.MovieApiModel
{
    public class MovieApiResult
    {
        public int id { get; set; }
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public double? budget { get; set; }
        public string status { get; set; }
        public string title { get; set; }
        public double? vote_average { get; set; }
        public int? vote_count { get; set; }
        public double popularity { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public DateTime? release_date { get; set; }
        public string imdb_id { get; set; }
        public List<ProductionCompanies> production_companies { get; set; }
        public List<ProductionCountries> production_countries { get; set; }
        public List<Genres> genres { get; set; }
    }
}
