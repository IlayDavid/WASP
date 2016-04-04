using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASP.Domain
{
    interface IBL
    {
        SuperUser initialize();
        Post getThread(Member member, int threadId);
        Forum getForum(int userId, int forumId);
        Subforum getSubforum(Member member, int subforumId);
        Post createThread(Member author, String title, String content,
            DateTime now, Post inReplyTo, Subforum container, DateTime editAt);

        int createForum(string userName, Forum forum);
        List<Member> getModerators(Member member, Subforum subforum);
        DateTime getModeratorTermTime(Member member, Member moderator, Subforum subforum);
        Subforum createSubForum(Member member, String name, String description);
        List<Forum> getAllForums();
        Post createReplyPost(Member author, String title, String content,
            DateTime now, Post inReplyTo, Subforum container, DateTime editAt);
        int updateModeratorTerm(Member member, Member moderator, Subforum subforum, DateTime term);
        int updateForum(int userId, int forumId);
        int defineForumPolicy(int userId, Forum forum);  //------------------------ policy object??
        Member subscribeToForum(String userName, String name, String email, String pass);
        int sendMessage(Member member, Member targetMember, Message message);
        int addModerator(Member member, Member moderator, Subforum subforum, DateTime term);
        int confirmEmail(Member member);
        int deletePost(Member member, Post post);
        Member login(string userName, string password);
        List<Member> getAdmins(int forumId);
        List<Member> getMembers(Member member, Forum forum);
        List<Subforum> getSubforums(Member member);
    }
}
