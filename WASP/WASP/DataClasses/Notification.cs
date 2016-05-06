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
        private static DAL2 dal = WASP.Config.Settings.GetDal();

        public static Notification Get(int id)
        {
            return dal.GetNotification(id);
        }

        public Notification(int id, String message, bool isNew, User source, User target)
        {
            this.id = id;
            this.message = message;
            this.isNew = isNew;
            this.source = source;
            this.target = target;
        }

        // DEPRECATED
        public Notification(int id, String message, bool isNew, User source, User target, DAL2 dal)
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
                //if (this.source == null)
                  //  this.source = this.dal.GetNotificationSource(this.Id);

                return this.source;
            }
        }
        public User Target
        {
            get
            {
                //if (this.target == null)
                 //   this.target = this.dal.GetNotificationTarget(this.Id);

                return this.target;
            }
        }

        public String Message
        {
            get
            {
                return this.message;
            }
        }
    }
}
