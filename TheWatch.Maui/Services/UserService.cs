using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWatch.Maui.Services
{
    public class UserService
    {
        private static UserService _instance;

        public static UserService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserService();
                }
                return _instance;
            }
        }

        public string GpsLocation { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string EmailAddress { get; set; }
        public string JWT { get; set; }

        private UserService() { }
    }
}