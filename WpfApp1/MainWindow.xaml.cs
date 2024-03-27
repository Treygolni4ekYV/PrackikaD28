using System.IO;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Forms;
using WpfApp1.Models;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        List<Theme> Themes = new List<Theme>();
        List<Question> Test = new List<Question>();
        int QuestionNumber = 0;
        int SelectedAnswer = 0;
        int CorrectAnswers = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //представим, что тут считывается файл с темами
            Themes.Add(new Theme("Name", "Данное свойство определяет имя элемента управления, через которое впоследствии можно будет обращаться к данному элементу, как в коде, так и в xaml разметке."));
            Themes.Add(new Theme("FieldModifier", "Свойство FieldModifier задает модификатор доступа к объекту"));
            Themes.Add(new Theme("Visibility", "Это свойство устанавливает параметры видимости элемента и может принимать одно из трех значений:\r\n\r\nVisible - элемент виден и участвует в компоновке.\r\n\r\nCollapsed - элемент не виден и не участвует в компоновке.\r\n\r\nHidden - элемент не виден, но при этом участвует в компоновке."));

            //представим, что тут считываются вопросы
            Test.Add(new Question(
                 "Что такое элемент управления в WPF?",
                 "Это объект, который отвечает за манипуляцию данными в приложении.",
                 "Это объект, предназначенный для отображения информации и взаимодействия с пользователем.",
                 "Это класс, который обеспечивает доступ к свойствам CLR.",
                 "Это контейнер для хранения других элементов управления.",
                 2));

            Test.Add(new Question(
                "Какое свойство WPF позволяет определить, как элемент управления ведет себя в разных состояниях?",
                "Event Triggers",
                "Data Bindings",
                "Dependency Properties",
                "Visual States",
                4));

            Test.Add(new Question(
                "Как можно задать стили для элементов управления в WPF?",
                "Используя свойство 'Style'",
                "Используя только встроенные стили WPF",
                "Через наследование от базовых классов элементов управления",
                "Нельзя задавать стили в WPF",
                1));

            Test.Add(new Question(
                "Что такое панель в WPF?",
                "Это специальный элемент, позволяющий привязывать события к элементам управления.",
                "Это область в окне приложения, где размещаются элементы управления.",
                "Это класс, который обеспечивает возможность группировать элементы управления.",
                "Это контейнер для управления позиционированием и размещением элементов управления.",
                4));

            Test.Add(new Question(
                "Как можно создать пользовательский элемент управления в WPF?",
                "Наследоваться от базового класса 'UserControl' и добавить необходимый функционал.",
                "Использовать только встроенные элементы управления без возможности создания собственных.",
                "Использовать только стандартные элементы управления без возможности настройки внешнего вида.",
                "Создать новый проект WPF и автоматически будет создан пользовательский элемент управления.",
                1));


            //Добавление тест в списки
            foreach (Theme theme in Themes)
            {
                //создание и добавление кнопки в панель
                Button btn = new Button
                {
                    Content = theme.Title,
                };
                btn.Click += (sender, e) => OpenTheme(sender, e);
                ThemesContainer_Panel.Children.Add(btn);

                //добавление MenuItem
                MenuItem menuItem = new MenuItem
                {
                    Header = theme.Title,
                };
                menuItem.Click += (sender, e) => OpenTheme(sender, e);
                ThemesConteiner_MenueItem.Items.Add(menuItem);
            }
        }

        private void OpenTheme(object sender, RoutedEventArgs e)
        {
            string? buttonContent = "";

            if (sender is Button)
            {
                buttonContent = ((Button)sender).Content.ToString();
            }
            else if (sender is MenuItem)
            {
                buttonContent = ((MenuItem)sender).Header.ToString();
            }
            else
            {
                return;
            }
            //если значение null то выходим
            if (string.IsNullOrEmpty(buttonContent))
            {
                return;
            }

            Theme theme = Themes.Where(x => x.Title == buttonContent).First();
            ThemeContent_TextBlock.Text = theme.Content;

            TestContent_StackPanel.Visibility = Visibility.Hidden;
            ControlQuestionsContainer_TextBlock.Visibility = Visibility.Hidden;
            ThemeContent_TextBlock.Visibility = Visibility.Visible;
        }

        private void OpenTest(object sender, RoutedEventArgs e)
        {
            GetName getName = new GetName();
            getName.ShowDialog();

            LoadQuestion(0);

            ControlQuestionsContainer_TextBlock.Visibility = Visibility.Hidden;
            ThemeContent_TextBlock.Visibility = Visibility.Hidden;
            TestContent_StackPanel.Visibility = Visibility.Visible;

            //выключаем кнопки
            Closer_Menu.Visibility = Visibility.Visible;
            Closer_LBar.Visibility = Visibility.Visible;
        }

        

        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {


            if (SelectedAnswer == Test[QuestionNumber].CorrectOption)
            {
                CorrectAnswers++;
                MessageBox.Show("Яша");
            }

            QuestionNumber++;
            LoadQuestion(QuestionNumber);

            if (QuestionNumber >= Test.Count)
            {
                //сделать окончание теста
                MessageBox.Show($"Ваш бал {CorrectAnswers} из {Test.Count}");

                //запись в файл
                if (!File.Exists("Users.txt"))
                {
                    File.Create("Users.txt").Close();
                }

                using (StreamWriter sw = new StreamWriter("Users.txt",true))
                {
                    sw.WriteLine($"{Apendix.Name}&{DateTime.Now}&{CorrectAnswers}&{Test.Count}");
                }

                //приведение значений в исходное положение
                QuestionNumber = 0;
                SelectedAnswer = 0;
                CorrectAnswers = 0;

                //включаем кнопки
                Closer_Menu.Visibility = Visibility.Hidden;
                Closer_LBar.Visibility = Visibility.Hidden;

                TestContent_StackPanel.Visibility = Visibility.Hidden;
            }
        }

        private bool LoadQuestion(int questionNumber)
        {
            if (questionNumber < Test.Count && questionNumber >= 0)
            {
                TestQuestion_TextBlock.Text = Test[questionNumber].Title;
                Option1Content_TextBlock.Text = Test[questionNumber].Option1;
                Option2Content_TextBlock.Text = Test[questionNumber].Option2;
                Option3Content_TextBlock.Text = Test[questionNumber].Option3;
                Option4Content_TextBlock.Text = Test[questionNumber].Option4;

                return true;
            }
            else
            {
                return false;
            }
        }

        public void OtherAnswer_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton btn)
            {
                SelectedAnswer = int.Parse(btn.Name[6].ToString());
            }
        }

        public void OpenControlQuestions(object sender, RoutedEventArgs e)
        {
            ThemeContent_TextBlock.Visibility = Visibility.Hidden;
            TestContent_StackPanel.Visibility = Visibility.Hidden;
            ControlQuestionsContainer_TextBlock.Visibility = Visibility.Visible;
        }

        private void OpenAllUserList(object sender, RoutedEventArgs e)
        {
            AllResults allResults = new AllResults(AllResults.Content.justList);
            allResults.ShowDialog();
        }

        private void OpenAllUserListWithOtherData(object sender, RoutedEventArgs e)
        {
            AllResults allResults = new AllResults(AllResults.Content.withOtherData);
            allResults.ShowDialog();
        }

        private void OpenAllOneUserData(object sender, RoutedEventArgs e)
        {
            AllResults allResults = new AllResults(AllResults.Content.oneUser);
            allResults.ShowDialog();
        }

        private void OpenAboutAutor(object sender, RoutedEventArgs e)
        {
            AboutAutor aboutAutor = new AboutAutor();
            aboutAutor.ShowDialog();
        }
    }
}