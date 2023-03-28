using Atali_len.Control.Base;
using Atali_len.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Atali_len.Control
{
    public class MainWindowViewControl:Base.ViewModel
    {
        public static ApplicationContext db = new ApplicationContext();
        public MainWindowViewControl() {
            HomeCommand = new RelayCommand(Home);
            ClientCommand = new RelayCommand(Clients);
            MerkyCommand = new RelayCommand(Merka);
            WorkCommand = new RelayCommand(WorksPage);
            ZakazCommand = new RelayCommand(ZakazPage);
            // Startup Page
            CurrentView = new HomeVM();
            OnLoad();

        }


        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand ClientCommand { get; set; }
        public ICommand MerkyCommand { get; set; }
        public ICommand WorkCommand { get; set; }
        public ICommand ZakazCommand { get; set; }
        private void Home(object obj) => CurrentView = new HomeVM();
        private void Clients(object obj) => CurrentView = new ClientVM();
        private void Merka(object obj) => CurrentView = new MerkyVM();
        private void WorksPage(object obj) => CurrentView = new WorkVM();
        private void ZakazPage(object obj) => CurrentView = new ZakazVM();



        private void OnLoad()
        {
            // гарантируем, что база данных создана
            db.Database.EnsureCreated();
            // загружаем данные из БД
            db.Clients.Load();
            db.Merkis.Load();
            db.Works.Load();
            db.Zakazs.Load();
        }
    }
}
