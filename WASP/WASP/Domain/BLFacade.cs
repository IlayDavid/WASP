using System;
using System.Collections.Generic;
using WASP.DataClasses;


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
            {
                // TODO: check if forum name and desc are fine with policy

                //create new forum with admin in it, create user for admin
                Forum newForum = new Forum(-1, forumName, description, null, dal);
                User user = new User(-1, adminName, adminUserName, email, pass, newForum);
                // TODO: need to check if user and forum are fine with policy
                Admin admin = new Admin(user, newForum, dal);
                newForum.AddAdmin(admin);
                newForum.AddMember(user);

                //TODO: add to DB at addAdmin,addMember and constructors ?
                dal.CreateForum(newForum);
                dal.CreateUser(user);
                dal.CreateAdmin(admin);

                return newForum;
            }
        }

        public int defineForumPolicy(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        public User subscribeToForum(int id, string userName, string name, string email, string pass, int targetForumID)
        {
            //TODO: check if user info fine with policy
            Forum forum = dal.GetForum(targetForumID);
            if (forum != null)
            {
                //TODO : check user info with policy.
                User user = new User(id, name, userName, email, pass, forum);
                dal.CreateUser(user);
                forum.AddMember(user);
                return user;
            }
            return null;
            //TODO: if forum is null, maybe need to throw exception ?
        }

        public Post createThread(int userID, int forumID, string title, string content, int subForumID)
        {
            User author = dal.GetUser(userID, forumID);
            Subforum sfContainer = dal.GetSubForum(subForumID);
            if (author != null && sfContainer != null)
            {
                // TODO: check post info with policy (title and content)
                Post original = new Post(title, content, -1, author, DateTime.Now, null, sfContainer, DateTime.Now, dal);
                return original;
            }
            return null;
        }


        public Post createReplyPost(int userID, int forumID, string content, int replyToPost_ID)
        {
            User author = dal.GetUser(userID, forumID);
            if (author != null)
            {
                Post post = dal.GetPost(replyToPost_ID);
                if (post != null)
                {
                    Post reply = new Post(null, content, -1, author, DateTime.Now, post, post.Container, DateTime.Now, dal);
                    return dal.CreatePost(reply);
                }
            }
            return null;
        }

        public Subforum createSubForum(int userID, int forumID, string name, string description, int moderatorID, DateTime term)
        {
            Forum forum = dal.GetForum(forumID);
            if (forum != null)
            {
                //TODO: check name and description with policy 
                Subforum sf = new Subforum(-1, name, description, dal, forum);
                return dal.CreateSubForum(sf);

            }
            return null;
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
            if (forum != null && sf != null)
            {
                if (forum.IsAdmin(userID) && forum.IsMember(moderatorID)) // checks if admin do that action, and if mod is mem of forum
                {
                    Moderator mod = new Moderator(forum.GetMember(moderatorID), term, sf, admin, dal);
                    sf.AddModerator(mod);
                    dal.CreateModerator(mod);
                    return mod;
                }
            }
            return null;
        }

        public int updateModeratorTerm(int userID, int forumID, int moderatorID, int subforumID, DateTime term)
        {
            Moderator mod = dal.GetModerator(moderatorID, subforumID);
            Admin admin = dal.GetAdmin(userID, forumID);
            if (mod != null && mod.Appointer.Id == admin.Id)
            {
                mod.TermExp = term;
                dal.updateModerator(mod);
                return 1;
            }
            return -1;
            //TODO: take care of exceptions ? 
        }

        public int confirmEmail(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        public int deletePost(int userID, int forumID, int postID)
        {
            throw new NotImplementedException();
        }

        public int editPost(int userID, int forumID, int postID, string content)
        {
            throw new NotImplementedException();
        }

        public int deleteModerator(int userID, int forumID, int moderatorID, int subForumID)
        {
            throw new NotImplementedException();
        }

        public int subForumTotalMessages(int userID, int forumID, int subForumID)
        {
            throw new NotImplementedException();
        }

        public int memberTotalMessages(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        public ModeratorReport moderatorReport(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        public int totalForums(int userID)
        {
            throw new NotImplementedException();
        }

        public List<User> membersInDifferentForums(int userID)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public List<Post> getThreads(int forumID, int subForumID, int from, int amount)
        {
            throw new NotImplementedException();
        }

        public Forum getForum(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        public Forum getForum(int forumID)
        {
            throw new NotImplementedException();
        }

        public Subforum getSubforum(int userID, int forumID, int subforumId)
        {
            throw new NotImplementedException();
        }

        public Subforum getSubforum(int forumID, int subforumId)
        {
            throw new NotImplementedException();
        }

        public List<Moderator> getModerators(int forumID, int subForumID)
        {
            throw new NotImplementedException();
        }

        public DateTime getModeratorTermTime(int userID, int forumID, int moderatorID, int subforumID)
        {
            throw new NotImplementedException();
        }

        public List<Forum> getAllForums()
        {
            throw new NotImplementedException();
        }

        public List<Admin> getAdmins(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        public List<User> getMembers(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        public List<Subforum> getSubforums(int forumID)
        {
            throw new NotImplementedException();
        }

        public Admin getAdmin(int userID, int forumID, int AdminID)
        {
            throw new NotImplementedException();
        }
    }
}
