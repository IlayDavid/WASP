using System;
using System.Collections.Generic;
using System.Linq;
using WASP.DataClasses.Policies;

namespace WASP.DataClasses
{
    public class Forum
    {
        private static int _idCounter = 0;
        private int _id;
        private String _name, _description;
        private List<Subforum> subforums;
        private List<Member> members;
        private List<Member> admins;
        private Policy policy;
        public Forum(String name, String description, Policy policy)
        {
            _id = _idCounter++;
            _name = name;
            _description = description;
            this.subforums = new List<Subforum>();
            this.members = new List<Member>();
            this.admins = new List<Member>();
            this.policy = policy;
        }

        public void AddPolicy(Policy policy)
        {
            policy.Next = this.policy;
            this.policy = policy;
        }

        void CheckMemberPolicy(User user)
        {
            this.policy.Validate(user);
        }

        internal Subforum GetSubForum(int subforumId)
        {
            return subforums.First((x) => x.Id == subforumId);
        }

        internal bool IsAdmin(Member member)
        {
            return admins.Contains(member);
        }

        internal bool IsMember(Member member)
        {
            return members.Contains(member);
        }
        internal bool IsSubForum(int subforumId)
        {
            return subforums.First((x) => x.Id == subforumId) != null;
        }

        internal void DefinePolicy(Forum forum)
        {
            throw new NotImplementedException();
        }

        internal Member GetMember(string username)
        {
            return members.First(member => member.UserName.Equals(username));
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
                return _name;
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