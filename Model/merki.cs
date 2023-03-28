using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atali_len.Model
{
    public class merki : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public int ID_client { get; set; }
        public string Date { get; set; }
        public string? ot { get; set; }
        public string? ob { get; set; }
        public string? vb { get; set; }
        public string? vs { get; set; }
        public string? og { get; set; }
        public string? opg { get; set; }
        public string? gd { get; set; }
        public string? gch { get; set; }



        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
