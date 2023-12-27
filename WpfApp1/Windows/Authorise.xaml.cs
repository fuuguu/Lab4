using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Models;

namespace WpfApp1.Windows
{
    /// <summary>
    /// Логика взаимодействия для Authorise.xaml
    /// </summary>
    public partial class Authorise : Window
    {
        private HttpClient client;
        public Authorise()
        {
            InitializeComponent();
            client = new HttpClient();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            User user = new User { EMail = Login.Text, Password = Password.Text };
            JsonContent content = JsonContent.Create(user);
            using var response = await client.PostAsync("http://localhost:5079/login", content);
            string responseText = await response.Content.ReadAsStringAsync();
            Response? resp = JsonSerializer.Deserialize<Response>(responseText);
            if (resp != null)
            {
                MainWindow main = new MainWindow(resp, this);
                main.Show();
                this.Hide();
            }
        }
    }
}
