﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WASP
{
    public class Forum
    {
        private int id;
        private String name, description;
        private Dictionary<int, Subforum> subforums;
        private Dictionary<int, User> members;
        private Dictionary<int, User> admins;

        public Forum(int id,String name, String description)
        {
            subforums = new Dictionary<int, Subforum>();
            this.id = id;
            this.name = name;
            this.description = description;
        }


        internal Subforum GetSubForum(int sf_ID)
        {
            Subforum tempForum;
            return subforums.TryGetValue(sf_ID,out tempForum) ? tempForum : null;
        }


        internal bool IsAdmin (int user_ID)
        {
            return admins.ContainsKey(user_ID);
        }

        internal bool IsMember(int user_ID)
        {
            return members.ContainsKey(user_ID);
        }

        internal Thread FindThread(int thread_ID)
        {
            throw new NotImplementedException();
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

        public User[] GetMembers ()
        {
            User[] users = new User[members.Values.Count];
            members.Values.CopyTo(users, 0);
            return users;         
        }

        public User[] GetAdmins()
        {
            User[] adminsArr = new User[admins.Values.Count];
            members.Values.CopyTo(adminsArr, 0);
            return adminsArr;
        }

        public void AddAdmin (User admin)
        {
            admins.Add(admin.Id, admin);
        }

        public void AddMember(User member)
        {
            members.Add(member.Id, member);
        }
        internal void AddSubForum(Subforum sf)
        {
            subforums.Add(sf.Id, sf);
        }

        public void RemoveAdmin (int admin_id)
        {
            admins.Remove(admin_id);
        }
        public void RemoveMember(int member_id)
        {
            members.Remove(member_id);
        }
        public void RemoveSubForum(int sforum_id)
        {
            subforums.Remove(sforum_id);
        }




    }
}