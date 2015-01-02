using System.Collections.Generic;

namespace Core
{
    public class UpdateTopicCommand
    {
        public string Text { get; set; }

        public string Title { get; set; }

        public IEnumerable<string> ExcludedUserNames { get; set; }

        public int Id { get; set; }
    }
}