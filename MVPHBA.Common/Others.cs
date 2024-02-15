using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPHBA.Common
{
    public static class Others
    {
        public static string ConnStr
        {
            get
            {
                var config = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
                var conStr = config.GetSection("ConnectionStrings").GetSection("MVPHBAConStr").Value ?? "";
                return conStr;
            }
        }
        public static string ServerDrivePath
        {
            get
            {
                var configRoot = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
                string drivePath = configRoot.GetSection("ServerInfos").GetSection("DrivePath").Value ?? "";
                return drivePath;
            }
        }
        public static string ImageToBase64String(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(imagePath);
                return Convert.ToBase64String(imageArray);
            }
            return string.Empty;
        }
        public static bool FileExists(string path)
        {
            if (File.Exists(path))
            {
                return true;
            }
            return false;
        }
        public static string BaseUrl()
        {
            try
            {
                var config = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
                var baseUrl = config.GetSection("Urls").GetSection("BaseUrl").Value;

                return baseUrl;

            }
            catch (Exception ex)
            {

            }
            return string.Empty;
        }
        public static async Task<GetStreamDto> ToArrayAsync(string path)
        {
            GetStreamDto data = new GetStreamDto();
            try
            {
                var fi = new FileInfo(path);
                var extension = fi.Extension.Replace(".", "");
                extension = extension.Contains("jpg") ? "jpeg" : extension;
                data.MIMEType = $"image/{extension}";
                data.FileName = fi.Name;
                var fileStream = fi.Open(FileMode.Open);

                var memoryStream = new MemoryStream();

                await fileStream.CopyToAsync(memoryStream);

                data.Byte = memoryStream.ToArray();
                fileStream.Close();
            }
            catch (Exception ex)
            {
                data = new GetStreamDto();
            }
            return data;
        }
    }
}
