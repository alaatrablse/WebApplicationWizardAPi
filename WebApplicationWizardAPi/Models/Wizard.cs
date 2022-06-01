using System;
using System.Collections.Generic;

namespace WebApplicationWizardAPi
{
    public partial class Wizard
    {
        public Wizard()
        {
            Pages = new HashSet<Page>();
        }

        public long Id { get; set; }
        public string? Titel { get; set; }
        public string? Description { get; set; }
        public long UserId { get; set; }
        public long Hashnum { get; set; }

        /*public virtual User User { get; set; } = null!;*/
        public virtual ICollection<Page> Pages { get; set; }
    }
}
