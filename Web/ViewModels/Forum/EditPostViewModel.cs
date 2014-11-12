using System.ComponentModel.DataAnnotations;

using Core.Forum.Models;

namespace MediaCommMvc.Web.ViewModels.Forum
{
    public class EditPostViewModel
    {
        public EditPostViewModel(Post post)
        {
            this.Text = post.Text;
            this.PostId = post.Id;
        }

        public int PostId { get; set; }

        [Required]
        public string Text { get; set; }
    }
}