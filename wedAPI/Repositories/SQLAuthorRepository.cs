using WebAPI_simple.Data;
using WebAPI_simple.Models.Domain;
using WebAPI_simple.Models.DTO;

namespace WebAPI_simple.Repositories
{
    public class SQLAuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _dbContext;

        public SQLAuthorRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<AuthorDTO> GellAllAuthors()
        {
            return _dbContext.Authors
                .Select(a => new AuthorDTO
                {
                    Id = a.Id,
                    FullName = a.FullName
                })
                .ToList();
        }

        public AuthorNoIdDTO GetAuthorById(int id)
        {
            var author = _dbContext.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null) return null;

            return new AuthorNoIdDTO { FullName = author.FullName };
        }

        public AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO)
        {
            var author = new Author { FullName = addAuthorRequestDTO.FullName };
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
            return addAuthorRequestDTO;
        }

        public AuthorNoIdDTO UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO)
        {
            var author = _dbContext.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null) return null;

            author.FullName = authorNoIdDTO.FullName;
            _dbContext.SaveChanges();
            return authorNoIdDTO;
        }

        public Author? DeleteAuthorById(int id)
        {
            var author = _dbContext.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null) return null;

            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
            return author;
        }
    }
}
