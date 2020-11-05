using BLL.Abstract;
using BLL.Constants;
using Core.Extensions;
using Core.Utility.Results;
using Entities.Dto;
using Entities.MovieApiModel;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace BLL.Concrete
{
    public class MovieAppService : IMovieAppService
    {
        private static HttpClient _httpClient = new HttpClient();
        public string sessionId = "a1001fdf51d6340e807384037554bd2df37a3fde";
        public string apiKey = "6353a5931f0e3c6a1981211e097a15cc";

        // filme puan verme
        public IResult MovieAddRate(AccountMovieCreateInput accountMovie, int movieId)
        {
            try
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(accountMovie.rated), Encoding.UTF8, "application/json");
                var result = _httpClient.PostAsync(string.Format("https://api.themoviedb.org/3/movie/{0}/rating?api_key={1}&session_id={2}", movieId, apiKey, sessionId), httpContent).Result;
                if (result.StatusCode == System.Net.HttpStatusCode.Created)
                    return new SuccessResult(Messages.Successfull);
                return new ErrorResult(Messages.Error);
            }
            catch (Exception ex)
            {

                return new ErrorResult(ex.Message);
            }

        }

        //Kişisel hesaptaki film bilgilerini getirme
        public IDataResult<AccountMovie> GetMovieById(int movieId)
        {
            HttpWebRequest apiRequest = WebRequest.CreateHttp(string.Format("https://api.themoviedb.org/3/movie/{0}/account_states?api_key={1}&session_id={2}", movieId, apiKey, sessionId)) as HttpWebRequest;

            string apiResponse = "";

            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
            }

            AccountMovie rootObject = JsonConvert.DeserializeObject<AccountMovie>(apiResponse);

            return new SuccessDataResult<AccountMovie>(rootObject);
        }

        //tüm filmleri sayfa sayfa getirme
        public IDataResult<PagedList<MovieResult>> GetMovies(int page, int pageSize)
        {
            var tasks = new List<MovieResult>();

            for (int i = 1; i < 50; i++)
            {
                tasks.AddRange(GetPages(i));
            }

            PagedList<MovieResult> model = new PagedList<MovieResult>(tasks, page, pageSize);
            return new SuccessDataResult<PagedList<MovieResult>>(model);
        }

        //filmleri periyodik olarak çekme
        public IDataResult<List<MovieResult>> GetMoviesThread()
        {
            var tasks = new List<MovieResult>();

            for (int i = 1; i < 50; i++)
            {
                tasks.AddRange(GetPages(i));
                Thread.Sleep(5000);
            }

            return new SuccessDataResult<List<MovieResult>>(tasks);
        }

        //yardımcı fonksiyon
        public List<MovieResult> GetPages(int pageNumber)
        {

            HttpWebRequest apiRequest = WebRequest.CreateHttp("https://api.themoviedb.org/3/discover/movie?api_key=" + apiKey + "&language=en-US" + "&sort_by=popularity.desc&include_adult=false&include_video=false&page=" + pageNumber) as HttpWebRequest;

            string apiResponse = "";

            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
            }

            Response rootObject = JsonConvert.DeserializeObject<Response>(apiResponse);

            return rootObject.results;
        }

        //belirnen filmi mail olarak yollama
        public IResult MailSend(string email, int movieId)
        {
            var movie = GetMovieFromApiById(movieId);

            var genresData = JsonConvert.SerializeObject(movie.Data.genres, Formatting.None);
            var productionCompaniesData= JsonConvert.SerializeObject(movie.Data.production_companies, Formatting.None);
            var productionCountriesData = JsonConvert.SerializeObject(movie.Data.production_countries, Formatting.None);


            List<string> mailTo = new List<string>();
            List<string> mailCC = new List<string>();
            List<string> mailBCC = new List<string>();

            
            mailTo.Add(email);
            string subject = "Movie Advice";
            string body = "<b>Adult:</b> " + movie.Data.adult + "<br></br>" + "<b>Backdrop Path</b>: " + movie.Data.backdrop_path + "<br></br>" + "<b>Budget:</b> " + movie.Data.budget + "<br></br>" + "<b>Genres:</b> " + genresData + "<br></br>" + "<b>Id:</b> " + movie.Data.id + "<br></br>" + "<b>Imdb Id:</b> " + movie.Data.imdb_id + "<br></br>" + "<b>Original Language:</b> " + movie.Data.original_language + "<br></br>" + "<b>Original Title:</b> " + movie.Data.original_title + "<br></br>" + "<b>Overview:</b> " + movie.Data.overview + "<br></br>" + "<b>Popularity:</b> " + movie.Data.popularity + "<br></br>" + "<b>Production Companies:</b> " + productionCompaniesData + "<br></br>" + "<b>Production Countries:</b> " + productionCountriesData + "<br></br>" + "<b>Release Date:</b> " + movie.Data.release_date + "<br></br>" + "<b>Status:</b> " + movie.Data.status + "<br></br>" + "<b>Title:</b> " + movie.Data.title + "<br></br>" + "<b>Vote Average:</b> " + movie.Data.vote_average + "<br></br>" + "<b>Vote Count:</b> " + movie.Data.vote_average;
            MailExtensions.MailSend(mailTo, mailCC, mailBCC, subject, body);

            return new SuccessResult(Messages.MailSuccessful);
        }
        
        //id'si verilen filmi getirme
        public IDataResult<MovieApiResult> GetMovieFromApiById(int movieId)
        {
            HttpWebRequest apiRequest = WebRequest.CreateHttp(string.Format("https://api.themoviedb.org/3/movie/{0}?api_key={1}", movieId, apiKey)) as HttpWebRequest;

            string apiResponse = "";

            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
            }

            MovieApiResult rootObject = JsonConvert.DeserializeObject<MovieApiResult>(apiResponse);

            return new SuccessDataResult<MovieApiResult>(rootObject);
        }
    }
}
