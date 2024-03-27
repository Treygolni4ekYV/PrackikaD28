namespace WpfApp1.Models
{
    internal class Question
    {
        public string Title { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public int CorrectOption { get; set; }

        public Question(
            string title, 
            string option1, 
            string option2,
            string option3,
            string option4,
            int correctOption)
        {
            this.Title = title;
            this.Option1 = option1;
            this.Option2 = option2;
            this.Option3 = option3;
            this.Option4 = option4;
            this.CorrectOption = correctOption;
        }
    }
}
