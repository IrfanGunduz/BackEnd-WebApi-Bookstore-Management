using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Applications.GenreOperations.Queries.GetGenres{


    public class GetGenresQuery{
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;
        public GetGenresQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle(){
            var genres = _context.Genres.Where(x=> x.IsActive).OrderBy(x=> x.Id);
            List<GenresViewModel> returnObj = _mapper.Map<List<GenresViewModel>>(genres);
            return returnObj;
        }
    }

    public class GenresViewModel{
        public int Id {get; set;}
        public string Name { get; set; }
    }
}