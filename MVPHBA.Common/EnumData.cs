using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPHBA.Common
{
    public class EnumData
    {
        public enum DBAttempt
        {
            Success,
            Fail
        }
        public enum PropertyType
        {
            [Description("Land")]
            Land,
            [Description("House")]
            House,
            [Description("Building")]
            Building
        }
    }
}
