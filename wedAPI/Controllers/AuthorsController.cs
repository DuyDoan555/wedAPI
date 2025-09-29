using Microsoft.AspNetCore.Mvc;
using WebAPI_simple.Models.DTO;
using WebAPI_simple.Repositories;

namespace WebAPI_simple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet("get-all-authors")]
        public IActionResult GetAll()
        {
            var authors = _authorRepository.GellAllAuthors();
            return Ok(authors);
        }

        [HttpGet("get-author-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            var author = _authorRepository.GetAuthorById(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpPost("add-author")]
        public IActionResult Add([FromBody] AddAuthorRequestDTO addAuthorDTO)
        {
            var newAuthor = _authorRepository.AddAuthor(addAuthorDTO);
            return Ok(newAuthor);
        }

        [HttpPut("update-author-by-id/{id}")]
        public IActionResult Update(int id, [FromBody] AuthorNoIdDTO authorDTO)
        {
            var updated = _authorRepository.UpdateAuthorById(id, authorDTO);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("delete-author-by-id/{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _authorRepository.DeleteAuthorById(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}
