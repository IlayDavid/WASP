using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WASP.DataClasses.Policies;

namespace AccTests.Tests.ServerSide
{
    [TestClass]
    class integrationTest
    {
        [TestMethod]
        public void integration()
        {
            var proj = Driver.getBridge();
            var supervisor = Functions.InitialSystem(proj);
            Assert.IsNotNull(supervisor);
             supervisor = proj.loginSU(supervisor.userName, supervisor.password);
            Assert.IsNotNull(supervisor);
            var forum = proj.createForum(supervisor.id, "forum" , "the "  + "th forum", 100 , "admin" ,
                    "admin" ,
                    "admin"  + "@gmail.com", "admin1234", new PasswordPolicy());
            Assert.IsNotNull(forum);
            Assert.IsTrue(proj.getForum(forum.Id).Id==forum.Id);
            var admin = proj.login("admin" , "admin1234", forum.Id);
            Assert.IsNotNull(admin);
            #region new admin
            var user =proj.subscribeToForum(101, "admin" + "2", "admin" + "2", "admin" + "2@gmail.com",
                "admin1234",
                forum.Id);
            Assert.IsNotNull(user);
            var newadmin=proj.addAdmin(100, forum.Id, 101);
            Assert.IsNotNull(newadmin);
            Assert.IsTrue(proj.getAdmins(supervisor.id,forum.Id).Count==2);
            Assert.IsTrue(proj.getAdmin(supervisor.id,forum.Id,admin.id).user.id==admin.id);
            var check = proj.sendMessage(101, forum.Id, 101.ToString(), "welcome to adminhood");
            Assert.IsTrue(check >= 0);
            #endregion new admin

            var member = proj.subscribeToForum(110 , "mod"  + "1", "mod"  + "1", "mod"  + "1@gmail.com",
                    "mod1234",
                    forum.Id);
#region new subforums
            Assert.IsNotNull(member);
            var subforum = proj.createSubForum(admin.id, forum.Id, "subforum" , "the "  + "th subforum", member.id,
                    DateTime.MaxValue);
            Assert.IsNotNull(subforum);
            Assert.IsTrue(proj.getSubforum(forum.Id,subforum.Id).Id==subforum.Id);
            Assert.IsNotNull(proj.createSubForum(admin.id, forum.Id, "subforum2", "the " + "2th subforum", member.id,
                    DateTime.MaxValue));
            Assert.IsTrue(proj.getSubforums(forum.Id).Count==2);
            Assert.IsTrue(proj.getSubforum(forum.Id,subforum.Id).Id==subforum.Id);
#endregion new subforums
            #region add/delete moderator
            member = proj.subscribeToForum(120 , "mod"  + "2", "mod"  + "2", "mod"  + "2@gmail.com",
                    "mod1234",
                    forum.Id);
            Assert.IsNotNull(member);
            var mod=proj.addModerator(admin.id, forum.Id, member.id, subforum.Id, DateTime.MaxValue);
            Assert.IsNotNull(mod);
            check=proj.updateModeratorTerm(admin.id, forum.Id, mod.user.id, subforum.Id, new DateTime(2100, 0, 0));
            Assert.IsTrue(check>=0);
            Assert.IsTrue(proj.getModeratorTermTime(admin.id,forum.Id,mod.user.id,subforum.Id).Equals(new DateTime(2100,0,0)));
            Assert.IsTrue(proj.getModerators(forum.Id,subforum.Id).Count==2);
            Assert.IsTrue(proj.deleteModerator(admin.id,forum.Id,mod.user.id,subforum.Id)>=0);
            Assert.IsTrue(proj.getModerators(forum.Id, subforum.Id).Count == 1);
            #endregion add/delete moderator
            var prevMember = member;
            var post = proj.createThread(prevMember.id, forum.Id, "title", "first message of forum", subforum.Id);
            var firstPost = post;
            Assert.IsNotNull(post);

            for (int j = 0; j < 10; j++)
                {
                    prevMember = member;
                    member = proj.subscribeToForum(130 + j , "user"  + "" + j, "user"  + "" + j,
                        "user"  + "" + j + "@gmail.com", "user1234",
                        forum.Id);
                Assert.IsNotNull(member);

                post = proj.createReplyPost(prevMember.id, forum.Id, "this is reply number " , post.id);
                Assert.IsNotNull(post);
            }
            Assert.IsTrue(proj.getMembers(supervisor.id,forum.Id).Count==(2+2+10));
            #region reports
            var reports = proj.moderatorReport(admin.id, forum.Id);
            foreach (var pairModSubforum in reports.ModeratorInsubForum)
            {
                Assert.IsTrue(pairModSubforum.Value == 110 && pairModSubforum.Key == subforum.Id);
            }
            foreach (var moderator in reports.moderators)
            {
                Assert.IsTrue(moderator.user.id == 110);
            }

            foreach (var moderatorsPost in reports.moderatorsPosts)
            {
                Assert.IsTrue(moderatorsPost.Value[0].author.id == 110);
                Assert.IsTrue(moderatorsPost.Value[0].id == firstPostId);
            }
            #endregion reports
            #region delete post
            var postUserId = post.author.id;
            var firstPostId = firstPost.id;
            Assert.IsTrue(proj.getThread(forum.Id,firstPost.id).id==firstPost.id);
            Assert.IsTrue(proj.postsByMember(admin.id,forum.Id, firstPost.author.id).Count==1);
            Assert.IsTrue(proj.subForumTotalMessages(admin.id,forum.Id,subforum.Id)==11);
            Assert.IsTrue(proj.deletePost(firstPost.author.id,forum.Id,firstPost.id)>=0);
            Assert.IsTrue(proj.postsByMember(admin.id, forum.Id, firstPost.author.id).Count == 0);
            Assert.IsTrue(proj.subForumTotalMessages(admin.id, forum.Id, subforum.Id) == 0);
            Assert.IsTrue(proj.postsByMember(admin.id, forum.Id, postUserId).Count == 0);
            Assert.IsNull(proj.getThread(forum.Id,firstPostId));
#endregion delete post

            forum = proj.createForum(supervisor.id, "forum2", "the 2" + "th forum", 100, "admin",
        "admin",
        "admin" + "@gmail.com", "admin1234", new PasswordPolicy());
        Assert.IsNotNull(forum);
        Assert.IsTrue(proj.getAllForums().Count == 2);
            Assert.IsTrue(proj.membersInDifferentForums(100).Count==2);


          
        }
    }
}
