using System;

namespace Client.DataClasses
{
    public class Notification
    {
        public enum Types : int
        {
            Message = 1, Post = 0
        }
        public string message;
        private bool isNew;
        private User source;
        private int sourceID;
        private User target;
        private int targetID;
        private int id;
        private DateTime creationTime;
        private Types type;

        public Notification(int id, String message, bool isNew, User source, User target)
        {
            this.id = id;
            this.message = message;
            this.isNew = isNew;
            this.source = source;
            this.target = target;
            this.type = Types.Post;
            this.creationTime = DateTime.Now;
        }
        public Notification(int id, String message, bool isNew, User source, User target, Types type)
        {
            this.id = id;
            this.message = message;
            this.isNew = isNew;
            this.source = source;
            this.target = target;
            this.type = type;
            this.creationTime = DateTime.Now;
        }

        public Notification(int id, String message, bool isNew, User source, User target, Types type, DateTime creationTime)
        {
            this.id = id;
            this.message = message;
            this.isNew = isNew;
            this.source = source;
            this.target = target;
            this.type = type;
            this.creationTime = creationTime;
        }
        public Notification(int id, String message, bool isNew, int source, int target, Types type)
        {
            this.id = id;
            this.message = message;
            this.isNew = isNew;
            this.sourceID = source;
            this.targetID = target;
            this.type = type;
        }

        public void setSourceUser(User source)
        {
            this.source = source;
        }

        public void setTargetUser(User target)
        {
            this.target = target;
        }

    }


}