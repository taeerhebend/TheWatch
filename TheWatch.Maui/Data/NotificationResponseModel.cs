namespace TheWatch.Maui.Data;

public class NotificationResponseModel
{
    public int NotificationId { get; set; } // Identifier of the notification
    public string ReturningData { get; set; } // Data associated with the notification
    public NotificationAction Action { get; set; } // Action taken by the user (e.g., Tapped, Dismissed)

    public enum NotificationAction
    {
        Tapped,
        Dismissed
        // Other actions like ButtonClicked can be added
    }
}