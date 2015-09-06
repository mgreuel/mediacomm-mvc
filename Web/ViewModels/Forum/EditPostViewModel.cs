using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using MediaCommMvc.Web.Models.Forum.Commands;
using MediaCommMvc.Web.Models.Forum.Models;

namespace MediaCommMvc.Web.ViewModels.Forum
{
    public class EditPostViewModel
    {
        public EditPostViewModel()
        {
        }

        public EditPostViewModel(Post post)
        {
            this.Text = post.Text;
            this.PostId = post.Id;
        }

        public int PostId { get; set; }

        [Required]
        [AllowHtml]
        public string Text { get; set; }

        public UpdatePostCommand ToSavePostCommand()
        {
            return new UpdatePostCommand { PostId = this.PostId, Text = this.Text };
        }
    }
}