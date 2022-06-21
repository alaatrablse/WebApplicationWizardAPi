using WebApplicationWizardAPi.Models;
using WebApplicationWizardAPi.Models.DTO;
using WebApplicationWizardAPi.Models.Repository;
using Microsoft.AspNetCore.Mvc;


namespace WebApplicationWizardAPi.Controllers
{
    [Route("api/wizards")]
    [ApiController]
    public class WizardController: ControllerBase
    {
        private readonly IDataRepository<Wizard, WizardDTO> _dataRepository;

        public WizardController(IDataRepository<Wizard, WizardDTO> dataRepository)
        {
            _dataRepository = dataRepository;
        }
        /////////////////use///////////////////
        [HttpGet]
        public IActionResult Get()
        {
            var wizards = _dataRepository.GetAll();
            return Ok(wizards);
        }

        /*[HttpGet("answer/{id}")]
        public IActionResult Get1(int id)
        {
            var wizards = _dataRepository.GetDto(id);
            return Ok("yes");
        }*/

        /////////////////use///////////////////
        [HttpGet("{Hashnum}")]
        public IActionResult Get(int Hashnum)
        {
            var wizard = _dataRepository.GetDto(Hashnum);
            if (wizard == null)
                return NotFound("Wizard not found.");
            return Ok(wizard);
        }
        /////////////////use///////////////////
        [HttpPost]
        public IActionResult Post([FromBody] Wizard wizard)
        {
            if (wizard is null)
            {
                return BadRequest("wizard is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var ss = wizard.Hashnum;
            if(wizard.Hashnum == 0)
            {
                Random random = new Random();
                // Any random integer   
                int num = random.Next(1000, 50000);

                while (_dataRepository.Check(num) == true)
                {
                    num = random.Next(1000, 50000);
                }
                wizard.Hashnum = num;
                _dataRepository.Add(wizard);
                return Ok(wizard);
            }
            else if (_dataRepository.Check(wizard.Hashnum) == true)
            {
                var wizardToUpdate = _dataRepository.Get(wizard.Id);
                if (wizardToUpdate == null)
                {
                    return NotFound("The User record couldn't be found.");
                }
                _dataRepository.Update(wizardToUpdate,wizard);
                return Ok(wizard);
            }

            return NotFound("error");
        }
        /////////////////use///////////////////
        [HttpDelete("{Hashnum}/{wizardid}")]
        public IActionResult Delete(int Hashnum, int wizardid)
        {
            var wizard = _dataRepository.GetDto(Hashnum);
            if (wizard == null)
            {
                return NotFound("the user record couldn't be found.");
            }
            if (wizard.UserId == wizardid)
            {
                _dataRepository.Delete(wizard);
                return Ok(wizard);
            }
            return NotFound("error");
           
        }

    }
}
