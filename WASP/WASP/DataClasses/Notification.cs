using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses
{
    public class Notification
    {
        private String message;
        private bool isNew;
        private User source;
        private User target;
        private int id;
        public Notification(int id, String message, bool isNew, User source, User target)
        {
            this.id = id;
            this.message = message;
            this.isNew = isNew;
            this.source = source;
            this.target = target;
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
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

        public String Message
        {
            get;
            set;
        }
    }
}
