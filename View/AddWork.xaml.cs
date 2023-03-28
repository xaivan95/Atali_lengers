using Atali_len.Control;
using Microsoft.Win32;
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
    /// Логика взаимодействия для AddWork.xaml
    /// </summary>
    public partial class AddWork : Window
    {
        WorkVM vm;
        Model.Work work = new Model.Work();
        public AddWork(WorkVM vm)
        {
            InitializeComponent();
            this.vm = vm;
        }

        public AddWork(WorkVM vm, Model.Work w)
        {
            InitializeComponent();
            this.vm = vm;
            
            tb2.Text = AppDomain.CurrentDomain.BaseDirectory + "Data\\image\\" +  w.foto;
            tb3.Text = AppDomain.CurrentDomain.BaseDirectory + "Data\\image\\" + w.file1;
            tb4.Text = AppDomain.CurrentDomain.BaseDirectory + "Data\\image\\" + w.file2;
            tb5.Text = AppDomain.CurrentDomain.BaseDirectory + "Data\\image\\" + w.file3;
            tb6.Text = AppDomain.CurrentDomain.BaseDirectory + "Data\\image\\" + w.file4;
            tb7.Text = AppDomain.CurrentDomain.BaseDirectory + "Data\\image\\" + w.file5;
            bt.Content = "Изменить";
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fb = new OpenFileDialog();
            fb.Filter = "jpg|*.jpg| bmp|*.bmp";
            if (fb.ShowDialog() == true)
            {
                var FilePath = fb.FileName;
                tb2.Text = FilePath; 

            }

        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fb = new OpenFileDialog();
            if (fb.ShowDialog() == true)
            {
                var FilePath = fb.FileName;
                switch ((sender as Button).Tag)
                {
                    case "2": tb3.Text = FilePath; break;
                    case "3": tb4.Text = FilePath; break;
                    case "4": tb5.Text = FilePath; break;
                    case "5": tb6.Text = FilePath; break;
                    case "6": tb7.Text = FilePath; break;
                }

            }
        }

        private void bt_Click(object sender, RoutedEventArgs e)
        {
            
            work.foto = tb2.Text;
            work.file1 = tb3.Text;
            work.file2 = tb4.Text;
            work.file3 = tb5.Text;
            work.file4 = tb6.Text;
            work.file5 = tb7.Text;

                vm.EditWork(work);
            this.Close();
        }
    }
}
