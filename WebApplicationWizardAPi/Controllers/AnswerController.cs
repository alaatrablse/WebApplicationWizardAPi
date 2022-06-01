using WebApplicationWizardAPi.Models;
using WebApplicationWizardAPi.Models.DTO;
using WebApplicationWizardAPi.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationWizardAPi.Controllers
{
    [Route("api/Answer")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IDataRepository<Answer, AnswerDTO> _dataRepository;
        public AnswerController(IDataRepository<Answer, AnswerDTO> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpPost]
        public IActionResult Post([FromBody] List <Answer> answer)
        {
            if (answer is null)
            {
                return BadRequest("wizard is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            foreach (Answer op in answer)
            {
                _dataRepository.Add(op);
            }
            
            return Ok(answer);
        }
    }
}
