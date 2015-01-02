namespace Core
{
    public class UpdateTopicCommand
    {
        public string Text { get; set; }

        public string Title { get; set; }

        public string ExcludedUsers { get; set; }

        public int Id { get; set; }
    }
}