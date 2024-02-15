using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPHBA.Common
{
    public static class Extensions
    {
        public static string SaveToServer(this IFormFile formFile)
        {
            string filePath = "";
            Guid fileName = Guid.NewGuid();
            string drivePath = Others.ServerDrivePath;
            if (!Directory.Exists(drivePath))
            {
                Directory.CreateDirectory(drivePath);
            }
            filePath = Path.Combine(drivePath, fileName.ToString()+ "." + formFile.FileName.Split(".")[1]);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
            return filePath;
        }
    }
}
