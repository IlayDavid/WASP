using System;
using System.Collections.Generic;
using WASP.DataClasses;
using WASP.Exceptions;


namespace WASP.Domain
{
    class BLFacade : IBL
    {
        private DAL dal;

        public SuperUser initialize(string name, string userName, int ID, string email, string pass)
        {
            throw new NotImplementedException();
        }

        public int isInitialize()
        {
            throw new NotImplementedException();
        }

        public Forum createForum(int userID, string forumName, string description, int adminID, string adminUserName, string adminName, string email, string pass)
        {
            //create new forum with admin in it, create user for admin
            Forum newForum = new Forum(-1, forumName, description, null, dal);
            User user = new User(-1, adminName, adminUserName, email, pass, newForum);
            // TODO: need to check if user and forum are fine with policy
            Admin admin = new Admin(user, newForum, dal);
            newForum.AddMember(user);
            newForum.AddAdmin(admin);

            // DANGER: Need to provide atomicity for this... This is a dangerous spot.
            newForum = dal.CreateForum(newForum);
            dal.CreateUser(user);
            dal.CreateAdmin(admin);

            return newForum;
        }

        public int defineForumPolicy(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        public User subscribeToForum(int id, string userName, string name, string email, string pass, int targetForumID)
        {
            // Should throw exception if forum no found.
            Forum forum = dal.GetForum(targetForumID);

            // Attempt to add user.
            User user = new User(id, name, userName, email, pass, forum);
            // If user doesn't follow forum policy will throw exception.
            forum.AddMember(user);

            // Will throw exception if unable to create user.
            return dal.CreateUser(user);
        }

        public Post createThread(int userID, int forumID, string title, string content, int subForumID)
        {
            // Should throw exception if user no found.
            User author = dal.GetUser(userID, forumID);
            // Should throw exception if subforum no found.
            Subforum sfContainer = dal.GetSubForum(subForumID);

            Post originalPost = new Post(-1, title, content, author, DateTime.Now, null, sfContainer, DateTime.Now, dal);
            // Should throw exception if post doesn't follow forum policy.
            sfContainer.AddThread(originalPost);
            author.AddPost(originalPost);

            return dal.CreatePost(originalPost);
        }


        public Post createReplyPost(int userID, int forumID, string content, int replyToPost_ID)
        {
            User author = dal.GetUser(userID, forumID);
            Post post = dal.GetPost(replyToPost_ID);
            Post reply = new Post(-1, null, content, author, DateTime.Now, post, post.Container, DateTime.Now, dal);

            post.AddReply(reply);
            author.AddPost(reply);

            return dal.CreatePost(reply);
        }

        public Subforum createSubForum(int userID, int forumID, string name, string description, int moderatorID, DateTime term)
        {
            Forum forum = dal.GetForum(forumID);
            Subforum sf = new Subforum(-1, name, description, forum, dal);

            forum.AddSubForum(sf);

            return dal.CreateSubForum(sf);
        }

        public int sendMessage(int userID, int forumID, string targetUserNameID, string message)
        {
            throw new NotImplementedException();
        }

        public Moderator addModerator(int userID, int forumID, int moderatorID, int subForumID, DateTime term)
        {
            Forum forum = dal.GetForum(forumID);
            Subforum sf = dal.GetSubForum(subForumID);
            Admin admin = dal.GetAdmin(userID, forumID);

            Moderator mod = new Moderator(forum.GetMember(moderatorID), term, sf, admin, dal);
            sf.AddModerator(mod);

            return dal.CreateModerator(mod);
        }

        public int updateModeratorTerm(int userID, int forumID, int moderatorID, int subforumID, DateTime term)
        {
            Moderator mod = dal.GetModerator(moderatorID, subforumID);
            Admin admin = dal.GetAdmin(userID, forumID);
            if (mod.Appointer.Id != admin.Id)
                throw new UnauthorizedEditModTerm(userID, moderatorID, subforumID);

            mod.TermExp = term;
            mod.SubForum.AddModerator(mod);
            dal.updateModerator(mod);

            return 1;
        }

        public int confirmEmail(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        public int deletePost(int userID, int forumID, int postID)
        {
            Post post = dal.GetPost(postID);

            post.Delete();

            return 1;
        }

        public int editPost(int userID, int forumID, int postID, string content)
        {
            User user = dal.GetUser(userID, forumID);
            Post post = dal.GetPost(postID);
            Forum forum = dal.GetForum(forumID);
            if (user.Id != post.GetAuthor.Id && forum.IsAdmin(userID))
                throw new UnauthorizedEditPost(userID, postID, post.Container.Id);
            post.Content = content;
            dal.UpdatePost(post);

            return 1;
        }

        public int deleteModerator(int userID, int forumID, int moderatorID, int subForumID)
        {
            Admin admin = dal.GetAdmin(userID, forumID);
            Moderator mod = dal.GetModerator(moderatorID, subForumID);
            if (mod.Appointer.Id != admin.Id)
                throw new UnauthorizedDeleteModerator(userID, moderatorID);
            dal.DeleteModerater(mod.Id, subForumID);
            return 1;
        }
    
    

        public int subForumTotalMessages(int userID, int forumID, int subForumID)
        {
            int counter = 0;
            Subforum sf = dal.GetSubForum(subForumID);
            Post[] threads = sf.GetThreads();
            foreach (Post post in threads)
            {
                if (post.GetAuthor.Id == userID)
                    counter++;
            }
            return counter;
        }
        public int memberTotalMessages(int userID, int forumID)
        {
            int counter = 0;
            Forum forum = dal.GetForum(forumID);
            Subforum [] sfArr = forum.GetAllSubForums();
            foreach (Subforum sf in sfArr)
            {
                counter += subForumTotalMessages(userID, forumID, sf.Id);
            }
            return counter;
        }

        public ModeratorReport moderatorReport(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        public int totalForums(int userID)
        {
            List<User> usersInDiffForums = membersInDifferentForums(userID);
            return usersInDiffForums.Count;
        }

        public List<User> membersInDifferentForums(int userID)
        {
            List<User> userList = new List<User>();
            Forum[] forums = dal.GetForumsUserID(userID);
            foreach (Forum forum in forums)
                
            {
                User user = forum.GetMember(userID);
                userList.Add(user);
            }
            return userList;
        }

        public User login(string userName, string password, int forumID)
        {
            throw new NotImplementedException();
        }

        public SuperUser loginSU(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Post getThread(int userID, int forumID, int threadId)
        {
            Post post = dal.GetPost(threadId);
            return post;
        }

        public List<Post> getThreads(int forumID, int subForumID, int from, int amount)
        {
            throw new NotImplementedException();
            //TODO : amitay needs to delete this
        }

        public Forum getForum(int userID, int forumID)
        {
            throw new NotImplementedException();
            // TODO: amitay needs to delete this
        }

        public Forum getForum(int forumID)
        {
            return dal.GetForum(forumID);
        }

        public Subforum getSubforum(int userID, int forumID, int subforumId)
        {
            //TODO: amitay needs to delete this
        }

        public Subforum getSubforum(int forumID, int subforumId)
        {
            return dal.GetSubForum(subforumId);
        }

        public List<Moderator> getModerators(int forumID, int subForumID)
        {

            Moderator [] mods = dal.GetModeratorsInSubForum(subForumID);
            List<Moderator> modsList = new List<Moderator>();
            foreach (Moderator mod in mods)
            {
                modsList.Add(mod);
            }
            return modsList;
        }

        public DateTime getModeratorTermTime(int userID, int forumID, int moderatorID, int subforumID)
        {
            Moderator mod = dal.GetModerator(moderatorID, subforumID);
            return mod.TermExp;
        }

        public List<Forum> getAllForums()
        {
            List<Forum> forumsList = new List<Forum>();
            Forum[] forums = dal.GetForums(null);
            foreach(Forum forum in forums)
            {
                forumsList.Add(forum);
            }
            return forumsList;
        }

        public List<Admin> getAdmins(int userID, int forumID)
        {
            Forum forum = dal.GetForum(forumID);
            List<Admin> adminsList = new List<Admin>();
            Admin[] admins = dal.GetAdmins(null, forum); 
            foreach (Admin admin in admins)
            {
                adminsList.Add(admin);
            }
            return adminsList;
        }

        public List<User> getMembers(int userID, int forumID)
        {
            Forum forum = dal.GetForum(forumID);
            List<User> membersList = new List<User>();
            User[] members = dal.GetUseres(null, forum);
            foreach (User member in members)
            {
                membersList.Add(member);
            }
            return membersList;
        }

        public List<Subforum> getSubforums(int forumID)
        {
            List<Subforum> sfList = new List<Subforum>();
            Forum forum = dal.GetForum(forumID);
            Subforum[] sfArr = forum.GetAllSubForums();
            foreach(Subforum sf in sfArr)
            {
                sfList.Add(sf);
            }
            return sfList;
        }

        public Admin getAdmin(int userID, int forumID, int AdminID)
        {
            // TODO : amitay wants to check with policy if user is a member, if so, show admins otherwise don't show admins.
            Admin admin = dal.GetAdmin(AdminID, forumID);
            return admin;
        }
    }
}
