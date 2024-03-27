using System.IO;
using System.Windows;
using WpfApp1.Models;

namespace WpfApp1.Forms
{
    public partial class AllResults : Window
    {
        private Content _contentType;
        private string? _name = null;


        public AllResults(Content contentType)
        {
            InitializeComponent();

            this._contentType = contentType;

            if (contentType == Content.oneUser)
            {
                GetName getName = new GetName();
                getName.ShowDialog();

                _name = Apendix.Name;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int maxMark = 0;
            int minMark = 5;
            int sumMark = 0;
            int usersCount = 0;

            if (!File.Exists("Users.txt"))
            {
                Users_TextBlock.Text = "Пользователей не найдено";
                return;
            }

            foreach (string line in File.ReadAllLines("Users.txt"))
            {
                string[] data = line.Split("&");

                if (!string.IsNullOrEmpty(_name)) 
                {
                    if (data[0] != _name)
                    {
                        continue;
                    }
                }
                Users_TextBlock.Text += $"Пользователь: {data[0]}\n"
                    + $"Дата: {data[1]}\n"
                    + $"Результат {data[2]}/{data[3]}\n" 
                    + "-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-\n\n";
                
                int temp;
                if ((temp = int.Parse(data[2].ToString())) > maxMark)
                {
                    maxMark = temp;
                }
                if (temp < minMark)
                {
                    minMark = temp;
                }

                sumMark += temp;
                usersCount++;
            }

            if (_contentType == Content.withOtherData || _contentType == Content.oneUser)
            {
                MoreInfo_TextBlock.Visibility = Visibility.Visible;
                MoreInfo_TextBlock.Text = $"Количество пользователей: {usersCount}\n"
                    + $"Макс оценка {maxMark}\n"
                    + $"Минимальная оценка {minMark}\n"
                    + $"Средняя оценка {sumMark / usersCount}";
            }
        }

        public new enum Content
        {
            withOtherData,
            justList,
            oneUser
        }
    }
}
