using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Linq;
using WASP.DataClasses.DAL_EXCEPTIONS;
using WASP.DataClasses;
using System.IO;

namespace WASP.DataClasses
{
    class DALSQL : DAL2
    {
        private static string _connectionString;
        public static string SetDb(string dbName)
        {
            _connectionString =
                $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={Directory.GetParent(Directory.GetParent(
                        Directory.GetCurrentDirectory()).FullName)}\{dbName}.mdf;Integrated Security=True;Connect Timeout=30";

            return _connectionString;
        }
        //private int forum_counter = 1;
        private Object forumLock = new Object();

        //private int post_counter = 1;
        private Object postLock = new Object();

        //private int subforum_counter = 1;
        private Object subForumLock = new Object();

        //private int notification_counter = 1;
        private Object notificationLock = new Object();

        //private int policy_counter = 1;
        private Object policyLock = new Object();

        private static List<ISuperUser> backUpSuperUsers;
        private static List<IUser> backUpUsers;
        private static List<IAdmin> backUpAdmins;
        private static List<IModerator> backUpModerators;
        private static List<IForum> backUpForums;
        private static List<ISubForum> backUpSubForums;
        private static List<INotification> backUpNotifications;
        private static List<IPost> backUpPosts;
        private static List<IPolicy> backUpPolicies;
        private static List<Index> backUpIndexes;
        public void Backup()
        {
            DALSQL.BackUpAll();
        }

        public void Restore()
        {
            DALSQL.GetBackUp();
        }

        public static void BackUpAll()
        {
            backUpIndexes = new List<Index>();
            backUpSuperUsers = new List<ISuperUser>();
            backUpUsers = new List<IUser>();
            backUpAdmins = new List<IAdmin>();
            backUpModerators = new List<IModerator>();
            backUpForums = new List<IForum>();
            backUpSubForums = new List<ISubForum>();
            backUpNotifications = new List<INotification>();
            backUpPosts = new List<IPost>();
            backUpPolicies = new List<IPolicy>();

            Forum_SystemDataContext db = new Forum_SystemDataContext(SetDb("Forums_System"));
            /*
            //1
            foreach (Index indx in db.Indexes)
            {
                Index index = new Index();
                index.post = indx.post;
                index.subforum = indx.subforum;
                index.forum = indx.forum;
                index.notification = indx.notification;
                index.policy = indx.policy;
        
                backUpIndexes.Add(indx);
            }
*/
            //1
            foreach (ISuperUser isuperUser in db.ISuperUsers)
            {
                ISuperUser isuper = new ISuperUser();
                isuper.id = isuperUser.id;
                isuper.userName = isuperUser.userName;
                isuper.password = isuperUser.password;
                backUpSuperUsers.Add(isuper);
            }

            //1.11
            foreach (IPolicy ipol in db.IPolicies)
            {
                IPolicy pol = new IPolicy();
                pol.id = ipol.id;
                pol.emailVerification = ipol.emailVerification;
                pol.minimumSeniority = ipol.minimumSeniority;
                pol.passwordPeriod = ipol.passwordPeriod;
                pol.postDeletePolicy = ipol.postDeletePolicy;
                pol.usersLoad = ipol.usersLoad;
                backUpPolicies.Add(pol);
            }

            //2
            foreach (IUser user in db.IUsers)
            {
                IUser iuser = new IUser();
                iuser.id = user.id;
                iuser.userName = user.userName;
                iuser.name = user.name;
                iuser.password = user.password;
                iuser.email = user.email;
                iuser.forumId = user.forumId;

                backUpUsers.Add(iuser);
            }
            //3
            foreach (IAdmin admin in db.IAdmins)
            {
                IAdmin iadmin = new IAdmin();
                iadmin.userId = admin.userId;
                iadmin.forumId = admin.forumId;
                backUpAdmins.Add(iadmin);
            }
            //4
            foreach (IModerator mod in db.IModerators)
            {
                IModerator imod = new IModerator();
                imod.userId = mod.userId;
                imod.subForumId = mod.subForumId;
                imod.forumId = mod.forumId;
                imod.byAdmin = mod.byAdmin;
                imod.term = mod.term;
                backUpModerators.Add(imod);
            }
            //5
            foreach (IForum forum in db.IForums)
            {
                IForum iforum = new IForum();
                iforum.id = forum.id;
                iforum.subject = forum.subject;
                iforum.description = forum.description;

                backUpForums.Add(iforum);
            }
            //6
            foreach (ISubForum subf in db.ISubForums)
            {
                ISubForum isubf = new ISubForum();
                isubf.id = subf.id;
                isubf.subject = subf.subject;
                isubf.description = subf.description;
                isubf.forumId = subf.forumId;

                backUpSubForums.Add(isubf);
            }
            //7
            foreach (IPost post in db.IPosts)
            {
                IPost ipost = new IPost();
                ipost.id = post.id;
                ipost.title = post.title;
                ipost.cnt = post.cnt;
                ipost.userId = post.userId;
                ipost.forumId = post.forumId;
                ipost.publishAt = post.publishAt;
                ipost.editAt = post.editAt;
                ipost.reply = post.reply;
                ipost.subforumId = post.subforumId;

                backUpPosts.Add(ipost);
            }
            //8
            foreach (INotification noti in db.INotifications)
            {
                INotification inoti = new INotification();
                inoti.id = noti.id;
                inoti.isNew = noti.isNew;
                inoti.message = noti.message;
                inoti.fromForumId = noti.fromForumId;
                inoti.fromUserId = noti.fromUserId;
                inoti.toForumId = noti.toForumId;
                inoti.toUserId = noti.toUserId;

                backUpNotifications.Add(inoti);
            }
        }

        public static void GetBackUp()
        {
            Forum_SystemDataContext db = new Forum_SystemDataContext(SetDb("Forums_System"));
            foreach (ISuperUser isuper in backUpSuperUsers)
            {
                db.ISuperUsers.InsertOnSubmit(isuper);
            }


            foreach (IPolicy ipol in backUpPolicies)
            {
                db.IPolicies.InsertOnSubmit(ipol);
            }

            //5
            foreach (IForum forum in backUpForums)
            {
                db.IForums.InsertOnSubmit(forum);
            }

            //6
            foreach (ISubForum subf in backUpSubForums)
            {
                db.ISubForums.InsertOnSubmit(subf);
            }

            //2
            foreach (IUser user in backUpUsers)
            {
                db.IUsers.InsertOnSubmit(user);
            }
            //3
            foreach (IAdmin admin in backUpAdmins)
            {
                db.IAdmins.InsertOnSubmit(admin);
            }
            //4
            foreach (IModerator mod in backUpModerators)
            {
                db.IModerators.InsertOnSubmit(mod);
            }


            //7
            foreach (IPost post in backUpPosts)
            {
                db.IPosts.InsertOnSubmit(post);
            }
            //8
            foreach (INotification noti in backUpNotifications)
            {
                db.INotifications.InsertOnSubmit(noti);
            }
            db.SubmitChanges();
        }

        private Forum_SystemDataContext db = new Forum_SystemDataContext(SetDb("Forums_System"));

        public void Clean()
        {
            db.INotifications.DeleteAllOnSubmit(db.INotifications);
            db.IPosts.DeleteAllOnSubmit(db.IPosts);
            db.ISubForums.DeleteAllOnSubmit(db.ISubForums);
            db.IForums.DeleteAllOnSubmit(db.IForums);
            db.IModerators.DeleteAllOnSubmit(db.IModerators);
            db.IAdmins.DeleteAllOnSubmit(db.IAdmins);
            db.IUsers.DeleteAllOnSubmit(db.IUsers);
            db.ISuperUsers.DeleteAllOnSubmit(db.ISuperUsers);
            db.IPolicies.DeleteAllOnSubmit(db.IPolicies);
            db.SubmitChanges();
        }

        private int getNextPostId()
        {

            lock (postLock)
            {
                Index indx = db.Indexes.First(x => true);
                int ret = indx.post;
                indx.post = indx.post + 1;
                db.SubmitChanges();
                return ret;
            }
        }
        private int getNextPolicyId()
        {

            lock (policyLock)
            {
                Index indx = db.Indexes.First(x => true);
                int ret = indx.policy;
                indx.policy = indx.policy + 1;
                db.SubmitChanges();
                return ret;
            }
        }
        private int getNextNotificationId()
        {
            lock (notificationLock)
            {
                Index indx = db.Indexes.First(x => true);
                int ret = indx.notification;
                indx.notification = indx.notification + 1;
                db.SubmitChanges();
                return ret;
            }
        }

        private int getNextSubForumId()
        {

            lock (subForumLock)
            {
                Index indx = db.Indexes.First(x => true);
                int ret = indx.subforum;
                indx.subforum = indx.subforum + 1;
                db.SubmitChanges();
                return ret;
            }
        }
        private int getNextForumId()
        {

            lock (forumLock)
            {
                Index indx = db.Indexes.First(x => true);
                int ret = indx.forum;
                indx.forum = indx.forum + 1;
                db.SubmitChanges();
                return ret;
            }
        }


        // dont need forum..
        public User CreateUser(User user)
        {
            IUser old_user = db.IUsers.FirstOrDefault(x => (x.id == user.Id && x.forumId == user.Forum.Id));
            if (old_user != null) throw new ExistException(String.Format("User {0} Forum {1} exists in the DataBase", user.Id, user.Forum.Id));
            IUser _user = new IUser();
            _user.id = user.Id;
            _user.userName = user.Username;
            _user.name = user.Name;
            _user.password = user.Password;
            _user.email = user.Email;
            _user.forumId = user.Forum.Id;
            _user.PasswordChangeDate = user.PasswordChangeDate;
            _user.StartDate = user.StartDate;

            db.IUsers.InsertOnSubmit(_user);
            db.SubmitChanges();
            return user;
        }

        public Admin CreateAdmin(Admin admin)
        {
            IAdmin old_admin = db.IAdmins.FirstOrDefault(x => (x.userId == admin.Id && x.forumId == admin.Forum.Id));
            if (old_admin != null) throw new ExistException(string.Format("User {0}, Forum {1} exists in the DataBase", admin.Id, admin.Forum.Id));

            IAdmin _admin = new IAdmin();
            _admin.userId = admin.User.Id;
            _admin.forumId = admin.Forum.Id;

            db.IAdmins.InsertOnSubmit(_admin);
            db.SubmitChanges();
            return admin;
        }
        public Moderator CreateModerator(Moderator mod)
        {
            IModerator old_mod = db.IModerators.FirstOrDefault(x => x.userId == mod.Id && x.forumId == mod.SubForum.Forum.Id);
            if (old_mod != null)
                throw new ExistException(string.Format("User {0}, Forum {1} exists in the DataBase", mod.Id, mod.SubForum.Forum.Id));

            if (mod.Appointer.Forum.Id != mod.User.Forum.Id || mod.Appointer.Forum.Id != mod.SubForum.Forum.Id ||
                mod.User.Forum.Id != mod.SubForum.Forum.Id)
                throw new InvalidException("Method: CreateModerator, doesnt match 'forum' of Appointer|Subforum|User");

            IModerator _mod = new IModerator();
            _mod.userId = mod.User.Id;
            _mod.byAdmin = mod.Appointer.User.Id;
            _mod.forumId = mod.Appointer.Forum.Id;
            _mod.subForumId = mod.SubForum.Id;
            _mod.term = mod.TermExp;
            _mod.startDate = mod.StartDate;

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
            if (forum.Policy != null)
                _forum.policyId = forum.Policy.Id;
            else
                _forum.policyId = null;
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
            ipost.subforumId = post.Subforum.Id;
            ipost.title = post.Title;
            ipost.cnt = post.Content;
            ipost.publishAt = post.PublishedAt;
            ipost.editAt = post.EditAt;
            if (post.InReplyTo == null)
                ipost.reply = null;
            else ipost.reply = post.InReplyTo.Id;

            ipost.userId = post.GetAuthor.Id;
            ipost.forumId = post.GetAuthor.Forum.Id;


            db.IPosts.InsertOnSubmit(ipost);
            db.SubmitChanges();
            post.Id = ipost.id;
            return post;
        }
        public User[] GetUsers(int[] userIds, int forumId)
        {
            List<User> users = new List<User>();
            foreach (IUser iuser in db.IUsers)
                if (iuser.forumId == forumId && (userIds == null || userIds.Contains(iuser.id)))
                    users.Add(GetUser(iuser.id, iuser.forumId));
            return users.ToArray();
        }
        public Moderator[] GetModerators(int[] moderatorIds, Subforum subforum)
        {
            List<Moderator> moderators = new List<Moderator>();
            foreach (IModerator imoderator in db.IModerators)
                if ((subforum == null || imoderator.ISubForum.id == subforum.Id) &&
                            (moderatorIds == null || moderatorIds.Contains(imoderator.userId)))
                    moderators.Add(GetModerator(imoderator.userId, imoderator.subForumId));

            return moderators.ToArray();
        }
        public Admin[] GetAdmins(int[] adminsIds, Forum forum)
        {
            List<Admin> admins = new List<Admin>();
            foreach (IAdmin iadmin in db.IAdmins)
                if ((forum == null || iadmin.forumId == forum.Id) &&
                    (adminsIds == null || adminsIds.Contains(iadmin.userId)))
                    admins.Add(GetAdmin(iadmin.userId, iadmin.forumId));

            return admins.ToArray();
        }
        public Forum[] GetForums(int[] forumsIds)
        {
            List<Forum> forums = new List<Forum>();
            foreach (IForum iforum in db.IForums)
                if (forumsIds == null || forumsIds.Contains(iforum.id))
                    forums.Add(GetForum(iforum.id));
            return forums.ToArray();
        }
        public Subforum[] GetSubForums(int[] subForumIds)
        {
            List<Subforum> subforums = new List<Subforum>();

            foreach (ISubForum isf in db.ISubForums)
                if (subForumIds == null || subForumIds.Contains(isf.id))
                    subforums.Add(GetSubForum(isf.id));
            return subforums.ToArray();
        }
        public Post[] GetPosts(int[] Posts)
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
                iadmin.userId = admin.User.Id;
                UpdateUser(admin.User);
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
                if (forum.Policy != null)
                    iforum.policyId = forum.Policy.Id;
                else
                    iforum.policyId = null;
                db.SubmitChanges();
                return forum;
            }
            else throw new UpdateException(String.Format("Forum {0} wasn't found", forum.Id));
        }
        public Moderator UpdateModerator(Moderator mod)
        {
            IModerator imod = db.IModerators.FirstOrDefault(x => (x.userId == mod.User.Id && x.subForumId == mod.SubForum.Id));
            if (imod != null)
            {
                if (mod.Appointer.Forum.Id != mod.User.Forum.Id || mod.Appointer.Forum.Id != mod.SubForum.Forum.Id ||
                                mod.User.Forum.Id != mod.SubForum.Forum.Id)
                    throw new Exception("Method: UpdateModerator, doesnt match 'forum' of Appointer|Subforum|User");

                imod.subForumId = mod.SubForum.Id;
                imod.term = mod.TermExp;
                imod.byAdmin = mod.Appointer.User.Id;
                imod.userId = mod.User.Id;
                imod.forumId = mod.User.Forum.Id;
                imod.startDate = mod.StartDate;

                UpdateUser(mod.User);
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
        public User UpdateUser(User user)
        {
            IUser iuser = db.IUsers.FirstOrDefault(x => (x.id == user.Id && x.forumId == user.Forum.Id));
            if (iuser != null)
            {
                iuser.userName = user.Username;
                iuser.name = user.Name;
                iuser.email = user.Email;
                iuser.password = user.Password;
                iuser.StartDate = user.StartDate;
                iuser.PasswordChangeDate = user.PasswordChangeDate;
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
                ipost.subforumId = post.Subforum.Id;
                ipost.title = post.Title;
                ipost.cnt = post.Content;
                ipost.publishAt = post.PublishedAt;
                ipost.editAt = post.EditAt;
                if (post.InReplyTo == null)
                    ipost.reply = null;
                else ipost.reply = post.InReplyTo.Id;
                ipost.userId = post.GetAuthor.Id;
                ipost.forumId = post.GetAuthor.Forum.Id;

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
                if (iforum.policyId != null)
                    forum.Policy = GetPolicy((int)iforum.policyId);

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
                User user = new User(iuser.id, iuser.name, iuser.userName, iuser.email, iuser.password, forum, iuser.StartDate, iuser.PasswordChangeDate);


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

                Moderator moderator = new Moderator(user, imoderator.term, subforum, admin, imoderator.startDate);
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
                Post post = new Post(ipost.id, ipost.title, ipost.cnt, GetUser(ipost.userId, ipost.IUser.forumId), ipost.publishAt,
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
                IPost reply = db.IPosts.FirstOrDefault(x => x.reply == postId);
                if (reply != null)
                {
                    DeletePost(reply.id);
                    return DeletePost(postId);
                }

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
            else throw new ExistException(string.Format("User {0} Forum {1} does not exist", id, forumId));
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
            else throw new ExistException(string.Format("Admin {0} Forum {1} does not exist", adminId, forumId));
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
            else throw new ExistException(string.Format("Moderator {0} Forum {1} does not exist", modId, subforumId));
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

        public Forum[] GetForumsUserID(int userId)
        {
            List<Forum> forums = new List<Forum>();
            foreach (IUser iuser in db.IUsers)
            {
                if (iuser.id == userId)
                    forums.Add(GetForum(iuser.forumId));
            }
            return forums.ToArray();
        }

        public SuperUser CreateSuperUser(SuperUser superuser)
        {
            ISuperUser isuperuser = db.ISuperUsers.FirstOrDefault(x => x.id == superuser.Id);
            if (isuperuser == null)
            {
                isuperuser = new ISuperUser();
                isuperuser.id = superuser.Id;
                isuperuser.userName = superuser.Username;
                isuperuser.password = superuser.Password;
                db.ISuperUsers.InsertOnSubmit(isuperuser);
                db.SubmitChanges();
                return superuser;
            }
            else
                throw new ExistException(string.Format("CreateSuperUser:  SuperUser {0} exists", superuser.Id));

        }

        public SuperUser UpdateSuperUser(SuperUser superuser)
        {
            ISuperUser isuperuser = db.ISuperUsers.FirstOrDefault(x => x.id == superuser.Id);
            if (isuperuser != null)
            {
                isuperuser.userName = superuser.Username;
                isuperuser.password = superuser.Password;
                db.SubmitChanges();
                return superuser;
            }
            else
                throw new ExistException(string.Format("SuperUser {0} does not exist", superuser.Id));
        }

        public bool DeleteSuperUser(int superuserId)
        {
            ISuperUser isuperuser = db.ISuperUsers.FirstOrDefault(x => x.id == superuserId);
            if (isuperuser != null)
            {
                db.ISuperUsers.DeleteOnSubmit(isuperuser);
                db.SubmitChanges();
                return true;
            }
            else
                throw new ExistException(string.Format("SuperUser {0} does not exist", superuserId));
        }

        public SuperUser GetSuperUser(int superUserId)
        {
            ISuperUser isuperuser = db.ISuperUsers.FirstOrDefault(x => x.id == superUserId);
            if (isuperuser != null)
            {
                SuperUser superUser = new SuperUser(isuperuser.id, isuperuser.userName, isuperuser.password);
                return superUser;
            }
            else
                throw new ExistException(string.Format("SuperUser {0} does not exist", superUserId));
        }

        public SuperUser[] GetSuperUsers(int[] superuserIds)
        {
            List<SuperUser> superusers = new List<SuperUser>();
            foreach (ISuperUser isuperuser in db.ISuperUsers)
                if (superuserIds == null || superuserIds.Contains(isuperuser.id))
                    superusers.Add(GetSuperUser(isuperuser.id));
            return superusers.ToArray();

        }



        public Notification GetNotification(int notificationId)
        {
            INotification inoti = db.INotifications.FirstOrDefault(x => x.id == notificationId);
            if (inoti != null)
            {
                Notification noti = new Notification(inoti.id, inoti.message, inoti.isNew, GetUser(inoti.fromUserId, inoti.fromForumId), GetUser(inoti.toUserId, inoti.toForumId), this);
                return noti;
            }
            throw new GetException(string.Format("Notifitcation {0} wasn't found", notificationId));
        }
        public bool DeleteNotification(int notificationId)
        {
            INotification inoti = db.INotifications.FirstOrDefault(x => x.id == notificationId);

            if (inoti != null)
            {
                db.INotifications.DeleteOnSubmit(inoti);
                db.SubmitChanges();
                return true;
            }
            else throw new ExistException(string.Format("Notification {0} does not exist", notificationId));
        }
        public Notification[] GetNotifications(int[] notificationsIds)
        {
            List<Notification> notifications = new List<Notification>();
            foreach (INotification inot in db.INotifications)
                if (notificationsIds == null || notificationsIds.Contains(inot.id))
                    notifications.Add(GetNotification(inot.id));
            return notifications.ToArray();
        }
        public Notification CreateNotification(Notification notification)
        {
            INotification inot = new INotification();
            inot.id = getNextNotificationId();
            inot.fromUserId = notification.Source.Id;
            inot.fromForumId = notification.Source.Forum.Id;
            inot.toUserId = notification.Target.Id;
            inot.toForumId = notification.Target.Forum.Id;
            inot.isNew = notification.IsNew;
            inot.message = notification.Message;

            db.INotifications.InsertOnSubmit(inot);
            db.SubmitChanges();
            notification.Id = inot.id;
            return notification;
        }

        public Notification UpdateNotification(Notification notification)
        {
            INotification inot = new INotification();
            inot.fromUserId = notification.Source.Id;
            inot.fromForumId = notification.Source.Forum.Id;
            inot.toUserId = notification.Target.Id;
            inot.toForumId = notification.Target.Forum.Id;
            inot.isNew = notification.IsNew;
            inot.message = notification.Message;

            db.SubmitChanges();
            return notification;
        }



        public Admin[] GetAdminsOfForum(int forumId)
        {
            IAdmin[] iadmins = db.IAdmins.Where(x => x.forumId == forumId).ToArray();
            List<Admin> admins = new List<Admin>();
            foreach (IAdmin iadm in iadmins)
                admins.Add(GetAdmin(iadm.userId, iadm.forumId));
            return admins.ToArray();
        }

        public Post[] GetPostsOfUser(int userId, int forumId)
        {
            IPost[] iposts = db.IPosts.Where(x => x.userId == userId && x.forumId == forumId).ToArray();
            List<Post> posts = new List<Post>();
            foreach (IPost ip in iposts)
                posts.Add(GetPost(ip.id));
            return posts.ToArray();
        }
        public Post[] GetReplysPost(int id)
        {
            IPost[] iposts = db.IPosts.Where(x => x.reply == id).ToArray();
            List<Post> posts = new List<Post>();
            foreach (IPost ip in iposts)
                posts.Add(GetPost(ip.id));
            return posts.ToArray();
        }

        public Moderator[] GetAppointedModsOfAdmin(int adminId, int forumid)
        {
            IModerator[] imods = db.IModerators.Where(x => x.byAdmin == adminId && x.forumId == forumid).ToArray();
            List<Moderator> mods = new List<Moderator>();
            foreach (IModerator im in imods)
                mods.Add(GetModerator(im.userId, im.subForumId));
            return mods.ToArray();
        }

        public User[] GetForumMembers(int forumID)
        {
            GetForum(forumID); //checking that forumID exists
            List<User> users = new List<User>();
            foreach (IUser iuser in db.IUsers.Where(x => x.forumId == forumID))
                users.Add(GetUser(iuser.id, iuser.forumId));
            return users.ToArray();

        }

        public Admin[] GetForumAdmins(int forumID)
        {
            GetForum(forumID); //checking that forumID exists
            List<Admin> admins = new List<Admin>();
            foreach (IAdmin iadmin in db.IAdmins.Where(x => x.forumId == forumID))
                admins.Add(GetAdmin(iadmin.userId, iadmin.forumId));
            return admins.ToArray();
        }

        public Policy GetForumPolicy(int forumID)
        {
            throw new NotImplementedException();
        }

        public Subforum[] GetForumSubForums(int forumID)
        {
            ISubForum[] isubforums = db.ISubForums.Where(x => x.forumId == forumID).ToArray();
            List<Subforum> subforums = new List<Subforum>();
            foreach (ISubForum ifm in isubforums)
                subforums.Add(GetSubForum(ifm.id));
            return subforums.ToArray();
        }

        public Moderator[] GetSubForumMods(int subForumID)
        {
            List<Moderator> moderators = new List<Moderator>();
            foreach (IModerator imod in db.IModerators)
            {
                if (imod.subForumId == subForumID)
                    moderators.Add(GetModerator(imod.userId, imod.subForumId));
            }
            return moderators.ToArray();
        }


        public Post[] GetSubForumThreads(int subForumID)
        {
            IPost[] iposts = db.IPosts.Where(x => x.subforumId == subForumID && x.reply == null).ToArray();
            List<Post> posts = new List<Post>();
            foreach (IPost ip in iposts)
                posts.Add(GetPost(ip.id));
            return posts.ToArray();
        }
        public Post[] GetUserPosts(int userID, int forumId)
        {
            IPost[] iposts = db.IPosts.Where(x => x.userId == userID && x.forumId == forumId).ToArray();
            List<Post> posts = new List<Post>();
            foreach (IPost ip in iposts)
                posts.Add(GetPost(ip.id));
            return posts.ToArray();
        }



        public Forum GetSubForumForum(int subForumID)
        {
            return GetSubForum(subForumID).Forum;
        }


        public Notification[] GetUserNewNotifications(int userID)
        {
            List<Notification> nots = new List<Notification>();
            foreach (INotification inot in db.INotifications.Where(x => x.toUserId == userID && x.isNew == true))
            {
                nots.Add(GetNotification(inot.id));
            }
            return nots.ToArray();
        }

        public Notification[] GetUserNotifications(int userID)
        {
            List<Notification> nots = new List<Notification>();
            foreach (INotification inot in db.INotifications.Where(x => x.toUserId == userID))
            {
                nots.Add(GetNotification(inot.id));
            }
            return nots.ToArray();
        }


        public Subforum GetModeratorSubForum(int modID, int forumId)
        {
            IModerator imod = db.IModerators.FirstOrDefault(x => x.userId == modID && x.forumId == forumId);
            if (imod == null)
                throw new ExistException(string.Format("GetModeratorSubForum: Moderator {0} Forum {1} does not exist", modID, forumId));
            return GetSubForum(imod.subForumId);
        }

        public Admin GetModeratorAppointerAdmin(int modID, int subforumId)
        {
            return GetModerator(modID, subforumId).Appointer;
        }

        public Moderator[] GetAdminAppointedMods(int adminID, int forumId)
        {
            List<Moderator> mods = new List<Moderator>();
            foreach (IModerator imod in db.IModerators.Where(x => x.forumId == forumId && x.byAdmin == adminID))
            {
                mods.Add(GetModerator(imod.userId, imod.subForumId));
            }
            return mods.ToArray();
        }


        public Post[] GetReplies(int PostID)
        {
            List<Post> posts = new List<Post>();
            foreach (IPost ipost in db.IPosts.Where(x => x.reply == PostID))
            {
                posts.Add(GetPost(ipost.id));
            }
            return posts.ToArray();
        }

        public Admin[] GetAdminsOfForum(Forum forum)
        {
            List<Admin> admins = new List<Admin>();
            foreach (IAdmin iadmin in db.IAdmins.Where(x => x.forumId == forum.Id))
            {
                admins.Add(GetAdmin(iadmin.userId, iadmin.forumId));
            }
            return admins.ToArray();
        }

        private class UserCompId : IComparer<User>
        {
            public int Compare(User x, User y)
            {
                if (x.Id > y.Id)
                    return 1;
                else return -1;
            }
        }

        public User[] GetUsersInDiffForums()
        {
            List<User> users = new List<User>();
            List<User> deleteUsers = new List<User>();
            if (db.IUsers.Count() == 0 || users.Count() == 1) return users.ToArray();

            foreach (IUser iuser in db.IUsers)
                users.Add(GetUser(iuser.id, iuser.forumId));



            IComparer<User> comp = new UserCompId();
            users.Sort(comp);


            for (int i = 1; i < users.Count(); i++)
                if (!(users.ElementAt(i).Id == users.ElementAt(i - 1).Id || (users.Count() - 1 >= i + 1 && users.ElementAt(i).Id == users.ElementAt(i + 1).Id)))
                    deleteUsers.Add(users.ElementAt(i));
            if (users.ElementAt(0).Id != users.ElementAt(1).Id)
                deleteUsers.Add(users.ElementAt(0));

            foreach (User user in deleteUsers)
                users.Remove(user);
            return users.ToArray();

        }

        public Policy GetPolicy(int id)
        {
            IPolicy ipolicy = db.IPolicies.FirstOrDefault(x => x.id == id);
            if (ipolicy != null)
            {
                Policy polc = new Policy(id, (Policy.PostDeletePolicy)ipolicy.postDeletePolicy, TimeSpan.FromTicks(ipolicy.passwordPeriod), ipolicy.emailVerification, new TimeSpan(ipolicy.minimumSeniority), ipolicy.usersLoad);
                return polc;
            }
            throw new ExistException(string.Format("GetPolicy: Policy {0} does not exist", id));
        }

        public bool DeletePolicy(int id)
        {
            IPolicy ipol = db.IPolicies.FirstOrDefault(x => x.id == id);
            if (ipol != null)
            {
                db.IPolicies.DeleteOnSubmit(ipol);
                db.SubmitChanges();
                return true;
            }
            else throw new ExistException(string.Format("DeletePolicy: Policy {0} does not exist", id));
        }

        public Policy UpdatePolicy(Policy policy)
        {
            IPolicy ipolicy = db.IPolicies.FirstOrDefault(x => x.id == policy.Id);
            if (ipolicy != null)
            {
                ipolicy.emailVerification = policy.EmailVerfication;
                ipolicy.minimumSeniority = policy.MinimumSeniority.Ticks;
                ipolicy.passwordPeriod = policy.PasswordTimeSpan.Ticks;
                ipolicy.postDeletePolicy = (int)policy.SelectedPostDeletePolicy;
                ipolicy.usersLoad = policy.UsersLoad;
                return policy;
            }
            throw new ExistException(string.Format("UpdatePolicy: Policy {0} does not exist", policy.Id));
        }

        public Policy CreatePolicy(Policy policy)
        {
            IPolicy ipolicy = new IPolicy();
            ipolicy = new IPolicy();
            ipolicy.id = getNextPolicyId();
            ipolicy.emailVerification = policy.EmailVerfication;
            ipolicy.minimumSeniority = policy.MinimumSeniority.Ticks;
            ipolicy.passwordPeriod = policy.PasswordTimeSpan.Ticks;
            ipolicy.postDeletePolicy = (int)policy.SelectedPostDeletePolicy;
            ipolicy.usersLoad = policy.UsersLoad;

            db.IPolicies.InsertOnSubmit(ipolicy);
            db.SubmitChanges();
            policy.Id = ipolicy.id;
            return policy;
        }
    }
}