using WebAPI_simple.Data;
using WebAPI_simple.Models.Domain;
using WebAPI_simple.Models.DTO;

namespace WebAPI_simple.Repositories
{
    public class SQLPublisherRepository : IPublisherRepository
    {
        private readonly AppDbContext _dbContext;

        public SQLPublisherRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<PublisherDTO> GetAllPublishers()
        {
            return _dbContext.Publishers
                .Select(p => new PublisherDTO()
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList();
        }

        public PublisherNoIdDTO GetPublisherById(int id)
        {
            var pub = _dbContext.Publishers.FirstOrDefault(p => p.Id == id);
            if (pub == null) return null;

            return new PublisherNoIdDTO()
            {
                Name = pub.Name
            };
        }

        public AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO)
        {
            var pub = new Publisher()
            {
                Name = addPublisherRequestDTO.Name
            };
            _dbContext.Publishers.Add(pub);
            _dbContext.SaveChanges();

            return addPublisherRequestDTO;
        }

        public PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO)
        {
            var pub = _dbContext.Publishers.FirstOrDefault(p => p.Id == id);
            if (pub == null) return null;

            pub.Name = publisherNoIdDTO.Name;
            _dbContext.SaveChanges();

            return publisherNoIdDTO;
        }

        public Publisher? DeletePublisherById(int id)
        {
            var pub = _dbContext.Publishers.FirstOrDefault(p => p.Id == id);
            if (pub == null) return null;

            _dbContext.Publishers.Remove(pub);
            _dbContext.SaveChanges();

            return pub;
        }
    }
}
