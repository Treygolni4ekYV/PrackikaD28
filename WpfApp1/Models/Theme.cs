namespace WpfApp1.Models
{
    internal class Theme
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public Theme(string title, string content) 
        {
            this.Title = title;
            this.Content = content;
        }
    }
}
