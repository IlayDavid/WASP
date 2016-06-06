using System.Collections.Generic;

namespace Client.DataClasses
{
    public class ModeratorReport
    {

        public List<Moderator> moderators = new List<Moderator>();
        public Dictionary<int, List<Post>> moderatorsPosts = new Dictionary<int, List<Post>>();
        public Dictionary<int, int> ModeratorInsubForum = new Dictionary<int, int>();

    }
}