using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.DataClasses
{
    class DALSQL : DAL
    {
        static private int forum_counter = 1;
        static private int user_counter = 1;
        static private int admin_counter = 1;
        static private int moderator_counter = 1;

        private ForumSystemDataContext db = new ForumSystemDataContext();
        
        public Admin CreateAdmin(Admin admin)
        {
            IAdmin _admin = new IAdmin();
            _admin.forumId = admin.Forum.Id;
            // למה אדמין צריך ת.ז. אם יש לו מנחה שמייצג אותו..?
            // אם אני יוצר מנחה, מה התאריך שלו..?
            // אפשר להגיד שאין לו תאריך ואז זה לכל הזמן..
            //_admin.userId = admin.
            db.SubmitChanges();
            
            throw new NotImplementedException();
        }

        public Forum CreateForum(Forum forum)
        {
            IForum _forum = new IForum();
            _forum.description = forum.Description;
            _forum.subject = forum.Name;

            throw new NotImplementedException();
        }

        public Moderator CreateModerator(Moderator mod)
        {
            IModerator _mod = new IModerator();
            _mod.subForumId = mod.SubForum.Id;
            _mod.term = mod.TermExp;
            // למה מנחה צריך ת.ז. אם יש לו משתמש..?

            db.SubmitChanges();
            throw new NotImplementedException();
        }

        public Subforum CreateSubForum(Subforum sf)
        {
            ISubForum _subf = new ISubForum();
            // אין מצביע לפורום...
            // _subf.forumId = sf.
            _subf.subject = sf.Name;
            _subf.description = sf.Description;

            db.SubmitChanges();
            throw new NotImplementedException();
        }

        public User CreateUser(User user)
        {
            IUser _user = new IUser();
            _user.userName = user.Username;
            _user.name = user.Name;
            _user.password = user.Password;
            _user.email = user.Email;
            
            db.SubmitChanges();
            throw new NotImplementedException();
        }

        public Admin[] GetAdmins(Collection<int> adminsIds)
        {

            List<Admin> admins = new List<Admin>(); 
            foreach (IAdmin iadmin in db.IAdmins)
            {
                if(adminsIds == null || adminsIds.Contains(iadmin.moderatorId))
                {
                    // לפורום אין מזהה?
                    
                    IUser iuser = iadmin.IModerator.IUser;
                    ISubForum isf = iadmin.IModerator.ISubForum;
               
                    User user = new User(iuser.id, iuser.name, iuser.userName, iuser.email, iuser.password);
                    Subforum sf = new Subforum(isf.id, isf.subject, isf.description, this);
                    Forum forum = new Forum(iadmin.IForum.subject, iadmin.IForum.description, null);

                    // מה אני נותן כאשר בתור התז
                    Moderator moderator = new Moderator(user, iadmin.IModerator.term, sf, null, -1, this);


                    Admin admin = new Admin(moderator, forum, this);
                    moderator.Appointer = admin;
                    admins.Add(admin);
                }       
            }
            return admins.ToArray();
            
        }

        public Forum[] GetForums(Collection<int> forumsIds)
        {

            List<Forum> forums = new List<Forum>();
            foreach (IForum iforum in db.IForums)
            {
                if (forumsIds == null || forumsIds.Contains(iforum.id))
                {
                    Forum forum = new Forum(iforum.subject, iforum.description, null);
                    forums.Add(forum);
                }
            }
            return forums.ToArray();
        }

        public Moderator[] GetModerators(Collection<int> moderatorIds, Forum forum)
        {

            List<Moderator> moderators = new List<Moderator>();
            foreach (IModerator imoderator in db.IModerators)
            {
                if (imoderator.ISubForum.forumId == forum.Id
                    && (moderatorIds == null || moderatorIds.Contains(imoderator.userId)) )
                {
                    IUser iuser = imoderator.IUser;
                    ISubForum isf = imoderator.ISubForum;

                    User user = new User(iuser.id, iuser.name, iuser.userName, iuser.email, iuser.password);
                    Subforum sf = new Subforum(isf.id, isf.subject, isf.description, this);

                    // מה אני נותן בתור מזהה?
                    Moderator moderator = new Moderator(user, imoderator.term, sf, null, -1, this);

                    moderators.Add(moderator);
                }
            }
            return moderators.ToArray();
        }


        public Subforum[] GetSubForums(Collection<int> subForumIds, Forum forum)
        {

            List<Subforum> subforums = new List<Subforum>();
            foreach (ISubForum isf in db.ISubForums)
            {
                if (isf.forumId == forum.Id &&
                    (subForumIds == null || subForumIds.Contains(isf.forumId)))
                {
                    Subforum sf = new Subforum(isf.id, isf.subject, isf.description, this);
                    subforums.Add(sf);
                }
            }
            return subforums.ToArray();
        }

        public User[] GetUseres(Collection<int> userIds, Forum forum)
        {
            List<User> users = new List<User>();
            foreach (IUser iuser in db.IUsers)
            {
                if (userIds == null || userIds.Contains(iuser.id))
                {
                    
                    User user = new User(iuser.id, iuser.name, iuser.userName, iuser.email, iuser.password);
                    users.Add(user);
                }
            }
            return users.ToArray();
        }


        // למה המזהה שווה ל -1
        public Admin UpdateAdmin(Admin admin)
        {
            foreach (IAdmin iadmin in db.IAdmins)
                if (iadmin.moderatorId == admin.InnerMod.Id)
                {
                    iadmin.forumId = admin.Forum.Id;
                    iadmin.moderatorId = admin.InnerMod.Id;
                }

                    // למה צריך להחזיר אובייקט?
            db.SubmitChanges();
            return admin;
        }

        public Forum UpdateForum(Forum forum)
        {
            foreach (IForum iforum in db.IForums)
                if (iforum.id == forum.Id)
                {
                    iforum.description = forum.Description;
                    iforum.subject = forum.Name;
                }

            // למה צריך להחזיר אובייקט?
            db.SubmitChanges();
            return forum;
        }

        public Moderator updateModerator(Moderator mod)
        {
            foreach (IModerator imod in db.IModerators)
                if (imod.userId == mod.user.Id)
                {
                    imod.subForumId = mod.SubForum.Id;
                    imod.term = mod.TermExp;
                }

            // למה צריך להחזיר אובייקט?
            db.SubmitChanges();
            return mod;
        }

        public Subforum UpdateSubForum(Subforum sf)
        {
            foreach (ISubForum isf in db.ISubForums)
                if (isf.id == sf.Id)
                {
                    isf.description = sf.Description;
                    //isf.forumId = sf.forum
                    isf.subject = sf.Name;
                }

            // למה צריך להחזיר אובייקט?
            db.SubmitChanges();
            return sf;
        }

        public User updateUser(User user)
        {
            foreach (IUser iuser in db.IUsers)
                if (iuser.id == user.Id)
                {
                    iuser.userName = user.Username;
                    iuser.name = user.Name;
                    iuser.email = user.Email;
                    iuser.password = user.Password;
                    // nofitication??
                }

            // למה צריך להחזיר אובייקט?
            db.SubmitChanges();
            return sf;
        }
    }
}
