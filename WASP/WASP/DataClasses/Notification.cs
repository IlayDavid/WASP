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
        public static Notification[] Get(int[] ids)
        {
            return dal.GetNotifications(ids);
        }

        public Notification Create()
        {
            return dal.CreateNotification(this);
        }

        public Notification Update()
        {
            return dal.UpdateNotification(this);
        }

        public bool Delete()
        {
            return dal.DeleteNotification(Id);
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
                return this.source;
            }
            set
            {
                source = value;
            }
        }
        public User Target
        {
            get
            {
                return this.target;
            }
            set
            {
                target = value;
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
