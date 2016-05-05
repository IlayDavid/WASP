using System;
using System.Collections.Generic;
using System.Linq;
using WASP.DataClasses.Policies;

namespace WASP.DataClasses
{
    public class Forum
    {
        private int id;
        private String name, description;
        private Dictionary<int, Subforum> subforums;
        private Dictionary<int, User> members;
        private Dictionary<int, Admin> admins;
        private Dictionary<int, Policy> policy;
        private DAL dal;
        public Forum(int id, String name, String description, Dictionary<int, Policy> policy,DAL dal)
        {
            this.name = name;
            this.description = description;
            this.policy = policy;
            this.members = new Dictionary<int, User>();
            this.admins = new Dictionary<int, Admin>();
            this.subforums = new Dictionary<int, Subforum>();
            this.dal = dal;

            this.id = id;

        }

        

        public void AddPolicy(Policy policy)
        {
            //this.policy.Add(policy.Id, policy);
        }




        public Subforum[] GetAllSubForums()
        {
            Subforum[] sfArr = new Subforum[subforums.Values.Count];
            subforums.Values.CopyTo(sfArr, 0);
            return sfArr;
        }

        public Subforum GetSubForum(int sfId)
        {
            Subforum sf;
            subforums.TryGetValue(sfId, out sf);
            return sf;
        }





        internal bool IsAdmin(int adminid)
        {
            return admins.ContainsKey(adminid);
        }

        internal bool IsMember(int memberid)
        {
            return members.ContainsKey(memberid);
        }
        internal bool IsSubForum(int sfid)
        {
            return subforums.ContainsKey(sfid);
        }

        internal void DefinePolicy(Forum forum)
        {
            throw new NotImplementedException();
        }

        internal User GetMember(int id)
        {
            User mem;
            members.TryGetValue(id, out mem);
            return mem;
        }

        internal void Update(Forum forum)
        {
            throw new NotImplementedException();
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






        public User[] GetMembers()
        {
            User[] userArr = new User[members.Values.Count];
            members.Values.CopyTo(userArr, 0);
            return userArr;
        }

        public Admin[] GetAdmins()
        {
            Admin[] adminArr = new Admin [admins.Values.Count];
            admins.Values.CopyTo(adminArr, 0);
            return adminArr;
        }

      
      

        public Subforum[] GetSubForum()
        {
            Subforum[] subArr = new Subforum[subforums.Values.Count];
            subforums.Values.CopyTo(subArr, 0);
            return subArr;
        }

       


 


        public void AddAdmin(Admin admin)
        {
            admins.Add(admin.Id, admin) ;
        }
        public Admin GetAdmin(int Id)
        {
            return admins[Id];
        }

        public void AddMember(User member)
        {
            members.Add(member.Id,member);
            
        }
        internal void AddSubForum(Subforum subforum)
        {
            subforums.Add(subforum.Id,subforum);
        }

        public bool RemoveAdmin(Admin admin)
        {
            return admins.Remove(admin.Id);
        }
        public bool RemoveMember(User member)
        {
            return members.Remove(member.Id);
        }
        public bool RemoveSubForum(Subforum subforum)
        {
            return subforums.Remove(subforum.Id);
        }
    }
}