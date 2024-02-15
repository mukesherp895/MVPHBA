using MVPHBA.Model.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPHBA.Model.ViewModels
{
    public class PropertyDetailVM
    {
        public long Id { get; set; }
        public string PropertyType { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public string Feature { get; set; }
        public string BrokerName { get; set; }
        public string BrokerContactNo { get; set; }
    }
}
