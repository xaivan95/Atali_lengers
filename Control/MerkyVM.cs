using Atali_len.Control.Base;
using Atali_len.Model;
using Atali_len.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Atali_len.Control
{
    public class MerkyVM : Base.ViewModel
    {
        private merki selectMerki= new merki();
        private int clientID = 0;
        //private ObservableCollection<merki> merkis = new ObservableCollection<merki>();
        private ObservableCollection<merki> merkisAll = MainWindowViewControl.db.Merkis.Local.ToObservableCollection();

        private ObservableCollection<Model.Client> clients = new ObservableCollection<Model.Client>();
        public ObservableCollection<Model.Client> Clients { get { return clients; } set { clients = value; OnPropertyChanged(); } }
        public merki SelectMerki { get { return selectMerki; } set { selectMerki = value; OnPropertyChanged(); } }
        public ObservableCollection<merki> Merkis { get { return new ObservableCollection<merki>(merkisAll.Where(x => x.ID_client == ClientID).ToList()); } set { OnPropertyChanged(); } }
        public int ClientID { get { return clientID; } set { clientID = value; OnPropertyChanged("Merkis"); } }
        public MerkyVM()
        {
            Clients = MainWindowViewControl.db.Clients.Local.ToObservableCollection();
            if (Clients.Count>0)
            ClientID = Clients[0].ID;
            merkisAll = MainWindowViewControl.db.Merkis.Local.ToObservableCollection();
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    MainWindowViewControl.db.Merkis.Remove(selectMerki);
                    MainWindowViewControl.db.SaveChanges();
                    Merkis = new ObservableCollection<merki>();
                });
            }
        }

        public ICommand AddCommand
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    AddMerki newMerki = new AddMerki(this);
                    newMerki.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                    newMerki.ShowDialog();

                });
            }
        }

        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    if (selectMerki != null)
                    {
                        AddMerki newMerki = new AddMerki(this,selectMerki);
                        newMerki.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                        newMerki.ShowDialog();
                    }
                    else
                        MessageBox.Show("Выберите мерку");

                });
            }
        }

        public void AddNewMerki(merki merk)
        {
            merk.ID_client = clientID;
            merk.Date = DateTime.Now.Date.ToShortDateString();
            MainWindowViewControl.db.Merkis.Add(merk);
            MainWindowViewControl.db.SaveChanges();
            Merkis = new ObservableCollection<merki>();
        }

        public void EditMerki(merki merk)
        {
            var result = MainWindowViewControl.db.Merkis.SingleOrDefault(b => b.ID == SelectMerki.ID);
            if (result != null)
            {
                result.vb = merk.vb;
                result.ob = merk.ob;
                result.ot = merk.ot;
                result.vs = merk.vs;

                result.og = merk.og;
                result.opg = merk.opg;
                result.gd = merk.gd;
                result.gch = merk.gch;
                MainWindowViewControl.db.SaveChanges();
            }
            Merkis = new ObservableCollection<merki>();

        }
    }
}
