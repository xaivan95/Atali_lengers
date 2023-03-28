using Atali_len.Control.Base;
using Atali_len.Model;
using Atali_len.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Work = Atali_len.Model.Work;
using Zakaz = Atali_len.Model.Zakaz;
using System.ComponentModel;
using System.Windows;

namespace Atali_len.Control
{
    public class WorkVM : Base.ViewModel
    {

        private ObservableCollection<Model.Client> clients = new ObservableCollection<Model.Client>();
        public ObservableCollection<Model.Client> Clients { get { return clients; } set { clients = value; OnPropertyChanged(); } }


        private ObservableCollection<Zakaz> zakazALL = MainWindowViewControl.db.Zakazs.Local.ToObservableCollection();
        private ObservableCollection<merki> merkisAll = MainWindowViewControl.db.Merkis.Local.ToObservableCollection();
        private ObservableCollection<Work> worksAll = MainWindowViewControl.db.Works.Local.ToObservableCollection();

        public ObservableCollection<Zakaz> Zakazs { get { return new ObservableCollection<Zakaz>(zakazALL.Where(x => x.ID_client == ClientID).ToList()); } set { OnPropertyChanged(); } }


        private int clientID = 0;
        private int merkiID = 0;
        private int workID = 0;
        private int zakazID = 0;
        public int ClientID { get { return clientID; } set { clientID = value; OnPropertyChanged("Zakazs"); } }
        public int ZakazID { get { return zakazID; } set { zakazID = value; OnPropertyChanged("NowZakaz"); OnPropertyChanged("DisplayImagePath"); OnPropertyChanged("Works"); OnPropertyChanged("NowMerki");  OnPropertyChanged("NowWork"); } }
        public int MerkiID { get { return merkiID; } set { merkiID = value; OnPropertyChanged("Works"); } }
        public int WorkID { get { return workID; } set { workID = value;  OnPropertyChanged("DisplayImagePath");  } }


        public Model.Zakaz NowZakaz
        { get { return ZakazID != 0 ? Zakazs.Where(x => x.ID == ZakazID).ToList()[0] : new Model.Zakaz(); } set { OnPropertyChanged(); } }
        public Model.Work NowWork
        { get { return ZakazID != 0 ? Works.Where(x => x.ID_zakaz == ZakazID).ToList()[0] : new Model.Work(); } set { OnPropertyChanged(); } }
        public merki NowMerki
        { get { return NowZakaz.ID_merki != 0 ? Merkis.Where(x => x.ID == NowZakaz.ID_merki).ToList()[0] : new merki(); } set { OnPropertyChanged(); } }


        public ObservableCollection<merki> Merkis { get { return new ObservableCollection<merki>(merkisAll.Where(x => x.ID == NowZakaz.ID_merki).ToList()); } set { OnPropertyChanged(); } }
        public ObservableCollection<Work> Works { get { return new ObservableCollection<Work>(worksAll.Where(x => x.ID_zakaz == ZakazID).ToList()); } set { OnPropertyChanged(); } }


        public string DisplayImagePath
        {
            get { var s = AppDomain.CurrentDomain.BaseDirectory +"Data\\image\\" + NowWork.foto;
                 return s; }
        }

        public WorkVM()
        {
            Clients = MainWindowViewControl.db.Clients.Local.ToObservableCollection();
            if (Clients.Count>0)
            ClientID = Clients[0].ID;
            if (Zakazs.Count>0)
            ZakazID = Zakazs[0].ID;
            if (Merkis.Count>0)
            MerkiID = Merkis[0].ID;
            if (Works.Count>0)
            WorkID = Works[0].Id;
        }
        public ICommand AddCommand
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    AddWork newMerki = new AddWork(this);
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
                    AddWork newMerki = new AddWork(this, NowWork);
                    newMerki.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                    newMerki.ShowDialog();

                });
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    MainWindowViewControl.db.Works.Remove(NowWork);
                    MainWindowViewControl.db.SaveChanges();
                    Works = new ObservableCollection<Work>();
                    WorkID = 0;
                });
            }
        }

        public ICommand OpenCommand
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    //Process.Start(AppDomain.CurrentDomain.BaseDirectory + "Data\\pdf\\" + obj.ToString());
                    try
                    {
                        using (Process process = new())
                        {
                            if (obj != null)
                                if (!obj.Equals(""))
                                {
                                    process.StartInfo.UseShellExecute = true;
                                    process.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "Data\\pdf\\" + obj.ToString();
                                    process.Start();
                                }
                        }
                    }
                    catch (Win32Exception noBrowser)
                    {
                        if (noBrowser.ErrorCode == -2147467259)
                        {
                            MessageBox.Show(noBrowser.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                });
            }
        }

           public void EditWork(Work w)
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data\\image\\" + (NowWork.Id )))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Data\\image\\" + (NowWork.Id ));

            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Data\\pdf\\" + (NowWork.Id )))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Data\\pdf\\" + (NowWork.Id));
            if (!w.foto.Contains(NowWork.foto))
            {
                if (File.Exists(w.foto))
                {
                    File.Copy(w.foto, AppDomain.CurrentDomain.BaseDirectory + "Data\\image\\" + (NowWork.Id) + "\\" + Path.GetFileName(w.foto), true);
                    w.foto = (NowWork.Id) + "\\" + Path.GetFileName(w.foto);
                }
                else
                    w.foto = "";
            }
            else
                w.foto = NowWork.foto;

            if (!w.file1.Contains(NowWork.file1))
            {
                if (File.Exists(w.file1))
                {

                    File.Copy(w.file1, AppDomain.CurrentDomain.BaseDirectory + "Data\\pdf\\" + (NowWork.Id) + "\\" + Path.GetFileName(w.file1), true);
                    w.file1 = (NowWork.Id) + "\\" + Path.GetFileName(w.file1);
                }
                else
                    w.file1 = "";
            }
            else
                w.file1 = NowWork.file1;

            if (!w.file2.Contains(NowWork.file2))
            {
                if (File.Exists(w.file2))
                {

                    File.Copy(w.file2, AppDomain.CurrentDomain.BaseDirectory + "Data\\pdf\\" + (NowWork.Id) + "\\" + Path.GetFileName(w.file2), true);
                    w.file2 = (NowWork.Id) + "\\" + Path.GetFileName(w.file2);
                }
                else
                    w.file2 = "";
            }
            else
                w.file2 = NowWork.file2;

            if (!w.file3.Contains(NowWork.file3))
            {
                if (File.Exists(w.file3))
                {

                    File.Copy(w.file3, AppDomain.CurrentDomain.BaseDirectory + "Data\\pdf\\" + (NowWork.Id) + "\\" + Path.GetFileName(w.file3), true);
                    w.file3 = (NowWork.Id) + "\\" + Path.GetFileName(w.file3);
                }
                else
                    w.file3 = "";
            }
            else
                w.file3 = NowWork.file3;

            if (!w.file4.Contains(NowWork.file4))
            {
                if (File.Exists(w.file4))
                {

                    File.Copy(w.file4, AppDomain.CurrentDomain.BaseDirectory + "Data\\pdf\\" + (    NowWork.Id) + "\\" + Path.GetFileName(w.file4), true);
                    w.file4 = (NowWork.Id) + "\\" + Path.GetFileName(w.file4);
                }
                else
                    w.file4 = "";
            }
            else
                w.file4 = NowWork.file4;

            if (!w.file5.Contains(NowWork.file5))
            {
                if (File.Exists(w.file5))
                {

                    File.Copy(w.file5, AppDomain.CurrentDomain.BaseDirectory + "Data\\pdf\\" + (    NowWork.Id) + "\\" + Path.GetFileName(w.file5), true);
                    w.file5 = (NowWork.Id) + "\\" + Path.GetFileName(w.file5);
                }
                else
                    w.file5 = "";
            }
            else
                w.file5 = NowWork.file5;

            var result = MainWindowViewControl.db.Works.SingleOrDefault(b => b.Id == NowWork.Id);
            if (result != null)
            {
                result.file1 = w.file1;
                result.file2 = w.file2;
                result.file3 = w.file3;
                result.file4 = w.file4;
                result.file5 = w.file5;
                result.foto = w.foto;
                MainWindowViewControl.db.SaveChanges();
            }
            ZakazID = 0;
            ZakazID = 1;
            worksAll = MainWindowViewControl.db.Works.Local.ToObservableCollection();
            Works = new ObservableCollection<Work>();
            OnPropertyChanged("NowWork");
        }

    }
}