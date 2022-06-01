using System.Collections.Generic;

namespace WebApplicationWizardAPi.Models.DTO
{
    public class PageDTO
    {
        public long Id { get; set; }
        public string? WizardType { get; set; }
        public int NumPages { get; set; }
        public Wizard? Wizard { get; set; }
       /* public ICollection<WizardDataDTO>? WizardData { get; set; }*/
    }
}
