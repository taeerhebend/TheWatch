using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWatch.Maui.Data
{
    public class SettingsModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Notifications { get; set; }
        public bool LocationEnabled { get; set; }
        public bool LocationTracking { get; set; }
        public bool SMSEnabled { get; set; }
    }
}
