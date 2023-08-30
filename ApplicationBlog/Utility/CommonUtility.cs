using ApplicationBlog.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net;

namespace ApplicationBlog.Utility
{
    public class CommonUtility
    {
        public static string GetFileExtension(string base64String)
        {
            var data = base64String.Substring(0, 5);
            switch (data.ToUpper())
            {
                case "IVBOR":
                    return "png";
                case "/9J/4":
                    return "jpg";
                case "AAAAF":
                    return "mp4";
                case "JVBER":
                    return "pdf";
                case "AAABA":
                    return "ico";
                case "UMFYI":
                    return "rar";
                case "E1XYD":
                    return "rtf";
                case "U1PKC":
                    return "txt";
                case "MQOWM":
                case "77U/M":
                    return "srt";
                default:
                    return string.Empty;
            }
        }
        
        public string SaveMediaFile(long UserId, string MediaFileBase64, string BasePath, string RelativeFolderPath)
        {
            string RelativeFilepath = string.Empty;
            if (!string.IsNullOrEmpty(MediaFileBase64))
            {
                var files = MediaFileBase64.Split(new string[] { "," }, StringSplitOptions.None);
                if (files.Count() == 1)
                    MediaFileBase64 = files[0];
                else
                    MediaFileBase64 = files[1];

                byte[] imageBytes = Convert.FromBase64String(MediaFileBase64);
                string fileName = UserId + "_" + Guid.NewGuid() + "." + CommonUtility.GetFileExtension(MediaFileBase64);

                RelativeFilepath = RelativeFolderPath + fileName;
                File.WriteAllBytes(BasePath + RelativeFolderPath + fileName, imageBytes);
            }
            return RelativeFilepath;
        }
        public static string ConvertObjectToJSON(Object objRequest)
        {
            return JsonConvert.SerializeObject(objRequest);
        }
        public static string GetClientIPAddress()
        {
            string IPAddress = string.Empty;
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            return IPAddress;
        }
    }
}
