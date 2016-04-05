using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WASP.DataClasses;

namespace WASP.Domain
{
    public interface ForumIBL
    {
        Post getThread(Member member, int threadId);
        //Forum getForum(int userId, int forumId);
        Subforum getSubforum(Member member, int subforumId);
        Post createThread(Member author, String content, DateTime now);

        List<Member> getModerators(Member member, Subforum subforum);
        DateTime getModeratorTermTime(Member member, Member moderator, Subforum subforum);
        Subforum createSubForum(Member member, String name, String description);
        Post createReplyPost(Member author, String title, String content,
            DateTime now, Post inReplyTo, Subforum container);
        int updateModeratorTerm(Member member, Member moderator, Subforum subforum, DateTime term);
        Forum getForum();
        int defineForumPolicy(SuperUser member, Forum forum);  //------------------------ policy object??
        Member subscribeToForum(String userName, String name, String email, String pass);
        int sendMessage(Member member, Member targetMember, Message message);
        int addModerator(Member member, Member moderator, Subforum subforum, DateTime term);
        int confirmEmail(Member member);
        int deletePost(Member member, Post post);
        Member login(string userName, string password);
        List<Member> getAdmins(User user);
        List<Member> getMembers(Member member);
        List<Subforum> getSubforums(Member member);
    }
}
