namespace WebApplicationWizardAPi.Models.DTO
{
    public class WizardDataDTO
    {
        public long Id { get; set; }
        public string? WizardIndex { get; set; }
        public PageDTO? Page { get; set; }
        public ICollection<AnswerDTO>? Answers { get; set; }
    }
}
