using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.GenreOperations.DeleteGenre{

    public class DeleteGenreCommmand{
        public int GenreId {get; set;}

        private readonly IBookStoreDbContext _context;

        public DeleteGenreCommmand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle(){
            var genre = _context.Genres.SingleOrDefault(x=> x.Id == GenreId);
            if(genre == null)
                throw new InvalidOperationException("Silinecek Kitap Türü Bulunamadı!");
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }


}
