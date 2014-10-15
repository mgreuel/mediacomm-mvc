﻿namespace MediaCommMvc.Web.ViewModels.Forum
{
    public class PostViewModel
    {
        public string AuthorName { get; set; }

        public string Created { get; set; }

        public int Id { get; set; }

        public bool IsEditable { get; set; }

        public string Text { get; set; }

        /*AuthorName.Equals(this.User.Identity.Name, StringComparison.OrdinalIgnoreCase) ||
                                 HttpContext.Current.User.IsInRole("Administrators")*/
    }
}