using Atali_len.Control;
using Atali_len.Model;
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
    /// Логика взаимодействия для AddZakaz.xaml
    /// </summary>
    public partial class AddZakaz : Window
    {
        ZakazVM vm;
        public AddZakaz(ZakazVM zakaz)
        {
            InitializeComponent();
            vm = zakaz;
            DataContext = vm;
        }

        public AddZakaz(ZakazVM zakaz, Model.Zakaz zak)
        {
            InitializeComponent();
            vm = zakaz;
            DataContext = vm;
            vm.ClientID = zak.ID_client;
            vm.MerkiID = zak.ID_merki;
            tb3.Text = zak.name;
            tb4.Text = zak.date;
            tb5.Text = zak.count.ToString();
            tb6.Text = zak.count_pred.ToString();
            tb7.Text = zak.doing;
            tb8.Text = zak.send;
            bt.Content = "Изменить";

        }

        private void bt_Click(object sender, RoutedEventArgs e)
        {
            var zakaz = new Model.Zakaz();
            zakaz.ID_client = (int)tb1.SelectedValue;
            zakaz.ID_merki = (int)tb2.SelectedValue;
            zakaz.name = tb3.Text;
            zakaz.date = tb4.Text;
            zakaz.count = int.Parse(tb5.Text);
            zakaz.count_pred = int.Parse(tb6.Text);
            zakaz.doing = tb7.Text;
            zakaz.send = tb8.Text;
            if (bt.Content.Equals("Добавить"))
                vm.AddZakaz(zakaz);
            else
                vm.EditZakaz(zakaz);
            this.Close();
        }
    }
}
