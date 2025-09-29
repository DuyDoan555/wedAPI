using Microsoft.AspNetCore.Mvc;
using WebAPI_simple.Models.DTO;
using WebAPI_simple.Repositories;

namespace WebAPI_simple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublishersController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAll()
        {
            var pubs = _publisherRepository.GetAllPublishers();
            return Ok(pubs);
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            var pub = _publisherRepository.GetPublisherById(id);
            if (pub == null) return NotFound();
            return Ok(pub);
        }

        [HttpPost("add-publisher")]
        public IActionResult Add([FromBody] AddPublisherRequestDTO addDTO)
        {
            var created = _publisherRepository.AddPublisher(addDTO);
            return Ok(created);
        }

        [HttpPut("update-publisher-by-id/{id}")]
        public IActionResult Update(int id, [FromBody] PublisherNoIdDTO dto)
        {
            var updated = _publisherRepository.UpdatePublisherById(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _publisherRepository.DeletePublisherById(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}
