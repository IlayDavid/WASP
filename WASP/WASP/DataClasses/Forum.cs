using System;
using System.Collections.Generic;
using System.Linq;
using WASP.DataClasses.Policies;

namespace WASP.DataClasses
{
    public class Forum
    {
        private int _id;
        private String name, description;
        private Dictionary<int, Subforum> subforums;
        private Dictionary<int, User> members;
        private Dictionary<int, Admin> admins;
        private Dictionary<int, Policy> policy;
        public Forum(String name, String description, Dictionary<int, Policy> policy)
        {
            this.name = name;
            this.description = description;
            this.policy = policy;
            this.members = new Dictionary<int, User>();
            this.admins = new Dictionary<int, Admin>();

        }

        

        public void AddPolicy(Policy policy)
        {
            this.policy.Add(policy.Id, policy);
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

        internal Member GetMember(string username)
        {
     
        }

        internal void Update(Forum forum)
        {
            throw new NotImplementedException();
        }

        public int Id
        {
            get
            {
                return _id;
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
                _name = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        public List<Member> GetMembers()
        {
            return members;
        }

        public List<Member> GetAdmins()
        {
            return admins;
        }

        public List<Subforum> GetSubForum()
        {
            return subforums;
        }

        public void AddAdmin(Member admin)
        {
            admins.Add(admin);
        }

        public void AddMember(Member member)
        {
            members.Add(member);
        }
        internal void AddSubForum(Subforum subforum)
        {
            subforums.Add(subforum);
        }

        public bool RemoveAdmin(Member member)
        {
            return admins.Remove(member);
        }
        public bool RemoveMember(Member member)
        {
            return members.Remove(member);
        }
        public bool RemoveSubForum(Subforum subforum)
        {
            return subforums.Remove(subforum);
        }
    }
}