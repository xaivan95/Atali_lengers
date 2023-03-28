using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atali_len.Model
{
    public class Work : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int? ID_zakaz { get; set; }
        public string? foto { get; set; }    
        public string? file1 { get; set; }
        public string? file2 { get; set; }
        public string? file3 { get; set; }
        public string? file4 { get; set; }
        public string? file5 { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
