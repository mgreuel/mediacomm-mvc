using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediaCommMvc.Web.Models.Forum;

namespace MediaCommMvc.Web.ViewModels.Forum
{
    public class TopicOverviewViewModel
    {
        public IEnumerable<string> ExcludedUsernames { get; set; }

        public string CreatedBy { get; set; }

        public string PostCount { get; set; }

        public string LastPostTime { get; set; }

        public string LastPostAuthor { get; set; }

        public string Title { get; set; }

        public bool ReadByCurrentUser { get; set; }

        public TopicDisplayPriority DisplayPriority { get; set; }

        public int Id { get; set; }
    }
}
