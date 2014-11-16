using System.ComponentModel.DataAnnotations;

using Core.Forum.Commands;
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

        public UpdatePostCommand ToSavePostCommand()
        {
           return new UpdatePostCommand { PostId = this.PostId, Text = this.Text };
        }
    }
}