using WebApplicationWizardAPi.Models;
using WebApplicationWizardAPi.Models.DTO;
using WebApplicationWizardAPi.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace WebApplicationWizardAPi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IDataRepository<User, UserDTO> _dataRepository;

        public UserController(IDataRepository<User, UserDTO> dataRepository)
        {
            _dataRepository = dataRepository;
        }
        ////////////////////use/////////////////////////////////
        [HttpGet]
        public IActionResult Get()
        {
            var Users = _dataRepository.GetAll();
            return Ok(Users);
        }
        ////////////////////use/////////////////////////////////
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _dataRepository.Get(id);
            if (user == null)
                return NotFound("User not found.");
            Dictionary<int, string> My_dict =
                    new Dictionary<int, string>();
            for (int i = 0; i < user.Wizards.Count; i++)
            {

                My_dict.Add((int)user.Wizards.ToArray()[i].Hashnum, (string)user.Wizards.ToArray()[i].Titel);
            }

            return Ok(My_dict);
        }
        ////////////////////use/////////////////////////////////
        [HttpGet("{email}/{pass}")]
        public IActionResult Get(string email, string pass)
        {
            if (pass == "00")
            {
                if (_dataRepository.Check(email))
                    return Ok(true);
                return NotFound(false);
            }

            var user = _dataRepository.GetUser(email, pass);
            
            if (user == null)
                return NotFound("User not found.");
            user.Password = "";
            return Ok(user);


        }
        ////////////////////use/////////////////////////////////
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (user is null)
            {
                return BadRequest("Author is null.");
            }
            if (_dataRepository.Check(user.Email))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _dataRepository.Add(user);
            return Ok(user);
        }

       
        /*[HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("user is null.");
            }

            var userToUpdate = _dataRepository.Get(id);
            if (userToUpdate == null)
            {
                return NotFound("The User record couldn't be found.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _dataRepository.Update(userToUpdate, user);
            return NoContent();
        }*/
        ////////////////////use/////////////////////////////////
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _dataRepository.Get(id);
            if (user == null)
            {
                return NotFound("the user record couldn't be found.");
            }
            _dataRepository.Delete(user);
            return Ok("OK");
        }
    }
}
