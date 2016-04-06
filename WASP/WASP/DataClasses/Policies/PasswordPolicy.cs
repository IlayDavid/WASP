using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses.Policies
{
    public class PasswordPolicy : Policy
    {
        Policy nextPolicy;
        bool diverse;
        int length;

        public Policy Next { get; set; }

        public PasswordPolicy(Policy next = null, bool diverse = false,  int length = 0)
        {
            this.nextPolicy = next;
            this.diverse = diverse;
            this.length = length;
        }

        public void Validate(User user)
        {
            if (this.length > 0 && user.Password.Length >= this.length)
                throw new Exception("Password must be at least " + this.length + " characters long.");
            if (this.diverse)
            {
                // Numeric Character - At least one character
                if (!user.Password.Any(c => char.IsDigit(c)))
                    throw new Exception("Password must contain at least one numeric character.");
                // Letter Character - At least one character
                if (!user.Password.Any(c => char.IsLetter(c)))
                    throw new Exception("Password must contain at least one letter.");
            }
            if (nextPolicy != null)
                this.nextPolicy.Validate(user);
        }
    }
}
