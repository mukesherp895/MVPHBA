using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPHBA.Model.DBModels
{
    public class Users : IdentityUser
    {
        public string UserType { get; set; }
        public string FullName { get; set; }
    }
}
