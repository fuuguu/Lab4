using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

namespace WpfApp1.Windows
{
    /// <summary>
    /// Логика взаимодействия для RenterWindow.xaml
    /// </summary>
    public partial class RenterWindow : Window
    {
        //public RenterWindow()
        //{
        //    InitializeComponent();
        //}
        private HttpClient client;
        private PriceList? priceList;
        public RenterWindow(String token)
        {
            InitializeComponent();
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            Task.Run(() => LoadPriceLists());
        }
        public RenterWindow(String token, Renter renter)
        {
            InitializeComponent();
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            Task.Run(() => LoadPriceLists());
            Name.Text = renter.Name;
            FirstName.Text = renter.FirstName;
            Lastname.Text = renter.LastName;
            //cbGroup.SelectedItem = renter.Group!.Name;
        }
        private async void LoadPriceLists()
        {
            List<PriceList>? list = await client.GetFromJsonAsync<List<PriceList>>("http://localhost:5079/api/priceLists");
            Dispatcher.Invoke(() =>
            {
                cbPriceList.ItemsSource = list?.Select(p => p.ApartType);
            });
        }
        public string? NameProperty
        {
            get { return Name.Text; }
        }
        public string? FirstNameProperty
        {
            get { return FirstName.Text; }
        }
        public string? LastNameProperty
        {
            get { return Lastname.Text; }
        }
        public async Task<int> getIdPriceList()
        {
            PriceList? price = await client.GetFromJsonAsync<PriceList>("http://localhost:5079/api/priceLists/" + cbPriceList.Text);
            return price!.Id;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

