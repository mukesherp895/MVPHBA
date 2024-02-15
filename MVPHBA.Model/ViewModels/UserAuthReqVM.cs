using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPHBA.Model.ViewModels
{
    public class UserAuthReqVM
    {
        [Required]
        public string UserName { get; set; } = "mukesherp895@gmail.com";
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "Mukesh@895";
        public string UserType { get; set; } = "Broker";
    }
}
