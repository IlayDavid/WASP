﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WASP.DataClasses.DAL_EXCEPTIONS;

namespace WASP.DataClasses
{
    class DALSQL : DAL
    {
        private int forum_counter = 1;
        private Object forumLock = new Object();

        private int post_counter = 1;
        private Object postLock = new Object();

        private int subforum_counter = 1;
        private Object subForumLock = new Object();

        private Forum_SystemDataContext db = new Forum_SystemDataContext();

        public void Clean()
        {
            //db.ExecuteCommand("DELETE FROM Entity");
            db.INotifications.DeleteAllOnSubmit(db.INotifications);
            db.IPosts.DeleteAllOnSubmit(db.IPosts);
            db.ISubForums.DeleteAllOnSubmit(db.ISubForums);
            db.IForums.DeleteAllOnSubmit(db.IForums);
            db.IModerators.DeleteAllOnSubmit(db.IModerators);
            db.IAdmins.DeleteAllOnSubmit(db.IAdmins);
            db.IUsers.DeleteAllOnSubmit(db.IUsers);
            
            
            

        }

        private int getNextPostId()
        {
            lock (postLock)
            {
                int ret = post_counter;
                post_counter++;
                return ret;
            }
        }
        private int getNextSubForumId()
        {
            lock (subForumLock)
            {
                int ret = subforum_counter;
                subforum_counter++;
                return ret;
            }
        }
        private int getNextForumId()
        {
            lock (forumLock)
            {
                int ret = forum_counter;
                forum_counter++;
                return ret;
            }
        }


        // dont need forum..
        public User CreateUser(User user)
        {
            IUser old_user = db.IUsers.FirstOrDefault(x => (x.id == user.Id && x.forumId == user.forum.Id));
            if (old_user != null) throw new ExistException(String.Format("User {0} Forum {0} exists in the DataBase", user.Id, user.forum.Id));
            IUser _user = new IUser();
            _user.id = user.Id;
            _user.userName = user.Username;
            _user.name = user.Name;
            _user.password = user.Password;
            _user.email = user.Email;
            _user.forumId = user.forum.Id;

            db.IUsers.InsertOnSubmit(_user);
            db.SubmitChanges();
            return user;
        }

        public Admin CreateAdmin(Admin admin)
        {
            IUser old_user = old_user = db.IUsers.FirstOrDefault(x => (x.id == admin.Id && x.forumId == admin.Forum.Id));
            if (old_user != null) throw new ExistException(string.Format("User {0}, Forum {0} exists in the DataBase", admin.Id, admin.Forum.Id));

            CreateUser(admin.InnerUser);

            IAdmin _admin = new IAdmin();
            _admin.userId = admin.InnerUser.Id;
            _admin.forumId = admin.Forum.Id;

            db.IAdmins.InsertOnSubmit(_admin);
            db.SubmitChanges();
            return admin;
        }

        public Moderator CreateModerator(Moderator mod)
        {
            IUser old_user = db.IUsers.FirstOrDefault(x => x.id == mod.Id && x.forumId == mod.SubForum.Forum.Id);
            if (old_user != null)
                throw new ExistException(string.Format("User {0}, Forum {0} exists in the DataBase", mod.Id, mod.SubForum.Forum.Id));

            if (mod.Appointer.Forum.Id != mod.user.forum.Id || mod.Appointer.Forum.Id != mod.SubForum.Forum.Id ||
                mod.user.forum.Id != mod.SubForum.Forum.Id)
                throw new InvalidException("Method: CreateModerator, doesnt match 'forum' of Appointer|Subforum|User");

            CreateUser(mod.user);

            IModerator _mod = new IModerator();
            _mod.userId = mod.user.Id;
            _mod.byAdmin = mod.Appointer.InnerUser.Id;
            _mod.forumId = mod.Appointer.Forum.Id;
            _mod.subForumId = mod.SubForum.Id;
            _mod.term = mod.TermExp;

            db.IModerators.InsertOnSubmit(_mod);
            db.SubmitChanges();
            return mod;
        }
        public Forum CreateForum(Forum forum)
        {
            IForum _forum = new IForum();
            _forum.id = getNextForumId();
            _forum.subject = forum.Name;
            _forum.description = forum.Description;

            db.IForums.InsertOnSubmit(_forum);
            db.SubmitChanges();
            forum.Id = _forum.id;
            return forum;
        }
        public Subforum CreateSubForum(Subforum sf)
        {
            ISubForum _subf = new ISubForum();
            _subf.id = getNextSubForumId();
            _subf.subject = sf.Name;
            _subf.description = sf.Description;
            _subf.forumId = sf.Forum.Id;

            db.ISubForums.InsertOnSubmit(_subf);
            db.SubmitChanges();
            sf.Id = _subf.id;
            return sf;
        }
        public Post CreatePost(Post post)
        {
            IPost ipost = new IPost();

            ipost.id = getNextPostId();
            ipost.subforumId = post.Container.Id;
            ipost.title = post.Title;
            ipost.cnt = post.Content;
            ipost.publishAt = post.PublishedAt;
            ipost.editAt = post.EditAt;
            if (post.InReplyTo == null)
                ipost.reply = null;
            else ipost.reply = post.InReplyTo.Id;

            ipost.userId = post.GetAuthor.Id;
            ipost.forumId = post.GetAuthor.forum.Id;


            db.IPosts.InsertOnSubmit(ipost);
            db.SubmitChanges();
            post.Id = ipost.id;
            return post;
        }

        public User[] GetUseres(Collection<int> userIds, Forum forum)
        {
            List<User> users = new List<User>();
            foreach (IUser iuser in db.IUsers)
                if ( (forum == null || iuser.forumId == forum.Id) && (userIds == null || userIds.Contains(iuser.id)))
                    users.Add(GetUser(iuser.id, iuser.forumId));
            return users.ToArray();
        }
        public Moderator[] GetModerators(Collection<int> moderatorIds, Subforum subforum)
        {
            List<Moderator> moderators = new List<Moderator>();
            foreach (IModerator imoderator in db.IModerators)
                if ( ( subforum == null || imoderator.ISubForum.id == subforum.Id) &&
                            (moderatorIds == null || moderatorIds.Contains(imoderator.userId)))
                    moderators.Add(GetModerator(imoderator.userId, imoderator.subForumId));

            return moderators.ToArray();
        }
        public Admin[] GetAdmins(Collection<int> adminsIds, Forum forum)
        {
            List<Admin> admins = new List<Admin>();
            foreach (IAdmin iadmin in db.IAdmins)
                if ( (forum == null || iadmin.forumId == forum.Id) &&
                    (adminsIds == null || adminsIds.Contains(iadmin.userId)))
                    admins.Add(GetAdmin(iadmin.userId, iadmin.forumId));

            return admins.ToArray();
        }
        public Forum[] GetForums(Collection<int> forumsIds)
        {
            List<Forum> forums = new List<Forum>();
            foreach (IForum iforum in db.IForums)
                if (forumsIds == null || forumsIds.Contains(iforum.id))
                    forums.Add(GetForum(iforum.id));
            return forums.ToArray();
        }
        public Subforum[] GetSubForums(Collection<int> subForumIds)
        {
            List<Subforum> subforums = new List<Subforum>();

            foreach (ISubForum isf in db.ISubForums)
                if (subForumIds == null || subForumIds.Contains(isf.id))
                    subforums.Add(GetSubForum(isf.id));
            return subforums.ToArray();
        }
        public Post[] GetPosts(Collection<int> Posts)
        {
            List<Post> posts = new List<Post>();
            foreach (IPost ipost in db.IPosts)
                if (Posts == null || Posts.Contains(ipost.id))
                    posts.Add(GetPost(ipost.id));
            return posts.ToArray();
        }


        public Admin UpdateAdmin(Admin admin)
        {
            IAdmin iadmin = db.IAdmins.FirstOrDefault(x => (x.userId == admin.Id && x.forumId == admin.Forum.Id));
            if (iadmin != null)
            {
                iadmin.forumId = admin.Forum.Id;
                iadmin.userId = admin.InnerUser.Id;
                updateUser(admin.InnerUser);
                db.SubmitChanges();
                return admin;
            }
            throw new UpdateException(String.Format("Admin {0} Forum {0} wasn't found", admin.Id, admin.Forum.Id));
        }
        public Forum UpdateForum(Forum forum)
        {
            IForum iforum = db.IForums.FirstOrDefault(x => (x.id == forum.Id));

            if (iforum != null)
            {
                iforum.description = forum.Description;
                iforum.subject = forum.Name;
                db.SubmitChanges();
                return forum;
            }
            else throw new UpdateException(String.Format("Forum {0} wasn't found", forum.Id));
        }
        public Moderator updateModerator(Moderator mod)
        {
            IModerator imod = db.IModerators.FirstOrDefault(x => (x.userId == mod.user.Id && x.subForumId == mod.SubForum.Id));
            if (imod != null)
            {
                if (mod.Appointer.Forum.Id != mod.user.forum.Id || mod.Appointer.Forum.Id != mod.SubForum.Forum.Id ||
                                mod.user.forum.Id != mod.SubForum.Forum.Id)
                    throw new Exception("Method: UpdateModerator, doesnt match 'forum' of Appointer|Subforum|User");

                imod.subForumId = mod.SubForum.Id;
                imod.term = mod.TermExp;
                imod.byAdmin = mod.Appointer.InnerUser.Id;
                imod.userId = mod.user.Id;
                imod.forumId = mod.user.forum.Id;

                updateUser(mod.user);
                db.SubmitChanges();
                return mod;
            }
            throw new UpdateException(String.Format("Moderator {0} Forum {0} wasn't found", mod.Id, mod.SubForum.Forum.Id));
        }

        /// <summary>
        /// no moderators update
        /// </summary>
        /// <param name="sf"></param>
        /// <returns></returns>
        public Subforum UpdateSubForum(Subforum sf)
        {
            ISubForum isf = db.ISubForums.FirstOrDefault(x => (x.id == sf.Id));
            if (isf != null)
            {
                isf.subject = sf.Name;
                isf.description = sf.Description;
                isf.forumId = sf.Forum.Id;
                db.SubmitChanges();
                return sf;
            }
            throw new UpdateException(String.Format("sub-forum {0} wasn't found", sf.Id));
        }

        /// <summary>
        /// no notification update
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User updateUser(User user)
        {
            IUser iuser = db.IUsers.FirstOrDefault(x => (x.id == user.Id && x.forumId == user.forum.Id));
            if (iuser != null)
            {
                iuser.userName = user.Username;
                iuser.name = user.Name;
                iuser.email = user.Email;
                iuser.password = user.Password;

                db.SubmitChanges();
                return user;
            }
            throw new UpdateException(String.Format("user {0} wasn't found", user.Id));
        }
        public Post UpdatePost(Post post)
        {
            IPost ipost = db.IPosts.FirstOrDefault(x => x.id == post.Id);
            if (ipost != null)
            {
                ipost.subforumId = post.Container.Id;
                ipost.title = post.Title;
                ipost.cnt = post.Content;
                ipost.publishAt = post.PublishedAt;
                ipost.editAt = post.EditAt;
                if (post.InReplyTo == null)
                    ipost.reply = null;
                else ipost.reply = post.InReplyTo.Id;
                ipost.userId = post.GetAuthor.Id;
                ipost.forumId = post.GetAuthor.forum.Id;

                db.SubmitChanges();
                return post;
            }
            throw new UpdateException(String.Format("post {0} wasn't found", post.Id));
        }



        public Forum GetForum(int id)
        {
            IForum iforum = db.IForums.FirstOrDefault(x => (x.id == id));
            if (iforum != null)
            {
                Forum forum = new Forum(iforum.id, iforum.subject, iforum.description, null, this);
                return forum;
            }
            throw new GetException(string.Format("forum {0} wasn't found", id));
        }
        public Subforum GetSubForum(int sfId)
        {
            ISubForum isf = db.ISubForums.FirstOrDefault(x => (x.id == sfId));
            if (isf != null)
            {
                Subforum sf = new Subforum(isf.id, isf.subject, isf.description, GetForum(isf.forumId), this);
                return sf;
            }
            throw new GetException(string.Format("sub-forum {0} wasn't found", sfId));
        }
        public User GetUser(int id, int forumId)
        {
            IUser iuser = db.IUsers.FirstOrDefault(x => (x.id == id && x.forumId == forumId));
            if (iuser != null)
            {
                Forum forum = GetForum(forumId);
                User user = new User(iuser.id, iuser.name, iuser.userName, iuser.email, iuser.password, forum);
                return user;
            }
            throw new GetException(string.Format("user {0} wasn't found", id));
        }
        public Moderator GetModerator(int id, int sfId)
        {
            IModerator imoderator = db.IModerators.FirstOrDefault(x => (x.userId == id && x.subForumId == sfId));
            if (imoderator != null)
            {
                Subforum subforum = GetSubForum(sfId);
                Admin admin = GetAdmin(imoderator.byAdmin, imoderator.forumId);
                User user = GetUser(id, imoderator.forumId);

                Moderator moderator = new Moderator(user, imoderator.term, subforum, admin, this);
                return moderator;
            }
            throw new GetException(string.Format("moderator {0} wasn't found", id));
        }
        public Admin GetAdmin(int adminId, int forumId)
        {
            IAdmin iadmin = db.IAdmins.FirstOrDefault(x => (x.userId == adminId && x.forumId == forumId));
            if (iadmin != null)
            {
                Forum forum = GetForum(forumId);
                User user = GetUser(adminId, forumId);
                Admin admin = new Admin(user, forum, this);
                return admin;
            }
            throw new GetException(string.Format("admin {0} wasn't found", adminId));
        }
        public Post GetPost(int postId)
        {
            IPost ipost = db.IPosts.FirstOrDefault(x => x.id == postId);
            if (ipost != null)
            {
                Post replyTo = null;
                if (ipost.reply != null) replyTo = GetPost((int)ipost.reply);
                Post post = new Post(ipost.id, ipost.title, ipost.cnt,  GetUser(ipost.userId, ipost.IUser.forumId), ipost.publishAt,
                   replyTo, GetSubForum(ipost.subforumId), ipost.editAt, this);
                return post;
            }
            throw new GetException(string.Format("post {0} wasn't found", postId));
        }


        public bool DeletePost(int postId)
        {
            IPost ipost = db.IPosts.FirstOrDefault(x => x.id == postId);
            if (ipost != null)
            {
                db.IPosts.DeleteOnSubmit(ipost);
                db.SubmitChanges();
                return true;
            }
            else throw new ExistException(string.Format("Post {0} does not exist", postId));
        }
        // delete moderator, delete forum, delete subforum

           
        public bool DeleteUser(int id, int forumId)
        {
            IUser iuser = db.IUsers.FirstOrDefault(x => x.id == id && x.forumId == forumId);
            if (iuser != null)
            {
                db.IUsers.DeleteOnSubmit(iuser);
                db.SubmitChanges();
                return true;
            }
            else throw new ExistException(string.Format("User {0} Forum {0} does not exist", id, forumId));
        }
        public bool DeleteAdmin(int adminId, int forumId)
        {
            IAdmin iadmin = db.IAdmins.FirstOrDefault(x => x.userId == adminId && x.forumId == forumId);
            if (iadmin != null)
            {
                int userId = iadmin.userId;
                db.IAdmins.DeleteOnSubmit(iadmin);
                db.SubmitChanges();
                DeleteUser(userId, forumId);

                return true;
            }
            else throw new ExistException(string.Format("Admin {0} Forum {0} does not exist", adminId, forumId));
        }
        public bool DeleteModerator(int modId, int subforumId)
        {
            IModerator imoderator = db.IModerators.FirstOrDefault(x => x.userId == modId && x.subForumId == subforumId);
            if (imoderator != null)
            {
                int userId = imoderator.userId;
                int forumId = imoderator.forumId;
                db.IModerators.DeleteOnSubmit(imoderator);
                db.SubmitChanges();
                DeleteUser(userId, forumId);
                return true;
            }
            else throw new ExistException(string.Format("Moderator {0} Forum {0} does not exist", modId, subforumId));
        }
        public bool DeleteForum(int forumId)
        {
            IForum iforum = db.IForums.FirstOrDefault(x => x.id == forumId);
            if (iforum != null)
            {
                db.IForums.DeleteOnSubmit(iforum);
                db.SubmitChanges();
                return true;
            }
            else throw new ExistException(string.Format("Forum {0} does not exist", forumId));
        }
        public bool DeleteSubforum(int subforumId)
        {
            ISubForum isubforum = db.ISubForums.FirstOrDefault(x => x.id == subforumId);
            if (isubforum != null)
            {
                db.ISubForums.DeleteOnSubmit(isubforum);
                db.SubmitChanges();
                return true;
            }
            else throw new ExistException(string.Format("subforum {0} does not exist", subforumId));
        }

        public Moderator[] GetModeratorsInSubForum(int subForumID)
        {
            List<Moderator> moderators = new List<Moderator>();
            foreach (IModerator imod in db.IModerators)
            {
                if (imod.subForumId == subForumID)
                    moderators.Add(GetModerator(imod.userId, imod.subForumId));
            }
            return moderators.ToArray();
        }
        public Forum [] GetForumsUserID(int userId)
        {
            List<Forum> forums = new List<Forum>();
            foreach (IUser iuser in db.IUsers)
            {
                if (iuser.id == userId)
                    forums.Add(GetForum(iuser.forumId));
            }
            return forums.ToArray();
        }
    }
}