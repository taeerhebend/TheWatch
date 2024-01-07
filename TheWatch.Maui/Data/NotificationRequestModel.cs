namespace TheWatch.Maui.Data;

public class NotificationRequestModel
{
    public int NotificationId { get; set; } // Unique identifier for the notification
    public string Title { get; set; } // Title of the notification
    public string Description { get; set; } // Description or body of the notification
    public string ReturningData { get; set; } // Data returned when the notification is tapped
    // Additional properties like sound, icon, schedule time can be added here
}