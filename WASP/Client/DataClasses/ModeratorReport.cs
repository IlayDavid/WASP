using System.Collections.Generic;

namespace Client.DataClasses
{
    public class ModeratorReport
    {
        public List<Moderator> moderators { get; set; }
        public Dictionary<int, List<Post>> moderatorsMessages { get; set; }
        public Dictionary<int, int> ModeratorInsubForum { get; set; }

    }
}