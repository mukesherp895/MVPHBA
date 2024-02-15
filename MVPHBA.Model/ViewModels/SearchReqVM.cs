using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPHBA.Model.ViewModels
{
    public class SearchVM
    {
        public int DisplayStart { get; set; }
        public int DisplayLength { get; set; }
        public string SortDir { get; set; }
        public int SortCol { get; set; }
        public string Location { get; set; }
        public int Price { get; set; }
        public string PropertyType { get; set; }
    }
}
