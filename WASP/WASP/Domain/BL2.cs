using System;
using System.Collections.Generic;
using WASP.DataClasses;


namespace WASP.Domain
{
    class BL2 : IBL

    {
        private DAL dal;


        /*
      * Pre-conditions: none.
      * Purpose: initialize the system and logs the superuser in
      * Return: super user details.
      */
        SuperUser initialize(string name, string userName, int ID, string email, string pass);

        /*
        * Purpose: check if the system is already initialize, should be called before initialize.
        * Return: 0 - if not initialize, 1 - otherwise.
        */
        int isInitialize();

        /*
         * Pre-conditions: super user is loged-in 
         * Purpose: create new forum which, with details of the admin.
         * Return: forum - on succsess, NULL - in fail.
         */

        /*
    * Pre-conditions: superuser is loged-in 
    * Purpose: set a policy for specific forum.
    * Return: 0 - on succsess, negative - in fail.        
    */
        int defineForumPolicy(int userID, int forumID);  //------------------------ policy object??


        public Forum createForum(int userID, string forumName, string description,int adminID, string adminUserName, string adminName, string email, string pass, Policy policy)
        {          
           // TODO: check if forum name and desc are fine with policy

            //create new forum with admin in it, create user for admin
            Forum newForum = new Forum(-1,forumName, description, policy, dal);
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



        Post createThread(int userID, int forumID, string title, string content, int subForumID)
        {
            User author = dal.GetUser(userID);
            Subforum sfContainer = dal.GetSubForum(subForumID,forumID);
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


       
        public  Subforum createSubForum(int userID, int forumID, string name, string description, int moderatorID, DateTime term)
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
        /*
       * Pre-conditions: Member is loged-in, second member is exists. 
       * Purpose: send a private message.
       */
        int sendMessage(int userID, int forumID, string targetUserNameID, string message);
        public int addModerator(int userID, int forumID, int moderatorID, int subForumID, DateTime term)
        {
            
            Forum forum = dal.GetForum(forumID);
            Subforum sf = dal.GetSubForum(subForumID);
            Admin admin = dal.GetAdmin(userID);
            if(forum != null && sf != null )
            {
                if (forum.IsAdmin(userID)&& forum.IsMember(moderatorID)) // checks if admin do that action, and if mod is mem of forum
                { 
                    Moderator mod = new Moderator(forum.GetMember(moderatorID),term,sf,admin,-1, dal);
                    sf.AddModerator(mod);
                    dal.CreateModerator(mod);
                    return 1;
                }
            }
            return -1;
        }


        /*
        * Pre-conditions: Member is loged-in, and is admin of the forum, moderator exist.
        * Purpose: Member updates moderator's term (new term=term)
        * Return: number > 0 if success
        */
        public int updateModeratorTerm(int userID, int forumID, int moderatorID, int subforumID, DateTime term)
        {
           
           Moderator mod = dal.GetModerator(moderatorID,subforumID);
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

        /*
      * Purpose: Confirms Member's email and adds him to the forum as an active member.
      * return: number>=0 if success
      */
        int confirmEmail(int userID, int forumID);

        /*
        * Pre-conditions: Member is loged-in, and own the post. (or manager, depend on policy)
        * Purpose: deletes the post
        * Return: number >= 0 id success
        */
        public int deletePost(int userID, int forumID, int postID)
        {
            
        }

        SuperUser IBL.initialize(string name, string userName, int ID, string email, string pass)
        {
            throw new NotImplementedException();
        }

        int IBL.isInitialize()
        {
            throw new NotImplementedException();
        }

        public Forum createForum(int userID, string forumName, string description, string adminUserName, string adminName, string email, string pass, Policy policy)
        {
            throw new NotImplementedException();
        }

        int IBL.defineForumPolicy(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        public User subscribeToForum(string userName, string name, string email, string pass, int targetForumID)
        {
            throw new NotImplementedException();
        }

        Post IBL.createThread(int userID, int forumID, string title, string content, int subForumID)
        {
            throw new NotImplementedException();
        }

        int IBL.sendMessage(int userID, int forumID, string targetUserNameID, string message)
        {
            throw new NotImplementedException();
        }

        int IBL.confirmEmail(int userID, int forumID)
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

        public List<Moderator> getModerators(int userID, int forumID, int subForumID)
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

        public List<Subforum> getSubforums(int userID, int forumID)
        {
            throw new NotImplementedException();
        }

        public Admin getAdmin(User user, int forumID, int userID)
        {
            throw new NotImplementedException();
        }
    }








}
}
