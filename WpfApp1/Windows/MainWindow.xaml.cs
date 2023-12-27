using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Models;
using WpfApp1.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpClient httpClient;
        private MainWindow mainWindow;
        private string? token;
        public MainWindow(Response response, MainWindow window)
        {
            InitializeComponent();
            this.mainWindow = window;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + response.access_token);
            token = response.access_token;
            Task.Run(() => Load());
        }
        private async Task Load()
        {
            List<Renter>? list = await httpClient.GetFromJsonAsync<List<Renter>>("http://localhost:5079/api/renters");
            foreach (Renter i in list!)
            {
                i.PriceList = await httpClient.GetFromJsonAsync<Models.PriceList>("http://localhost:5079/api/priceLists/" + i.PriceList);
            }
            Dispatcher.Invoke(() =>
            {
                ListRenters.ItemsSource = null;
                ListRenters.Items.Clear();
                ListRenters.ItemsSource = list;
            });
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.mainWindow.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PriceListWindow prWindow = new PriceListWindow(token!);
            prWindow.ShowDialog();
        }
        //добавление
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RenterWindow rWindow = new RenterWindow(token!);
            if (rWindow.ShowDialog() == true)
            {
                Renter renter = new Renter
                {
                    Name = rWindow.NameProperty,
                    FirstName = rWindow.FirstNameProperty,
                    LastName = rWindow.LastNameProperty,
                    //NumOfResidents = rWindow.DateBirthProperty,
                    //LivingSpace = rWindow.LivingSpaceProperty,
                    //ApartType = rWindow.ApartTypeProperty,
                    //CostOfLiving = rWindow.CostOfLivingProperty,
                    //RentAmount = rWindow.RentAmountProperty,
                    PriceListId = await rWindow.getIdPriceList()
                };
                JsonContent content = JsonContent.Create(renter);
                using var response = await httpClient.PostAsync("http://localhost:5079/api/renters", content);
                string responseText = await response.Content.ReadAsStringAsync();
                await Load();
            }
        }
        //изменение
        private async void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Renter? st = ListRenters.SelectedItem as Renter;
            RenterWindow rWindow = new RenterWindow(token!, st!);
            if (rWindow.ShowDialog() == true)
            {
                st!.Name = rWindow.NameProperty;
                st!.FirstName = rWindow.FirstNameProperty;
                st!.LastName = rWindow.LastNameProperty;
                //st!.BirthDay = rWindow.DateBirthProperty;
                st!.PriceListId = await rWindow.getIdPriceList();
                JsonContent content = JsonContent.Create(st);
                using var response = await httpClient.PutAsync("http://localhost:5079/api/renters", content);
                string responseText = await response.Content.ReadAsStringAsync();
                await Load();
            }
        }
        //удаление
        private async void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Renter? st = ListRenters.SelectedItem as Renter;
            JsonContent content = JsonContent.Create(st);
            using var response = await httpClient.DeleteAsync("http://localhost:5079/api/renters/" + st!.Id);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }
    }
}