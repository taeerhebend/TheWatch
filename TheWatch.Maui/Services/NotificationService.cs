using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;
using TheWatch.Maui.Data;

namespace TheWatch.Maui.Services
{
    public class NotificationService
    {
        private static NotificationService _instance;

        public static NotificationService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NotificationService();
                return _instance;
            }
        }

        public async Task SendNotification(string title, string message)
        {
            var notification = new NotificationRequestModel
            {
                Title = title,
                Description = message,
                ReturningData = "/WatchCalls/WatchResponseSelection.razor" // Returning data when a notification is tapped by the user
            };

            NotificationCenter.Current.Show(notification);
        }

        public void OnNotificationTapped(NotificationResponseModel response)
        {
            if (response.ReturningData == "/WatchCalls/WatchResponseSelection.razor")
            {
                var shell = (Shell)Application.Current.MainPage;
                shell.GoToAsync("/WatchCalls/WatchResponseSelection.razor");
            }
        }
    }
}