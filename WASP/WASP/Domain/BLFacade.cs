using System;
using System.Collections.Generic;

using WASP.DataClasses;
using WASP.DataClasses.Reports;
using WASP.Exceptions;


namespace WASP.Domain
{
    public class BLFacade : IBL
    {
        private static DAL2 dal = WASP.Config.Settings.GetDal();
        private bool initialized;
        public BLFacade()
        {
            this.initialized = false;
        }

        public void Clean()
        {
            dal.Clean();
        }

        public void Restore()
        {
            dal.Restore();
        }

        public void Backup()
        {
            dal.Backup();
        }

        public SuperUser initialize(string name, string userName, int ID, string email, string pass)
        {
            if (initialized)
            {
               // throw new InitializeException("System already initialized!");
            }
            SuperUser user = new SuperUser(ID, userName, pass);
            this.initialized = true;
            return user.Create();
        }

        public int isInitialize()
        {
            if (!initialized)
                return 0;
            return 1;
        }

        public Forum createForum(int userID, string forumName, string description, int adminID, string adminUserName, string adminName, string email, string pass, Policy policy)
        {
            SuperUser su = SuperUser.Get(userID);

            policy.Create();
            // create new forum with admin in it, create user for admin
            Forum newForum = new Forum(-1, forumName, description, policy);
            newForum = newForum.Create();

            User user = new User(adminID, adminName, adminUserName, email, pass, newForum);
            // TODO: need to check if user and forum are fine with policy
            Admin admin = new Admin(user, newForum);
            try
            {
                newForum.AddMember(user);
                newForum.AddAdmin(admin);
            }
            catch (WaspException e)
            {
                newForum.Delete();
                return null;
            }
            admin.User = user.Create();
            admin.Create();

            return Forum.Get(newForum.Id);
        }

        public int defineForumPolicy(int userID, int forumID, string deletePost, TimeSpan passwordPeriod, bool emailVerification, TimeSpan minimumSeniority, int usersLoad, string[] questions, bool notifyOffline, bool superUser = false)
        {
            //TODO
            Forum forum = Forum.Get(forumID); ;
            if (superUser)
            {
                SuperUser.Get(userID);
            }
            else
            {
                Admin admin = Admin.Get(userID, forumID);
            }
            Policy.PostDeletePolicy dp = (Policy.PostDeletePolicy)Enum.Parse(typeof(Policy.PostDeletePolicy), deletePost);
            Policy policy = new Policy(-1, dp, passwordPeriod, emailVerification, minimumSeniority, usersLoad, questions, notifyOffline);
                
            policy.Update();
            return 1;
        }

        public User subscribeToForum(int id, string userName, string name, string email, string pass, int targetForumID)
        {
            // Should throw exception if forum not found.
            Forum forum = Forum.Get(targetForumID);

            // Attempt to add user.
            User user = new User(id, name, userName, email, pass, forum);
            forum.AddMember(user);
            user.Create();

            return user;
        }

        public Post createThread(int userID, int forumID, string title, string content, int subForumID)
        {
            // Should throw exception if user no found.
            User author = User.Get(userID, forumID);
            // Should throw exception if subforum no found.
            Subforum sf = Subforum.Get(subForumID);

            Post originalPost = new Post(-1, title, content, author, DateTime.Now, null, sf, DateTime.Now);
            // Should throw exception if post doesn't follow forum policy.
            sf.AddThread(originalPost);
            author.AddPost(originalPost);

            return originalPost.Create();
        }


        public Post createReplyPost(int userID, int forumID, string content, int replyToPost_ID)
        {
            User author = User.Get(userID, forumID);
            Post post = Post.Get(replyToPost_ID);
            Post reply = new Post(-1,null, content, author, DateTime.Now, post, post.Subforum, DateTime.Now);

            post.AddReply(reply);
            author.AddPost(reply);

            return reply.Create();
        }

        public Subforum createSubForum(int userID, int forumID, string name, string description, int moderatorID, DateTime term)
        {
            Admin admin = Admin.Get(userID, forumID);
            Forum forum = Forum.Get(forumID);
            User user = User.Get(moderatorID, forumID);

            Subforum sf = new Subforum(-1, name, description, forum);
            Moderator mod = new Moderator(user, term, sf, admin);
            sf.AddModerator(mod);
            forum.AddSubForum(sf);
            sf = sf.Create();
            mod.Create();
            return sf;
        }

        public int sendMessage(int userID, int forumID, int targetUserNameID, string message)
        {
            User source = User.Get(userID, forumID);
            User target = User.Get(targetUserNameID, forumID);
            Notification newMessage = new Notification(-1, message, true, source, target, Notification.Types.Message);
            target.NewNotification(newMessage);

            return 1;
        }

        public Moderator addModerator(int userID, int forumID, int moderatorID, int subForumID, DateTime term)
        {
            Admin admin = Admin.Get(userID, forumID);
            Forum forum = Forum.Get(forumID);
            Subforum sf = Subforum.Get(subForumID);
            User user = User.Get(moderatorID, forumID);

            Moderator mod = new Moderator(user, term, sf, admin);
            sf.AddModerator(mod);

            return mod.Create();
        }

        public int updateModeratorTerm(int userID, int forumID, int moderatorID, int subforumID, DateTime term)
        {
            Moderator mod = Moderator.Get(moderatorID, subforumID);
            Admin admin = Admin.Get(userID, forumID);
            if (mod.Appointer.Id != admin.Id)
                throw new UnauthorizedEditModTerm(userID, moderatorID, subforumID);

            mod.TermExp = term;
            mod.Update();

            return 1;
        }

        public int confirmEmail(int userID, int forumID)
        {
            // TODO: 
            throw new NotImplementedException();
        }

        public int deletePost(int userID, int forumID, int postID)
        {
            Post post = Post.Get(postID);

            post.Delete();

            return 1;
        }

        public int editPost(int userID, int forumID, int postID, string content)
        {
            User user = User.Get(userID, forumID);
            Post post = Post.Get(postID);
            Forum forum = Forum.Get(forumID);
            if (user.Id != post.GetAuthor.Id && !forum.IsAdmin(userID))
                throw new UnauthorizedEditPost(userID, postID, post.Subforum.Id);
            post.Content = content;
            post.Update();

            return 1;
        }

        public int deleteModerator(int userID, int forumID, int moderatorID, int subForumID)
        {
            Admin admin = Admin.Get(userID, forumID);
            Moderator mod = Moderator.Get(moderatorID, subForumID);
            if (mod.Appointer.Id != admin.Id)
                throw new UnauthorizedDeleteModerator(userID, moderatorID);
            mod.Delete();
            return 1;
        }

        public Admin addAdmin(int userID, int forumID, int adminId)
        {
            SuperUser su = null;
            Admin admin = null;
            try
            {
                su = SuperUser.Get(userID);
            }
            catch (WaspException e)
            {
                admin = Admin.Get(userID, forumID);
            }
            Forum forum = Forum.Get(forumID);

            User user = User.Get(adminId, forumID);
            Admin toBeAdmin = new Admin(user, forum);

            return toBeAdmin.Create();
        }

        public int subForumTotalMessages(int userID, int forumID, int subForumID, bool superUser = false)
        {
            if (superUser)
            {
                SuperUser.Get(userID);
            }
            else
            {
                Admin admin = Admin.Get(userID, forumID);
            }
                
            Subforum sf = Subforum.Get(subForumID);
            int counter = 0;


            Post[] threads = sf.GetThreads();
            foreach (Post post in threads)
            {
                if (post.GetAuthor.Id == userID)
                    counter += 1 + post.NumOfReplies();
            }
            return counter;
        }

        public Post[] postsByMember(int adminID, int forumID, int userID, bool superUser = false)
        {
            if (superUser)
            {
                SuperUser.Get(adminID);
            }
            else
            {
                Admin.Get(adminID, forumID);
            }
            
            User user = User.Get(userID, forumID);

            return user.GetAllPosts();
        }

        public ModeratorReport moderatorReport(int userID, int forumID, bool superUser = false)
        {
            if(!superUser)
                Admin.Get(userID, forumID);
            else
                SuperUser.Get(userID);
            Forum forum = Forum.Get(forumID);
            List<Moderator> mods = new List<Moderator>();
            foreach (Subforum sf in forum.GetAllSubForums())
            {
                foreach (Moderator mod in sf.GetAllModerators())
                {
                    mods.Add(mod);
                }
            }
            return new ModeratorReport(forum, mods.ToArray());
        }

        public int totalForums(int userID)
        {
            SuperUser su = SuperUser.Get(userID);
            return Forum.Get(null).Length;
        }

        public User[] membersInDifferentForums(int userID)
        {
            SuperUser super = dal.GetSuperUser(userID);
            User[] users = dal.GetUsersInDiffForums();
            return users;
        }

        public User login(string userName, string password, int forumID)
        {
            Forum forum = Forum.Get(forumID);
            foreach (User user in forum.GetMembers())
            {
                if (user.Username.Equals(userName) && user.Password.Equals(password))
                {
                    ///user.OnlineCount++;
                    return user;
                }

            }

            throw new LoginException("Username or passwords are wrong.");
        }

        public SuperUser loginSU(string userName, string password)
        {
            foreach (SuperUser user in dal.GetSuperUsers(null))
            {
                if (user.Username.Equals(userName) && user.Password.Equals(password))
                {
                    //user.OnlineCount++;
                    return user;
                }
            }
            throw new LoginException("Username or passwords are wrong.");
        }

        public Post getThread(int forumID, int threadId)
        {
            Post post = Post.Get(threadId);
            return post;
        }

        public Post[] getThreads(int subForumID)
        {
            return Subforum.Get(subForumID).GetThreads();
        }

        public Forum getForum(int forumID)
        {
            return Forum.Get(forumID);
        }

        public Subforum getSubforum(int forumID, int subforumId)
        {
            return Subforum.Get(subforumId);
        }

        public Moderator[] getModerators(int forumID, int subForumID)
        {
            return Subforum.Get(subForumID).GetAllModerators();
        }

        public DateTime getModeratorTermTime(int userID, int forumID, int moderatorID, int subforumID)
        {
            Moderator mod = Moderator.Get(moderatorID, subforumID);
            return mod.TermExp;
        }

        public Forum[] getAllForums()
        {
            return Forum.Get(null);
        }

        public Admin[] getAdmins(int userID, int forumID)
        {
            return Admin.Get(null, forumID);
        }

        public User[] getMembers(int userID, int forumID)
        {
            Forum forum = Forum.Get(forumID);

            return forum.GetMembers();
        }

        public Subforum[] getSubforums(int forumID)
        {
            Forum forum = Forum.Get(forumID);
            return forum.GetAllSubForums();
        }

        public Admin getAdmin(int userID, int forumID, int AdminID)
        {
            Admin admin = Admin.Get(AdminID, forumID);
            return admin;
        }

        public Notification[] getAllNotificationses(int userID, int forumID)
        {
            return User.Get(userID, forumID).GetAllNotifications();
        }

        public Notification[] getNewNotificationses(int userID, int forumID)
        {
            Notification[] notifs = User.Get(userID, forumID).GetNewNotifications();
            foreach(Notification notif in notifs)
            {
                notif.IsNew = false;
                notif.Update();
            }
            return notifs;
        }

        public Post[] getReplys(int forumID, int subForumID, int postID)
        {
            Post post = Post.Get(postID);
            return post.GetAllReplies();
        }

        public User[] getFriends(int userID, int forumID)
        {
            return User.Get(userID, forumID).GetAllFriends();
        }

        public int addFriend(int userID, int forumID, int friendID)
        {
            User user = User.Get(userID, forumID);
            User friend = User.Get(friendID, forumID);

            user.AddFriend(friend);

            dal.AddFriend(user, friend);
            return 1;
        }
    }
}
