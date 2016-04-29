using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses
{
    public class Notifications
    {
        private String message;
        private bool isNew;
        private User source;
        private User target;
        public Notifications(String message, bool isNew,User source, User target)
        {
            this.message = message;
            this.isNew = isNew;
            this.source = source;
            this.target = target;
        }

        public bool IsNew
        {
            get
            {
                return isNew;
            }
            set
            {
                isNew = value;
            }
        }
        public User Source
        {
            get
            {
                return this.source;
            }
        }
        public User Target
        {
            get
            {
                return this.target;
            }
        }






    }

}
