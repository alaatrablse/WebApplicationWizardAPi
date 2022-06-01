using System.Collections.Generic;

namespace WebApplicationWizardAPi.Models.DTO
{
    public class WizardDTO
    {
        public long Id { get; set; }
        public string? Titel { get; set; }
        public string? Description { get; set; }
        public long? Hashnum { get; set; }
        public UserDTO? User { get; set; }
        public ICollection<PageDTO>? Pages { get; set; }
    }
}
