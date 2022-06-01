using System;
using System.Collections.Generic;

namespace WebApplicationWizardAPi
{
    public partial class WizardDatum
    {
        public WizardDatum()
        {
            Answers = new HashSet<Answer>();
        }

        public long Id { get; set; }
        public string? WizardIndex { get; set; }
        public long PageId { get; set; }

       /* public virtual Page Page { get; set; } = null!;*/
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
