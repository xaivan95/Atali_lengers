using Atali_len.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Atali_len.View
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
       
        ClientVM ClientVM;
        Model.Client client = new Model.Client();
        public AddClient(ClientVM clientVM)
        {
            InitializeComponent();
            ClientVM = clientVM;
        }

        public AddClient(ClientVM clientVM, Model.Client client)
        {
            InitializeComponent();
            ClientVM = clientVM;
            tb1.Text=client.Name;
            tb2.Text=client.Number;
            tb3.Text = client.email;
            tb4.Text=client.Adress;
            tb5.Text=client.Adress_cdek;
            tb6.Text=client.Birtday;
            bt.Content = "Изменить";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            client.Name = tb1.Text;
            client.Number = tb2.Text;
            client.email = tb3.Text;
            client.Adress = tb4.Text;
            client.Adress_cdek = tb5.Text;
            client.Birtday = tb6.Text;
            if (bt.Content.Equals("Добавить"))
                ClientVM.AddNewClient(client);
            else
                ClientVM.EditClient(client);
            this.Close();
        }
    }
}
