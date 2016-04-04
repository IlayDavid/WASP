using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace WASP
{
    public class Forum
    {
        private static int _idCounter=0;
        private int _id;
        private String _name, _description;
        private List<Subforum> subforums = new List<Subforum>();
        private List<Member> members=new List<Member>();
        private List<Member> admins=new List<Member>();
        public Forum(String name, String description)
        {
            _id=_idCounter;
            _idCounter++;
            _name = name;
            _description = description;
        }


        internal Subforum GetSubForum(int subforumId)
        {
            return subforums.First((x) => x.Id == subforumId);
        }

        internal bool IsAdmin (Member member)
        {
            return admins.Contains(member);
        }

        internal bool IsMember(Member member)
        {
            return members.Contains(member);
        }
        internal bool IsSubForum(int subforumId)
        {
            return subforums.First((x) => x.Id == subforumId)!=null;
        }

        internal void DefinePolicy(Forum forum)
        {
            throw new NotImplementedException();
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

        public List<Member> GetMembers ()
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

        public void AddAdmin (Member admin)
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

        public bool RemoveAdmin (Member member)
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