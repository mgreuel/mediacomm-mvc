namespace MediaCommMvc.Web.Models.Forum.Commands
{
    public class UpdatePostCommand
    {
        public int PostId { get; set; }

        public string Text { get; set; }
    }
}