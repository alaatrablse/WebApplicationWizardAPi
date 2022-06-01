using System.Collections.Generic;

namespace WebApplicationWizardAPi.Models.DTO
{
    public class UserDTO
    {
        public UserDTO()
        {

        }

        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Type { get; set; }
        public ICollection<WizardDTO>? Wizards { get; set; }
    }
}
