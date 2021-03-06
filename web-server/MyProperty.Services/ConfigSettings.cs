using System;
using System.Collections.Generic;
using System.Text;

namespace MyProperty.Services
{
    public class ConfigSettings
    {
        public AppSettings AppSettings { get; set; }
    }

    public class AppSettings
    {
        public string Secret { get; set; }
    }
}
