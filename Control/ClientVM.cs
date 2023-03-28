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
using Client = Atali_len.Model.Client;

namespace Atali_len.Control
{
   
    public class ClientVM : Base.ViewModel
    {
        private Client selectClient = new Client();
        private ObservableCollection<Client> clients = new ObservableCollection<Client>();

        public Client SelectClient { get { return selectClient; } set { selectClient = value; OnPropertyChanged(); } }
        public ObservableCollection<Client> Clients { get {  return clients; } set { clients = value; OnPropertyChanged("Clients"); } }

        public ClientVM()
        {
            Clients = MainWindowViewControl.db.Clients.Local.ToObservableCollection(); 
        }



        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    MainWindowViewControl.db.Clients.Remove(selectClient);
                    MainWindowViewControl.db.SaveChanges();
                    //Clients.Remove(selectClient);
                });
            }
        }

        public ICommand AddCommand
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    AddClient newClient = new AddClient(this);
                    newClient.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                    newClient.ShowDialog();

                });
            }
        }

        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    if (selectClient != null)
                    {
                        AddClient newClient = new AddClient(this, selectClient);
                        newClient.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                        newClient.ShowDialog();
                    }
                    else
                        MessageBox.Show("Выберите клиента");

                });
            }
        }

        public void AddNewClient(Client client)
        {
            MainWindowViewControl.db.Clients.Add(client);
            MainWindowViewControl.db.SaveChanges();
            //Clients.Add(client);
        }

        public void EditClient(Client client)
        {
            var result = MainWindowViewControl.db.Clients.SingleOrDefault(b => b.ID == SelectClient.ID);
            if (result != null)
            {
                result.Adress = client.Adress;
                result.Adress_cdek = client.Adress_cdek;
                result.Number = client.Number;
                result.Birtday = client.Birtday;
                result.Name = client.Name;
                result.email = client.email;
                MainWindowViewControl.db.SaveChanges();
            }
            Clients = MainWindowViewControl.db.Clients.Local.ToObservableCollection();

        }
    }
}
