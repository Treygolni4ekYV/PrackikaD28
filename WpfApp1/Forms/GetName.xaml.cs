using System.Windows;
using WpfApp1.Models;

namespace WpfApp1.Forms
{
    public partial class GetName : Window
    {
        public GetName()
        {
            InitializeComponent();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            Apendix.Name = Name_TextBox.Text;
            this.Close();
        }
    }
}
