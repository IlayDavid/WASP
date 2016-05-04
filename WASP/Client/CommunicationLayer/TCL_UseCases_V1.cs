using Client.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.CommunicationLayer
{
    public partial class TCL : ICL
    {
        
        //---------------------------Version 1 Use Cases Start------------------------------------
        public SuperUser initialize(string name, string userName, int ID, string email, string pass)
        {
            _su = new SuperUser(name, userName, ID, email, pass);
            _isInitialize = true;
            return _su;
        }
        public int isInitialize()
        {
            return _isInitialize ? 1 : 0;
        }

        public Forum createForum(int userID, string forumName, string description, int adminId, string adminUserName, string adminName, string email, string pass, Policy policy)
        {
            if (userID != _su.id)
                throw new Exception("User with id - "+userID+" cannot add forum");
            User admin = new User(adminId, adminName, adminUserName, email, pass);
            Forum forum = new Forum(forumName, description, admin);
            forums.Add(forum.id, forum);
            forum.members.Add(_su.id, (User) _su );
            return forum;
        }

        public int defineForumPolicy(int userID, int forumID)
        {
            throw new NotImplementedException();
        }  //------------------------ policy object??

        public User subscribeToForum(int id, string userName, string name, string email, string pass, int targetForumID)
        {
            User newUser = new User(id, name, userName, email, pass);
            forums[targetForumID].members.Add(newUser.id, newUser);
            return newUser;
        }

        public Post createThread(int userID, int forumID, string title, string content, int subForumID)
        {
            Post newPost = new Post(title, content, forums[forumID].members[userID], subForumID, null);
            forums[forumID].subforums[subForumID].threads.Add(newPost);
            posts.Add(newPost.id, newPost);
           
            return newPost;
        }

        public Post createReplyPost(int userID, int forumID, string content, int replyToPost_ID)
        {
            Post inReply = posts[replyToPost_ID];
            Post newPost = new Post("", content, forums[forumID].members[userID], inReply.containerID, inReply);
            inReply.replies.Add(newPost);

            posts.Add(newPost.id, newPost);
            return newPost;
        }

        public Subforum createSubForum(int userID, int forumID, string name, string description, int moderatorID, DateTime term)
        {
            User user = forums[forumID].members[moderatorID];
            User admin = null;
            try
            {
                admin = forums[forumID].admins[userID];
            }
            catch(Exception)
            {
                if (_su.id == userID)
                    admin = _su;
            }


            Moderator m = new Moderator(user, term, admin);

            Subforum newSf = new Subforum(name, description, m, term);
            subforums.Add(newSf.id, newSf);
            forums[forumID].subforums.Add(newSf.id, newSf);
            return newSf;
        }
        //---------------------------Version 1 Use Cases End------------------------------------
    }
}