using System;
using System.Collections.Generic;

namespace WebApplicationWizardAPi
{
    public partial class User
    {
        public User()
        {
            Wizards = new HashSet<Wizard>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Type { get; set; } = null!;

        public virtual ICollection<Wizard> Wizards { get; set; }
    }
}
