using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChattingApplication
{
    class Settings
    {
        public static string Username;
        public static string Password;
        public static string Email;
        public static bool IsVerified;
        public static bool isMuted;

        public static string GetUpdateJSON()
        {
            WebClient client = new WebClient();
            string rawJson = client.DownloadString("http://127.0.0.1:1564/api/login?email=" + ChattingApplication.Settings.Email + "&password=" + ChattingApplication.Settings.Password);
            return rawJson;
        }
    }
}
