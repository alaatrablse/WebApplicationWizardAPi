using System;
using System.Collections.Generic;

namespace WebApplicationWizardAPi
{
    public partial class Answer
    {
        public long Id { get; set; }
        public string? Answer1 { get; set; }
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public long WizardDatumId { get; set; }

        /*public virtual WizardDatum WizaedData { get; set; } = null!;*/
    }
}
