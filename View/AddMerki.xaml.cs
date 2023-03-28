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
    /// Логика взаимодействия для AddMerki.xaml
    /// </summary>
    public partial class AddMerki : Window
    {
        public AddMerki(MerkyVM merkyVM)
        {
            InitializeComponent();
            MerkyVM = merkyVM;
        }

        MerkyVM MerkyVM;
        Model.merki mer = new Model.merki();

        public AddMerki(MerkyVM merkyVM, Model.merki mer)
        {
            InitializeComponent();
            MerkyVM = merkyVM;
            tb1.Text = mer.ot;
            tb2.Text = mer.ob;
            tb3.Text = mer.vb;
            tb4.Text = mer.vs;
            tb5.Text = mer.og;
            tb6.Text = mer.opg;
            tb7.Text = mer.gd;
            tb8.Text = mer.gch;
            bt.Content = "Изменить";
        }

        private void bt_Click(object sender, RoutedEventArgs e)
        {
            mer.ot = tb1.Text;
            mer.ob = tb2.Text;
            mer.vb = tb3.Text;
            mer.vs = tb4.Text;
            mer.og = tb5.Text;
            mer.opg = tb6.Text;
            mer.gd = tb7.Text;
            mer.gch = tb8.Text;
            if (bt.Content.Equals("Добавить"))
                MerkyVM.AddNewMerki(mer);
            else
                MerkyVM.EditMerki(mer);
            this.Close();
        }
    }
}
