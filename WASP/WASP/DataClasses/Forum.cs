using System;
using System.Collections.Generic;
using System.Linq;
using WASP.Config;
namespace WASP.DataClasses
{
    public class Forum
    {
        private int id;
        private String name, description;
        private Dictionary<int, Subforum> subforums = null;
        private Dictionary<int, User> members = null;
        private Dictionary<int, Admin> admins = null;
        private Policy policy;
        private static DAL2 dal = WASP.Config.Settings.GetDal();

        public Forum initialize()
        {
            Object obj = this.Subforums;
            obj = this.Members;
            obj = this.Admins;
            return this;
        }

        public static Forum Get(int id)
        {
            if (Settings.UseCache())
                return WASP.Config.Settings.GetCache().GetForum(id);
            return dal.GetForum(id).initialize();
        }
        public static Forum[] Get(int[] ids)
        {
            return dal.GetForums(ids);
        }
        public Forum Create()
        {
            return dal.CreateForum(this);
        }

        public Forum Update()
        {
            return dal.CreateForum(this);
        }

        public bool Delete()
        {
            return dal.DeleteForum(Id);
        }

        public Forum(int id, String name, String description, Policy policy)
        {
            this.name = name;
            this.description = description;
            this.policy = policy;
            this.id = id;
        }

        // DEPRECATED
        public Forum(int id, String name, String description, Policy policy, DAL2 dal)
        {
            this.name = name;
            this.description = description;
            this.policy = policy;
            this.id = id;
        }
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }




        private Dictionary<int, Subforum> Subforums
        {
            get
            {
                if (subforums == null)
                {
                    subforums = new Dictionary<int, Subforum>();
                    foreach (Subforum sf in dal.GetForumSubForums(Id))
                    {
                        subforums.Add(sf.Id, sf);
                    }
                }
                return subforums;
            }
        }
        private Dictionary<int, User> Members
        {
            get
            {
                if (members == null)
                {
                    members = new Dictionary<int, User>();
                    foreach (User user in dal.GetForumMembers(Id))
                    {
                        members.Add(user.Id, user);
                    }
                }
                return members;
            }
        }
        private Dictionary<int, Admin> Admins
        {
            get
            {
                if (admins == null)
                {
                    admins = new Dictionary<int, Admin>();
                    foreach (Admin admin in dal.GetForumAdmins(Id))
                    {
                        admins.Add(admin.Id, admin);
                    }
                }
                return admins;
            }
        }


        public Policy Policy
        {
            get
            {
                return policy;
            }
            set
            {
                policy = value;
            }
        }



        public Subforum[] GetAllSubForums()
        {
            Subforum[] sfArr = new Subforum[Subforums.Values.Count];
            Subforums.Values.CopyTo(sfArr, 0);
            return sfArr;
        }
        public Subforum GetSubForum(int sfId)
        {
            Subforum sf;
            Subforums.TryGetValue(sfId, out sf);
            return sf;
        }
        public Subforum[] GetSubForums()
        {
            Subforum[] subArr = new Subforum[Subforums.Values.Count];
            Subforums.Values.CopyTo(subArr, 0);
            return subArr;
        }
        internal bool IsSubForum(int sfid)
        {
            return Subforums.ContainsKey(sfid);
        }
        public bool RemoveSubForum(Subforum subforum)
        {
            return Subforums.Remove(subforum.Id);
        }
        internal void AddSubForum(Subforum subforum)
        {
            Subforums.Add(subforum.Id, subforum);
        }


        internal bool IsMember(int memberid)
        {
            return Members.ContainsKey(memberid);
        }
        public User[] GetMembers()
        {
            User[] userArr = new User[Members.Values.Count];
            Members.Values.CopyTo(userArr, 0);
            return userArr;
        }
        internal User GetMember(int id)
        {
            User mem;
            Members.TryGetValue(id, out mem);
            return mem;
        }
        public void AddMember(User member)
        {
            Members.Add(member.Id, member);
        }
        public bool RemoveMember(User member)
        {
            return Members.Remove(member.Id);
        }
        
        
        internal bool IsAdmin(int adminid)
        {
            return Admins.ContainsKey(adminid);
        }
        public Admin[] GetAdmins()
        {
            Admin[] adminArr = new Admin[Admins.Values.Count];
            Admins.Values.CopyTo(adminArr, 0);
            return adminArr;
        }
        public void AddAdmin(Admin admin)
        {
            Admins.Add(admin.Id, admin);
        }
        public Admin GetAdmin(int Id)
        {
            return Admins[Id];
        }
        public bool RemoveAdmin(Admin admin)
        {
            return Admins.Remove(admin.Id);
        }

        internal void NotifyAllMembers(Notification notification)
        {
            foreach (User member in GetMembers())
            {
                member.NewNotification(new Notification(-1, notification.Message, notification.IsNew,
                    notification.Source, member, notification.Type));
            }
        }
    }
}