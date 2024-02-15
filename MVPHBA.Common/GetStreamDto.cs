using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPHBA.Common
{
    public class GetStreamDto
    {
        public string MIMEType { get; set; }
        public string FileName { get; set; }
        public byte[] Byte { get; set; }
    }
}
