using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.MovieApiModel
{
    public class Response
    {
        public int page { get; set; }
        public List<MovieResult> results { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
    }
}
