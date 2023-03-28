using Atali_len.Control.Base;
using Atali_len.Model;
using Atali_len.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Work = Atali_len.Model.Work;
using Zakaz = Atali_len.Model.Zakaz;

namespace Atali_len.Control
{
    public class ZakazVM : Base.ViewModel
    {

        private ObservableCollection<Model.Client> clients = new ObservableCollection<Model.Client>();
        public ObservableCollection<Model.Client> Clients { get { return clients; } set { clients = value; OnPropertyChanged(); } }

        private int clientID = 0;
        private int merkiID = 0;
        public int MerkiID { get { return merkiID; } set { merkiID = value; OnPropertyChanged(); } }
        public int ClientID { get { return clientID; } set { clientID = value; OnPropertyChanged("Merkis"); } }


        public ObservableCollection<merki> Merkis { get { return new ObservableCollection<merki>(merkisAll.Where(x => x.ID_client == ClientID).ToList()); } set { OnPropertyChanged(); } }


        private ObservableCollection<merki> merkisAll = MainWindowViewControl.db.Merkis.Local.ToObservableCollection();

        private ObservableCollection<Zakaz> zakazAll = MainWindowViewControl.db.Zakazs.Local.ToObservableCollection();

        private ObservableCollection<Model.Client> clientAll = MainWindowViewControl.db.Clients.Local.ToObservableCollection();
        public ObservableCollection<Model.Zakaz> Zakazs { get {
                return new ObservableCollection<Model.Zakaz>(zakazAll.Join(clientAll,
                p => p.ID_client,
                c => c.ID,
                (p, c) => new Model.Zakaz()
                {
                    ID = p.ID,
                    ID_client = p.ID_client,
                    count = p.count,
                    count_pred = p.count_pred,
                    date = p.date,
                    doing = p.doing,
                    send = p.send,
                    FIO = c.Name,
                    ID_merki = p.ID_merki,
                    name = p.name
                }).ToList()); } set {  OnPropertyChanged(); } }

        private Zakaz selectZakaz = new Zakaz();
        public Zakaz SelectZakaz { get { return selectZakaz; } set { selectZakaz = value; OnPropertyChanged(); } }

        public ZakazVM() {
            Clients = MainWindowViewControl.db.Clients.Local.ToObservableCollection();
            Zakazs = new  ObservableCollection<Model.Zakaz>(zakazAll.Join(clientAll,
                p => p.ID_client,
                c => c.ID, 
                (p,c) => new Model.Zakaz()
                {
                    ID = p.ID,
                    ID_client = p.ID_client,
                    count = p.count,
                    count_pred = p.count_pred,
                    date = p.date,
                    doing = p.doing,
                    send = p.send,
                    FIO = c.Name,
                    ID_merki = p.ID_merki,
                    name = p.name
                }).ToList());

        }


        public ICommand AddCommand
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    AddZakaz newZakaz = new AddZakaz(this);
                    newZakaz.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                    newZakaz.ShowDialog();

                });
            }
        }

        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    AddZakaz newZakaz = new AddZakaz(this, SelectZakaz);
                    newZakaz.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                    newZakaz.ShowDialog();

                });
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    var z = MainWindowViewControl.db.Zakazs.Find(SelectZakaz.ID);
                    MainWindowViewControl.db.Zakazs.Remove(z);
                    MainWindowViewControl.db.SaveChanges();
                    Zakazs = new ObservableCollection<Zakaz>();
                    
                });
            }
        }

        public void AddZakaz(Zakaz zakaz)
        {
            MainWindowViewControl.db.Zakazs.Add(zakaz);
            MainWindowViewControl.db.SaveChanges();
            var work = new Work()
            {
                ID_zakaz = zakaz.ID
            };
            MainWindowViewControl.db.Works.Add(work);
            MainWindowViewControl.db.SaveChanges();
            Zakazs = new ObservableCollection<Zakaz>();
        }

        public void EditZakaz(Zakaz zakaz)
        {
            var result = MainWindowViewControl.db.Zakazs.SingleOrDefault(b => b.ID == SelectZakaz.ID);
            if (result != null)
            {
                result.send = zakaz.send;
                result.doing = zakaz.doing;
                result.count = zakaz.count;
                result.count_pred = zakaz.count_pred;
                result.name = zakaz.name;
                result.ID_client = zakaz.ID_client;
                result.ID_merki = zakaz.ID_merki;
                result.date = zakaz.date;
                MainWindowViewControl.db.SaveChanges();
            }

            Zakazs = new ObservableCollection<Zakaz>();
        }
    }
}
