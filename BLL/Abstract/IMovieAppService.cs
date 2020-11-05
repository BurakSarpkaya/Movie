
using Core.Utility.Results;
using Entities.Dto;
using Entities.MovieApiModel;
using PagedList;
using System.Collections.Generic;

namespace BLL.Abstract
{
    public interface IMovieAppService
    {
        IDataResult<PagedList<MovieResult>> GetMovies(int page, int pageSize);
        IDataResult<List<MovieResult>> GetMoviesThread();
        IResult MovieAddRate(AccountMovieCreateInput accountMovie, int movieId);
        IDataResult<AccountMovie> GetMovieById(int movieId);
        IResult MailSend(string email,int movieId);
        IDataResult<MovieApiResult> GetMovieFromApiById(int movieId);
    }
}
