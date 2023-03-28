using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atali_len.Model
{
    public class Zakaz : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public int ID_merki { get; set; }
        public int ID_client { get; set; }
        public string? name { get; set; }
        public int? count { get; set; }
        public int? count_pred { get; set;  }
        public string? doing { get; set; }
        public string? send { get; set; }
        public string? date { get; set; }
        [NotMapped]
        public string? FIO { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
