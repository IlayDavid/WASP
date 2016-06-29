using System;
using System.Collections.Generic;
using System.Linq;
using WASP.DataClasses.DAL_EXCEPTIONS;
using System.IO;
using static WASP.DataClasses.Notification;
using WASP.DataClasses.Cache2;
using WASP.LoggerPC;
namespace WASP.DataClasses
{
    class DALSQL : DAL2
    {
        private IDALCache2 _cache;
        private Logger _logger;
        private static string _connectionString;
        static Object DB_lock = new object();

        public static string SetDb(string dbName)
        {
            _connectionString =
                $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={Directory.GetParent(Directory.GetParent(
                        Directory.GetCurrentDirectory()).Parent.FullName)}\{dbName}.mdf;Integrated Security=True; MultipleActiveResultSets=True";
            //Connect Timeout=30
            return _connectionString;
        }

        public DALSQL()
        {
            _cache = new DALCache2(this);
            _logger = new Logger("../../../LoggerOutput/dal_logger.txt");
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
        private static List<IFriend> backUpFriends;

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
            backUpFriends = new List<IFriend>();

            Forum_SystemDataContext db = new Forum_SystemDataContext(SetDb("Forums_System"));

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
                pol.question1 = ipol.question1;
                pol.question2 = ipol.question2;
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
                iuser.answer1 = user.answer1;
                iuser.answer2 = user.answer2;

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
                inoti.sourceForum = noti.sourceForum;
                inoti.source = noti.source;
                inoti.type = noti.type;
                inoti.date = noti.date;
                inoti.toUserId = noti.toUserId;
                backUpNotifications.Add(inoti);
            }

            //9
            foreach (IFriend ifriend in db.IFriends)
            {
                IFriend ifrnd = new IFriend();
                ifrnd.forumId = ifriend.forumId;
                ifrnd.userId = ifriend.userId;
                ifrnd.friendId = ifriend.friendId;
            }
        }

        public static void GetBackUp()
        {
            lock (DB_lock)
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

                foreach (IFriend frnd in backUpFriends)
                {
                    db.IFriends.InsertOnSubmit(frnd);
                }
                db.SubmitChanges();
            }
        }
        private Forum_SystemDataContext db = new Forum_SystemDataContext(SetDb("Forums_System"));

        public void Clean()
        {
            lock (DB_lock)
            {
                _logger.deleteFile();
                db.IFriends.DeleteAllOnSubmit(db.IFriends);
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
                db = new Forum_SystemDataContext(SetDb("Forums_System"));
                _cache = new DALCache2(this);
            }
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
            lock (DB_lock)
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
                _user.answer1 = user.Answers[0];
                _user.answer2 = user.Answers[1];

                _user.onlineCount = user.OnlineCount;
                _user.wantNotifications = user.WantNotifications;
                _user.secret = user.Secret;

                db.IUsers.InsertOnSubmit(_user);
                db.SubmitChanges();

                _cache.AddUser(user.initialize());
                _logger.writeToFile("create user");
                return user;
            }
        }

        public Admin CreateAdmin(Admin admin)
        {
            lock (DB_lock)
            {
                IAdmin old_admin = db.IAdmins.FirstOrDefault(x => (x.userId == admin.Id && x.forumId == admin.Forum.Id));
                if (old_admin != null) throw new ExistException(string.Format("Admin {0}, Forum {1} exists in the DataBase", admin.Id, admin.Forum.Id));
                IUser curr_user = db.IUsers.FirstOrDefault(x => (x.id == admin.User.Id && x.forumId == admin.Forum.Id));
                if (curr_user == null) throw new ExistException(string.Format("user {0}, Forum {1} does not exists in the DataBase", admin.Id, admin.Forum.Id));
                IAdmin _admin = new IAdmin();
                _admin.userId = admin.User.Id;
                _admin.forumId = admin.Forum.Id;

                db.IAdmins.InsertOnSubmit(_admin);
                db.SubmitChanges();
                _cache.AddAdmin(admin.initialize());
                _logger.writeToFile("create admin");
                return admin;
            }
        }
        public Moderator CreateModerator(Moderator mod)
        {
            lock (DB_lock)
            {
                IModerator old_mod = db.IModerators.FirstOrDefault(x => x.userId == mod.Id && x.forumId == mod.SubForum.Forum.Id);
                if (old_mod != null)
                    throw new ExistException(string.Format("Moderator {0}, Forum {1} exists in the DataBase", mod.Id, mod.SubForum.Forum.Id));
                IUser curr_user = db.IUsers.FirstOrDefault(x => (x.id == mod.User.Id && x.forumId == mod.SubForum.Forum.Id));
                if (curr_user == null) throw new ExistException(string.Format("user {0}, Forum {1} does not exists in the DataBase", mod.Id, mod.SubForum.Forum.Id));

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

                _cache.AddModerator(mod);
                _logger.writeToFile("create moderator");
                return mod;
            }
        }
        public Forum CreateForum(Forum forum)
        {
            lock (DB_lock)
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

                _cache.AddForum(forum.initialize());
                _logger.writeToFile("create forum");

                return forum;
            }
        }
        public Subforum CreateSubForum(Subforum sf)
        {
            lock (DB_lock)
            {
                ISubForum _subf = new ISubForum();
                _subf.id = getNextSubForumId();
                _subf.subject = sf.Name;
                _subf.description = sf.Description;
                _subf.forumId = sf.Forum.Id;

                db.ISubForums.InsertOnSubmit(_subf);
                db.SubmitChanges();
                sf.Id = _subf.id;
                _logger.writeToFile("create sub-forum");
                _cache.AddSubforum(sf.initialize());

                return sf;
            }
        }
        public Post CreatePost(Post post)
        {
            lock (DB_lock)
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

                _cache.AddPost(post.initialize());


                return post;
            }
        }
        public User[] GetUsers(int[] userIds, int forumId)
        {
            lock (DB_lock)
            {
                List<User> users = new List<User>();
                foreach (IUser iuser in db.IUsers)
                    if (iuser.forumId == forumId && (userIds == null || userIds.Contains(iuser.id)))
                        users.Add(GetUser(iuser.id, iuser.forumId));
            
                return users.ToArray();
            }
        }
        public Moderator[] GetModerators(int[] moderatorIds, Subforum subforum)
        {
            lock (DB_lock)
            {
                List<Moderator> moderators = new List<Moderator>();
                foreach (IModerator imoderator in db.IModerators)
                    if ((subforum == null || imoderator.ISubForum.id == subforum.Id) &&
                                (moderatorIds == null || moderatorIds.Contains(imoderator.userId)))
                        moderators.Add(GetModerator(imoderator.userId, imoderator.subForumId));

                return moderators.ToArray();
            }
        }
        public Admin[] GetAdmins(int[] adminsIds, Forum forum)
        {
            lock (DB_lock)
            {
                List<Admin> admins = new List<Admin>();
                foreach (IAdmin iadmin in db.IAdmins)
                    if ((forum == null || iadmin.forumId == forum.Id) &&
                        (adminsIds == null || adminsIds.Contains(iadmin.userId)))
                        admins.Add(GetAdmin(iadmin.userId, iadmin.forumId));

                return admins.ToArray();
            }
        }
        public Forum[] GetForums(int[] forumsIds)
        {
            lock (DB_lock)
            {
                List<Forum> forums = new List<Forum>();
                foreach (IForum iforum in db.IForums)
                    if (forumsIds == null || forumsIds.Contains(iforum.id))
                        forums.Add(GetForum(iforum.id));
                return forums.ToArray();
            }
        }
        public Subforum[] GetSubForums(int[] subForumIds)
        {
            lock (DB_lock)
            {
                List<Subforum> subforums = new List<Subforum>();

                foreach (ISubForum isf in db.ISubForums)
                    if (subForumIds == null || subForumIds.Contains(isf.id))
                        subforums.Add(GetSubForum(isf.id));
                return subforums.ToArray();
            }
        }
        public Post[] GetPosts(int[] Posts)
        {
            lock (DB_lock)
            {
                List<Post> posts = new List<Post>();
                foreach (IPost ipost in db.IPosts)
                    if (Posts == null || Posts.Contains(ipost.id))
                        posts.Add(GetPost(ipost.id));
                return posts.ToArray();
            }
        }


        public Admin UpdateAdmin(Admin admin)
        {
            lock (DB_lock)
            {
                IAdmin iadmin = db.IAdmins.FirstOrDefault(x => (x.userId == admin.Id && x.forumId == admin.Forum.Id));
                if (iadmin != null)
                {
                    iadmin.forumId = admin.Forum.Id;
                    iadmin.userId = admin.User.Id;
                    UpdateUser(admin.User);
                    db.SubmitChanges();
                    _cache.AddAdmin(admin);
                    return admin;
                }
                throw new UpdateException(String.Format("Admin {0} Forum {0} wasn't found", admin.Id, admin.Forum.Id));
            }
        }
        public Forum UpdateForum(Forum forum)
        {
            lock (DB_lock)
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
                    _cache.AddForum(forum);
                    return forum;
                }
                else throw new UpdateException(String.Format("Forum {0} wasn't found", forum.Id));
            }
        }
        public Moderator UpdateModerator(Moderator mod)
        {
            lock (DB_lock)
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
                    _cache.AddModerator(mod);
                    return mod;
                }
                throw new UpdateException(String.Format("Moderator {0} Forum {0} wasn't found", mod.Id, mod.SubForum.Forum.Id));
            }
        }

        /// <summary>
        /// no moderators update
        /// </summary>
        /// <param name="sf"></param>
        /// <returns></returns>
        public Subforum UpdateSubForum(Subforum sf)
        {
            lock (DB_lock)
            {
                ISubForum isf = db.ISubForums.FirstOrDefault(x => (x.id == sf.Id));
                if (isf != null)
                {
                    isf.subject = sf.Name;
                    isf.description = sf.Description;
                    isf.forumId = sf.Forum.Id;
                    db.SubmitChanges();
                    _cache.AddSubforum(sf);
                    return sf;
                }
                throw new UpdateException(String.Format("sub-forum {0} wasn't found", sf.Id));
            }
        }

        /// <summary>
        /// no notification update
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User UpdateUser(User user)
        {
            lock (DB_lock)
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
                    iuser.answer1 = user.Answers[0];
                    iuser.answer2 = user.Answers[1];
                    iuser.onlineCount = user.OnlineCount;
                    iuser.wantNotifications = user.WantNotifications;
                    iuser.secret = user.Secret;

                    db.SubmitChanges();
                    _cache.AddUser(user);
                    return user;
                }
                throw new UpdateException(String.Format("user {0} wasn't found", user.Id));
            }
        }
        public Post UpdatePost(Post post)
        {
            lock (DB_lock)
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

                    _cache.AddPost(post);
                    return post;
                }
                throw new UpdateException(String.Format("post {0} wasn't found", post.Id));
            }
        }

        public Forum GetForum(int id)
        {
            lock (DB_lock)
            {
                Forum cf = _cache.GetForum(id);
                if (cf != null)
                    return cf;
                IForum iforum = db.IForums.FirstOrDefault(x => (x.id == id));
                if (iforum != null)
                {
                    Forum forum = new Forum(iforum.id, iforum.subject, iforum.description, null, this);
                    if (iforum.policyId != null)
                        forum.Policy = GetPolicy((int)iforum.policyId);
                    _cache.AddForum(forum);
                    return forum.initialize();

                }
                throw new GetException(string.Format("forum {0} wasn't found", id));
            }
        }


        public Subforum GetSubForum(int sfId)
        {
            lock (DB_lock)
            {
                Subforum csf = _cache.GetSubforum(sfId);
                if (csf != null)
                    return csf;

                ISubForum isf = db.ISubForums.FirstOrDefault(x => (x.id == sfId));
                if (isf != null)
                {
                    Subforum sf = new Subforum(isf.id, isf.subject, isf.description, GetForum(isf.forumId), this);
                    _cache.AddSubforum(sf);
                    return sf.initialize();
                }
                throw new GetException(string.Format("sub-forum {0} wasn't found", sfId));
            }
        }


        public User GetUser(int id, int forumId)
        {
            lock (DB_lock)
            {
                User cuser = _cache.GetUser(id, forumId);
                if (cuser != null)
                    return cuser;
                IUser iuser = db.IUsers.FirstOrDefault(x => (x.id == id && x.forumId == forumId));
                if (iuser != null)
                {
                    Forum forum = GetForum(forumId);
                    string[] answers = { iuser.answer1, iuser.answer2 };
                    User user = new User(iuser.id, iuser.name, iuser.userName, iuser.email, iuser.password, forum, iuser.StartDate, iuser.PasswordChangeDate, answers, iuser.wantNotifications);
                    _cache.AddUser(user);
                    return user.initialize();
                }
                throw new GetException(string.Format("user {0} wasn't found", id));
            }
        }



        public Moderator GetModerator(int id, int sfId)
        {
            lock (DB_lock)
            {
                Moderator cmod = _cache.GetModerator(id, sfId);
                if (cmod != null)
                    return cmod;

                IModerator imoderator = db.IModerators.FirstOrDefault(x => (x.userId == id && x.subForumId == sfId));
                if (imoderator != null)
                {
                    Subforum subforum = GetSubForum(sfId);
                    Admin admin = GetAdmin(imoderator.byAdmin, imoderator.forumId);
                    User user = GetUser(id, imoderator.forumId);

                    Moderator moderator = new Moderator(user, imoderator.term, subforum, admin, imoderator.startDate);
                    _cache.AddModerator(moderator);
                    return moderator;
                }
                throw new GetException(string.Format("moderator {0} wasn't found", id));
            }
        }
        public Admin GetAdmin(int adminId, int forumId)
        {
            lock (DB_lock)
            {
                Admin cadmin = _cache.GetAdmin(adminId, forumId);
                if (cadmin != null)
                    return cadmin;
                IAdmin iadmin = db.IAdmins.FirstOrDefault(x => (x.userId == adminId && x.forumId == forumId));
                if (iadmin != null)
                {
                    Forum forum = GetForum(forumId);
                    User user = GetUser(adminId, forumId);
                    Admin admin = new Admin(user, forum, this);
                    _cache.AddAdmin(admin);
                    return admin.initialize();
                }
                throw new GetException(string.Format("admin {0} wasn't found", adminId));
            }
        }

        public Post GetPost(int postId)
        {
            lock (DB_lock)
            {
                Post cpost = _cache.GetPost(postId);
                if (cpost != null)
                    return cpost;

                IPost ipost = db.IPosts.FirstOrDefault(x => x.id == postId);
                if (ipost != null)
                {
                    Post replyTo = null;
                    if (ipost.reply != null) replyTo = GetPost((int)ipost.reply);
                    Post post = new Post(ipost.id, ipost.title, ipost.cnt, GetUser(ipost.userId, ipost.IUser.forumId), ipost.publishAt,
                       replyTo, GetSubForum(ipost.subforumId), ipost.editAt, this);

                    _cache.AddPost(post);
                    return post.initialize();
                }
                throw new GetException(string.Format("post {0} wasn't found", postId));
            }
        }



        public bool DeletePost(int postId)
        {
            lock (DB_lock)
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
                    _cache.RemovePost(postId);
                    return true;
                }
                else throw new ExistException(string.Format("Post {0} does not exist", postId));
            }
            // delete moderator, delete forum, delete subforum
        }
        public bool DeleteUser(int id, int forumId)
        {
            lock (DB_lock)
            {
                IUser iuser = db.IUsers.FirstOrDefault(x => x.id == id && x.forumId == forumId);
                if (iuser != null)
                {
                    db.IUsers.DeleteOnSubmit(iuser);
                    db.SubmitChanges();
                    _cache.RemoveUser(id, forumId);
                    return true;
                }
                else throw new ExistException(string.Format("User {0} Forum {1} does not exist", id, forumId));
            }
        }
        public bool DeleteAdmin(int adminId, int forumId)
        {
            lock (DB_lock)
            {
                IAdmin iadmin = db.IAdmins.FirstOrDefault(x => x.userId == adminId && x.forumId == forumId);
                if (iadmin != null)
                {
                    int userId = iadmin.userId;
                    db.IAdmins.DeleteOnSubmit(iadmin);
                    db.SubmitChanges();
                    DeleteUser(userId, forumId);
                    _cache.RemoveAdmin(adminId, forumId);
                    return true;
                }
                else throw new ExistException(string.Format("Admin {0} Forum {1} does not exist", adminId, forumId));
            }
        }
        public bool DeleteModerator(int modId, int subforumId)
        {
            lock (DB_lock)
            {
                IModerator imoderator = db.IModerators.FirstOrDefault(x => x.userId == modId && x.subForumId == subforumId);
                if (imoderator != null)
                {
                    int userId = imoderator.userId;
                    int forumId = imoderator.forumId;
                    db.IModerators.DeleteOnSubmit(imoderator);
                    db.SubmitChanges();
                    DeleteUser(userId, forumId);
                    _cache.RemoveModerator(modId, subforumId);
                    return true;
                }
                else throw new ExistException(string.Format("Moderator {0} Forum {1} does not exist", modId, subforumId));
            }
        }
        public bool DeleteForum(int forumId)
        {
            lock (DB_lock)
            {
                IForum iforum = db.IForums.FirstOrDefault(x => x.id == forumId);
                if (iforum != null)
                {
                    db.IForums.DeleteOnSubmit(iforum);
                    db.SubmitChanges();
                    _cache.RemoveForum(forumId);
                    return true;
                }
                else throw new ExistException(string.Format("Forum {0} does not exist", forumId));
            }
        }
        public bool DeleteSubforum(int subforumId)
        {
            lock (DB_lock)
            {
                ISubForum isubforum = db.ISubForums.FirstOrDefault(x => x.id == subforumId);
                if (isubforum != null)
                {
                    db.ISubForums.DeleteOnSubmit(isubforum);
                    db.SubmitChanges();
                    _cache.RemoveSubforum(subforumId);
                    return true;
                }
                else throw new ExistException(string.Format("subforum {0} does not exist", subforumId));
            }
        }

        public Forum[] GetForumsUserID(int userId)
        {
            lock (DB_lock)
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

        public SuperUser CreateSuperUser(SuperUser superuser)
        {
            lock (DB_lock)
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
                    _cache.AddSuperUser(superuser);
                    _logger.writeToFile("added superuser");
                    _logger.writeToFile("created superuser");
                    return superuser;
                }
                else
                    throw new ExistException(string.Format("CreateSuperUser:  SuperUser {0} exists", superuser.Id));

            }
        }

        public SuperUser UpdateSuperUser(SuperUser superuser)
        {
            ISuperUser isuperuser = db.ISuperUsers.FirstOrDefault(x => x.id == superuser.Id);
            if (isuperuser != null)
            {
                isuperuser.userName = superuser.Username;
                isuperuser.password = superuser.Password;
                db.SubmitChanges();
                _cache.AddSuperUser(superuser);
                return superuser;
            }
            else
                throw new ExistException(string.Format("SuperUser {0} does not exist", superuser.Id));
        }

        public bool DeleteSuperUser(int superuserId)
        {
            lock (DB_lock)
            {
                ISuperUser isuperuser = db.ISuperUsers.FirstOrDefault(x => x.id == superuserId);
                if (isuperuser != null)
                {
                    db.ISuperUsers.DeleteOnSubmit(isuperuser);
                    db.SubmitChanges();
                    _cache.RemoveSuperUser(superuserId);
                    return true;
                }
                else
                    throw new ExistException(string.Format("SuperUser {0} does not exist", superuserId));
            }
        }

        public SuperUser GetSuperUser(int superUserId)
        {
            lock (DB_lock)
            {
                SuperUser csu = _cache.GetSuperUser(superUserId);
                if (csu != null)
                    return csu;

                ISuperUser isuperuser = db.ISuperUsers.FirstOrDefault(x => x.id == superUserId);
                if (isuperuser != null)
                {
                    SuperUser superUser = new SuperUser(isuperuser.id, isuperuser.userName, isuperuser.password);
                    _cache.AddSuperUser(superUser);
                    return superUser;
                }
                else
                    throw new ExistException(string.Format("SuperUser {0} does not exist", superUserId));
            }
        }

        public SuperUser[] GetSuperUsers(int[] superuserIds)
        {
            lock (DB_lock)
            {
                List<SuperUser> superusers = new List<SuperUser>();
                foreach (ISuperUser isuperuser in db.ISuperUsers)
                    if (superuserIds == null || superuserIds.Contains(isuperuser.id))
                        superusers.Add(GetSuperUser(isuperuser.id));
                return superusers.ToArray();
            }
        }



        public Notification GetNotification(int notificationId)
        {
            lock (DB_lock)
            {
                INotification inoti = db.INotifications.FirstOrDefault(x => x.id == notificationId);
                if (inoti != null)
                {
                    User source = null;
                    if (inoti.source > 0)
                        source = GetUser(inoti.source, inoti.sourceForum);
                    Notification noti = new Notification(inoti.id, inoti.message, inoti.isNew, source, GetUser(inoti.toUserId, inoti.sourceForum), (Types)inoti.type, inoti.date);
                    return noti;
                }
                throw new GetException(string.Format("Notifitcation {0} wasn't found", notificationId));
            }
        }
        public bool DeleteNotification(int notificationId)
        {
            lock (DB_lock)
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
        }
        public Notification[] GetNotifications(int[] notificationsIds)
        {
            lock (DB_lock)
            {
                List<Notification> notifications = new List<Notification>();
                foreach (INotification inot in db.INotifications)
                    if (notificationsIds == null || notificationsIds.Contains(inot.id))
                        notifications.Add(GetNotification(inot.id));
                return notifications.ToArray();
            }
        }
        public Notification CreateNotification(Notification notification)
        {
            lock (DB_lock)
            {
                if (notification.Source != null && notification.Source.Forum.Id != notification.Target.Forum.Id)
                    throw new InvalidException(string.Format("CreateNotification: target's forum ({1}) and source's forum ({1}) do not match", notification.Target.Forum.Id, notification.Source.Forum.Id));
                INotification inot = new INotification();
                inot.id = getNextNotificationId();
                if (notification.Source == null)
                    inot.source = 0;
                inot.sourceForum = notification.Target.Forum.Id;
                inot.toUserId = notification.Target.Id;
                inot.isNew = notification.IsNew;
                inot.message = notification.Message;
                inot.date = notification.CreationTime;
                inot.type = (int)notification.Type;
                db.INotifications.InsertOnSubmit(inot);
                db.SubmitChanges();
                notification.Id = inot.id;
                return notification;
            }
        }

        public Notification UpdateNotification(Notification notification)
        {
            lock (DB_lock)
            {
                INotification inot = db.INotifications.FirstOrDefault(x => x.id == notification.Id);
                if (inot != null)
                {
                    int source = 0;
                    if (notification.Source != null)
                        source = notification.Source.Id;

                    inot.source = source;
                    inot.toUserId = notification.Target.Id;
                    inot.sourceForum = notification.Target.Forum.Id;
                    inot.isNew = notification.IsNew;
                    inot.message = notification.Message;
                    inot.date = notification.CreationTime;
                    inot.type = (int)notification.Type;
                    db.SubmitChanges();
                    return notification;
                }
                throw new ExistException(string.Format("UpdateNotification: Notification {0} does not exist", notification.Id));

            }
        }

        public Admin[] GetAdminsOfForum(int forumId)
        {
            lock (DB_lock)
            {
                IAdmin[] iadmins = db.IAdmins.Where(x => x.forumId == forumId).ToArray();
                List<Admin> admins = new List<Admin>();
                foreach (IAdmin iadm in iadmins)
                    admins.Add(GetAdmin(iadm.userId, iadm.forumId));
                return admins.ToArray();
            }
        }

        public Post[] GetPostsOfUser(int userId, int forumId)
        {
            lock (DB_lock)
            {
                IPost[] iposts = db.IPosts.Where(x => x.userId == userId && x.forumId == forumId).ToArray();
                List<Post> posts = new List<Post>();
                foreach (IPost ip in iposts)
                    posts.Add(GetPost(ip.id));
                return posts.ToArray();
            }
        }
        public Post[] GetReplysPost(int id)
        {
            lock (DB_lock)
            {
                IPost[] iposts = db.IPosts.Where(x => x.reply == id).ToArray();
                List<Post> posts = new List<Post>();
                foreach (IPost ip in iposts)
                    posts.Add(GetPost(ip.id));
                return posts.ToArray();
            }
        }

        public Moderator[] GetAppointedModsOfAdmin(int adminId, int forumid)
        {
            lock (DB_lock)
            {
                IModerator[] imods = db.IModerators.Where(x => x.byAdmin == adminId && x.forumId == forumid).ToArray();
                List<Moderator> mods = new List<Moderator>();
                foreach (IModerator im in imods)
                    mods.Add(GetModerator(im.userId, im.subForumId));
                return mods.ToArray();
            }
        }

        public User[] GetForumMembers(int forumID)
        {
            lock (DB_lock)
            {
                GetForum(forumID); //checking that forumID exists
                List<User> users = new List<User>();
                foreach (IUser iuser in db.IUsers.Where(x => x.forumId == forumID))
                    users.Add(GetUser(iuser.id, iuser.forumId));
                return users.ToArray();

            }
        }
        public Admin[] GetForumAdmins(int forumID)
        {
            lock (DB_lock)
            {
                GetForum(forumID); //checking that forumID exists
                List<Admin> admins = new List<Admin>();
                foreach (IAdmin iadmin in db.IAdmins.Where(x => x.forumId == forumID))
                    admins.Add(GetAdmin(iadmin.userId, iadmin.forumId));
                return admins.ToArray();
            }
        }
        public Policy GetForumPolicy(int forumID)
        {
            lock (DB_lock)
            {
                IForum iforum = db.IForums.First(x => x.id == forumID);
                IPolicy ipol = db.IPolicies.First(x => x.id == iforum.id);
                string[] questions = { ipol.question1, ipol.question2 };
                Policy pol = new Policy(ipol.id, (Policy.PostDeletePolicy)ipol.postDeletePolicy,
                                        TimeSpan.FromTicks(ipol.passwordPeriod),
                                        ipol.emailVerification, TimeSpan.FromTicks(ipol.minimumSeniority),
                                        ipol.usersLoad, questions, ipol.notifyOffline);
                return pol;
            }
        }
        public Subforum[] GetForumSubForums(int forumID)
        {
            lock (DB_lock)
            {
                ISubForum[] isubforums = db.ISubForums.Where(x => x.forumId == forumID).ToArray();
                List<Subforum> subforums = new List<Subforum>();
                foreach (ISubForum ifm in isubforums)
                    subforums.Add(GetSubForum(ifm.id));
                return subforums.ToArray();
            }
        }
        public Moderator[] GetSubForumMods(int subForumID)
        {
            lock (DB_lock)
            {
                List<Moderator> moderators = new List<Moderator>();
                foreach (IModerator imod in db.IModerators)
                {
                    if (imod.subForumId == subForumID)
                        moderators.Add(GetModerator(imod.userId, imod.subForumId));
                }
                return moderators.ToArray();
            }
        }


        public Post[] GetSubForumThreads(int subForumID)
        {
            lock (DB_lock)
            {
                IPost[] iposts = db.IPosts.Where(x => x.subforumId == subForumID && x.reply == null).ToArray();
                List<Post> posts = new List<Post>();
                foreach (IPost ip in iposts)
                    posts.Add(GetPost(ip.id));
                return posts.ToArray();
            }
        }
        public Post[] GetUserPosts(int userID, int forumId)
        {
            lock (DB_lock)
            {
                IPost[] iposts = db.IPosts.Where(x => x.userId == userID && x.forumId == forumId).ToArray();
                List<Post> posts = new List<Post>();
                foreach (IPost ip in iposts)
                    posts.Add(GetPost(ip.id));
                return posts.ToArray();
            }
        }


        public Forum GetSubForumForum(int subForumID)
        {
            lock (DB_lock)
            {
                return GetSubForum(subForumID).Forum;
            }
        }


        public Notification[] GetUserNewNotifications(int userID)
        {
            lock (DB_lock)
            {
                List<Notification> nots = new List<Notification>();
                foreach (INotification inot in db.INotifications.Where(x => x.toUserId == userID && x.isNew == true))
                {
                    nots.Add(GetNotification(inot.id));
                }
                return nots.ToArray();
            }
        }

        public Notification[] GetUserNotifications(int userID)
        {
            lock (DB_lock)
            {
                List<Notification> nots = new List<Notification>();
                foreach (INotification inot in db.INotifications.Where(x => x.toUserId == userID))
                {
                    nots.Add(GetNotification(inot.id));
                }
                return nots.ToArray();
            }
        }


        public Subforum GetModeratorSubForum(int modID, int forumId)
        {
            lock (DB_lock)
            {
                IModerator imod = db.IModerators.FirstOrDefault(x => x.userId == modID && x.forumId == forumId);
                if (imod == null)
                    throw new ExistException(string.Format("GetModeratorSubForum: Moderator {0} Forum {1} does not exist", modID, forumId));
                return GetSubForum(imod.subForumId);
            }
        }

        public Admin GetModeratorAppointerAdmin(int modID, int subforumId)
        {
            lock (DB_lock)
            {
                return GetModerator(modID, subforumId).Appointer;
            }
        }

        public Moderator[] GetAdminAppointedMods(int adminID, int forumId)
        {
            lock (DB_lock)
            {
                List<Moderator> mods = new List<Moderator>();
                foreach (IModerator imod in db.IModerators.Where(x => x.forumId == forumId && x.byAdmin == adminID))
                {
                    mods.Add(GetModerator(imod.userId, imod.subForumId));
                }
                return mods.ToArray();
            }
        }


        public Post[] GetReplies(int PostID)
        {
            lock (DB_lock)
            {
                List<Post> posts = new List<Post>();
                foreach (IPost ipost in db.IPosts.Where(x => x.reply == PostID))
                {
                    posts.Add(GetPost(ipost.id));
                }
                return posts.ToArray();
            }
        }

        public Admin[] GetAdminsOfForum(Forum forum)
        {
            lock (DB_lock)
            {
                List<Admin> admins = new List<Admin>();
                foreach (IAdmin iadmin in db.IAdmins.Where(x => x.forumId == forum.Id))
                {
                    admins.Add(GetAdmin(iadmin.userId, iadmin.forumId));
                }
                return admins.ToArray();
            }
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
            lock (DB_lock)
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
        }

        public Policy GetPolicy(int id)
        {
            lock (DB_lock)
            {
                IPolicy ipolicy = db.IPolicies.FirstOrDefault(x => x.id == id);
                if (ipolicy != null)
                {
                    string[] questions = { ipolicy.question1, ipolicy.question2 };
                    Policy polc = new Policy(id, (Policy.PostDeletePolicy)ipolicy.postDeletePolicy, TimeSpan.FromTicks(ipolicy.passwordPeriod), ipolicy.emailVerification, new TimeSpan(ipolicy.minimumSeniority), ipolicy.usersLoad, questions, ipolicy.notifyOffline);
                    return polc;
                }
                throw new ExistException(string.Format("GetPolicy: Policy {0} does not exist", id));
            }
        }

        public bool DeletePolicy(int id)
        {
            lock (DB_lock)
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
        }

        public Policy UpdatePolicy(Policy policy)
        {
            lock (DB_lock)
            {
                IPolicy ipolicy = db.IPolicies.FirstOrDefault(x => x.id == policy.Id);
                if (ipolicy != null)
                {
                    ipolicy.emailVerification = policy.EmailVerfication;
                    ipolicy.minimumSeniority = policy.MinimumSeniority.Ticks;
                    ipolicy.passwordPeriod = policy.PasswordTimeSpan.Ticks;
                    ipolicy.postDeletePolicy = (int)policy.SelectedPostDeletePolicy;
                    ipolicy.usersLoad = policy.UsersLoad;
                    ipolicy.question1 = policy.Questions[0];
                    ipolicy.question2 = policy.Questions[1];
                    ipolicy.notifyOffline = policy.NotifyOffline;
                    return policy;
                }
                throw new ExistException(string.Format("UpdatePolicy: Policy {0} does not exist", policy.Id));
            }
        }

        public Policy CreatePolicy(Policy policy)
        {
            lock (DB_lock)
            {
                IPolicy ipolicy = new IPolicy();
                ipolicy = new IPolicy();
                ipolicy.id = getNextPolicyId();
                ipolicy.emailVerification = policy.EmailVerfication;
                ipolicy.minimumSeniority = policy.MinimumSeniority.Ticks;
                ipolicy.passwordPeriod = policy.PasswordTimeSpan.Ticks;
                ipolicy.postDeletePolicy = (int)policy.SelectedPostDeletePolicy;
                ipolicy.usersLoad = policy.UsersLoad;
                ipolicy.question1 = policy.Questions[0];
                ipolicy.question2 = policy.Questions[1];
                ipolicy.notifyOffline = policy.NotifyOffline;

                db.IPolicies.InsertOnSubmit(ipolicy);
                db.SubmitChanges();
                policy.Id = ipolicy.id;
                return policy;
            }
        }

        public User[] GetUserFriends(int id, int forumId)
        {
            lock (DB_lock)
            {
                List<User> friends = new List<User>();
                foreach (IFriend ifriend in db.IFriends)
                    if (ifriend.userId == id && ifriend.forumId == forumId)
                        friends.Add(GetUser(ifriend.friendId, forumId));
                return friends.ToArray();
            }
        }

        // אני מניח ששניהם קיימים במערכת
        public void AddFriend(User user, User friend)
        {
            lock (DB_lock)
            {
                IUser usr = db.IUsers.FirstOrDefault(x => (x.id == user.Id && x.forumId == user.Forum.Id));
                if (usr == null) throw new ExistException(String.Format("AddFriend: User {0} Forum {1} is not exists in the DataBase", user.Id, user.Forum.Id));

                IUser frnd = db.IUsers.FirstOrDefault(x => (x.id == friend.Id && x.forumId == friend.Forum.Id));
                if (frnd == null) throw new ExistException(String.Format("AddFriend: User (the friend) {0} Forum {1} is not exists in the DataBase", friend.Id, friend.Forum.Id));

                if (user.Forum.Id != friend.Forum.Id)
                    throw new ExistException(String.Format("AddFriend: the friend's ({0}) forum and user's ({1}) forum do not match", friend.Id, user.Id));

                IFriend _friend = new IFriend();
                _friend.userId = user.Id;
                _friend.friendId = friend.Id;
                _friend.forumId = user.Forum.Id;

                db.IFriends.InsertOnSubmit(_friend);
                db.SubmitChanges();
            }
        }
    }
}