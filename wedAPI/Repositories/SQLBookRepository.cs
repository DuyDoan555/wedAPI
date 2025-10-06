using WebAPI_simple.Data;
using WebAPI_simple.Models.Domain;
using WebAPI_simple.Models.DTO;

namespace WebAPI_simple.Repositories
{
    public class SQLBookRepository : IBookRepository
    {
        private readonly AppDbContext _dbContext;

        public SQLBookRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        public List<BookWithAuthorAndPublisherDTO> GetAllBooks(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 1000)
        {
            var allBooks = _dbContext.Books.Select(b => new BookWithAuthorAndPublisherDTO()
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                IsRead = b.IsRead,
                DateRead = b.IsRead ? b.DateRead.Value : null,
                Rate = b.IsRead ? b.Rate.Value : null,
                Genre = b.Genre,
                CoverUrl = b.CoverUrl,
                PublisherName = b.Publisher.Name,
                AuthorNames = b.Book_Authors.Select(n => n.Author.FullName).ToList()
            }).AsQueryable();

            
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("title", StringComparison.OrdinalIgnoreCase))
                {
                    allBooks = allBooks.Where(x => x.Title.Contains(filterQuery));
                }
            }

           
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("title", StringComparison.OrdinalIgnoreCase))
                {
                    allBooks = isAscending
                        ? allBooks.OrderBy(x => x.Title)
                        : allBooks.OrderByDescending(x => x.Title);
                }
            }

            
            var skipResults = (pageNumber - 1) * pageSize;
            return allBooks.Skip(skipResults).Take(pageSize).ToList();
        }

        
        public BookWithAuthorAndPublisherDTO GetBookById(int id)
        {
            var book = _dbContext.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return null;

            var dto = new BookWithAuthorAndPublisherDTO
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher?.Name,
                AuthorNames = _dbContext.Books_Authors
                    .Where(ba => ba.BookId == book.Id)
                    .Select(ba => ba.Author.FullName)
                    .ToList()
            };
            return dto;
        }

        
        public AddBookRequestDTO AddBook(AddBookRequestDTO addBookRequestDTO)
        {
            var book = new Book
            {
                Title = addBookRequestDTO.Title,
                Description = addBookRequestDTO.Description,
                IsRead = addBookRequestDTO.IsRead,
                DateRead = addBookRequestDTO.IsRead ? addBookRequestDTO.DateRead.Value : null,
                Rate = addBookRequestDTO.IsRead ? addBookRequestDTO.Rate.Value : null,
                Genre = addBookRequestDTO.Genre,
                CoverUrl = addBookRequestDTO.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherID = addBookRequestDTO.PublisherID
            };

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

            
            foreach (var authorId in addBookRequestDTO.AuthorIds)
            {
                var bookAuthor = new Book_Author
                {
                    BookId = book.Id,
                    AuthorId = authorId
                };
                _dbContext.Books_Authors.Add(bookAuthor);
            }

            _dbContext.SaveChanges();

            return addBookRequestDTO;
        }

        
        public AddBookRequestDTO? UpdateBookById(int id, AddBookRequestDTO bookDTO)
        {
            var book = _dbContext.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return null;

            book.Title = bookDTO.Title;
            book.Description = bookDTO.Description;
            book.IsRead = bookDTO.IsRead;
            book.DateRead = bookDTO.IsRead ? bookDTO.DateRead.Value : null;
            book.Rate = bookDTO.IsRead ? bookDTO.Rate.Value : null;
            book.Genre = bookDTO.Genre;
            book.CoverUrl = bookDTO.CoverUrl;
            book.PublisherID = bookDTO.PublisherID;

            _dbContext.SaveChanges();

            return bookDTO;
        }

        
        public Book? DeleteBookById(int id)
        {
            var book = _dbContext.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return null;

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
            return book;
        }
    }
}
