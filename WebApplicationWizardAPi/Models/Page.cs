using System;
using System.Collections.Generic;

namespace WebApplicationWizardAPi
{
    public partial class Page
    {
        public Page()
        {
            WizardData = new HashSet<WizardDatum>();
        }

        public long Id { get; set; }
        public string? WizardType { get; set; }
        public int NumPages { get; set; }
        public long WizardId { get; set; }

        /*public virtual Wizard Wizard { get; set; } = null!;*/
        public virtual ICollection<WizardDatum> WizardData { get; set; }
    }
}
