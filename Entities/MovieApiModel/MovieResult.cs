﻿using System;
using System.Collections.Generic;

namespace Entities.MovieApiModel
{
    public class MovieResult

    {
        public double popularity { get; set; }
        public int? vote_count { get; set; }
        public bool? video { get; set; }
        public string poster_path { get; set; }
        public int id { get; set; }
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string title { get; set; }
        public double? vote_average { get; set; }
        public string overview { get; set; }
        public DateTime? release_date { get; set; }
        public List<object> genre_ids { get; set; }

    }
}
