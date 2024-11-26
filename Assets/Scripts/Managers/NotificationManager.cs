using Unity.Notifications.Android;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {

        //Remove notifications that have been displayed.
        AndroidNotificationCenter.CancelAllDisplayedNotifications();

        //Create Android notification channel
        var c = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Reminder Channel",
            Importance = Importance.High,
            Description = "Reminder notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);

        //Set up notification
        var notification = new AndroidNotification();
        notification.Title = "Hey! Comeback!";
        notification.Text = "These papers wont deliver themselves!";
        notification.FireTime = System.DateTime.Now.AddSeconds(10);

        //Send Notification
        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        //If the script is triggered, cancel scheduled notification and send new notification
        if(AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
