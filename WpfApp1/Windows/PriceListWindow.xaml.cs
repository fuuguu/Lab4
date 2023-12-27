using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
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
using System.Diagnostics;

namespace WpfApp1.Windows
{
    /// <summary>
    /// Логика взаимодействия для PriceListWindow.xaml
    /// </summary>
    public partial class PriceListWindow : Window
    {
        private HttpClient client;
        private PriceList? price;
        public PriceListWindow(string token)
        {
            InitializeComponent();
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            Task.Run(() => Load());
        }
        private async Task Load()
        {
            List<Group>? list = await client.GetFromJsonAsync<List<Group>>("http://localhost:5079/api/priceLists");
            Dispatcher.Invoke(() =>
            {
                ListPriceLists.ItemsSource = null;
                ListPriceLists.Items.Clear();
                ListPriceLists.ItemsSource = list;
            });
        }
        private async Task Save()
        {
            PriceList price = new PriceList
            {
                ApartType = ApartType.Text,
                PricePerMeter = Convert.ToDouble(PricePerMeter.Text),
                Utilities = Convert.ToDouble(Utilities.Text)
            };
            JsonContent content = JsonContent.Create(price);
            using var response = await client.PostAsync("http://localhost:5079/api/group", content);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await Save();
        }

        private void ListPriceLists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            price = ListPriceLists.SelectedItem as PriceList;
            ApartType.Text = price?.ApartType;
            PricePerMeter.Text = Convert.ToString(price?.PricePerMeter);
            Utilities.Text = Convert.ToString(price?.Utilities);
        }

        private async Task Edit()
        {
            price!.ApartType = ApartType.Text;
            price!.PricePerMeter = Convert.ToDouble(PricePerMeter.Text);
            price!.Utilities = Convert.ToDouble(Utilities.Text);
            JsonContent content = JsonContent.Create(price);
            using var response = await client.PutAsync("http://localhost:5079/api/priceLists", content);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }
        private async Task Delete()
        {
            using var response = await client.DeleteAsync("http://localhost:5079/api/priceLists/" + price?.Id);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await Edit();
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            await Delete();
        }
    }
}
