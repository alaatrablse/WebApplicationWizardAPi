using System.Collections.Generic;

namespace WebApplicationWizardAPi.Models.DTO
{
    public class AnswerDTO
    {
        public long Id { get; set; }
        public string? Answer1 { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; } 
        public WizardDataDTO? WizaedData { get; set; }
    }
}
