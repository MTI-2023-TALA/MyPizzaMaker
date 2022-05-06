using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontPizza
{
    public class Config
    {
        public static string Base = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2" : "https://localhost";
        public static string BaseWeb = $"{Base}:7028/";
    }
}
