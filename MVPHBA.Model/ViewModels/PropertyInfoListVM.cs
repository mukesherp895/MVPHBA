using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPHBA.Model.ViewModels
{
    public class PropertyInfoListVM
    {
        public long RowNum { get; set; }
        public int RecCount { get; set; }
        public int Id { get; set; }
        public string PropertyType { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string Feature { get; set; }
    }
}
