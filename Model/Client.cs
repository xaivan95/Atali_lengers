using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atali_len.Model
{
    public class Client : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string? Adress { get; set; }
        public string? Adress_cdek { get; set; }
        public string? Birtday { get; set; }
        public string? email { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
