using System;
using System.Collections;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Android;

public class LocalNotisManager : MonoBehaviour
{
    private const string CHANNEL_ID = "notis01";
    private const string CHANNEL_CREATED_KEY = "NotiChannels_Created";

    private void Start()
    {
        if (!PlayerPrefs.HasKey(CHANNEL_CREATED_KEY))
        {
            var group = new AndroidNotificationChannelGroup()
            {
                Id = "Main",
                Name = "Main notifications"
            };
            AndroidNotificationCenter.RegisterNotificationChannelGroup(group);

            var channel = new AndroidNotificationChannel()
            {
                Id          = CHANNEL_ID,
                Name        = "Default Channel",
                Importance  = Importance.Default,
                Description = "Generic notifications",
                Group       = "Main"
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);

            StartCoroutine(RequestPermission());

            PlayerPrefs.SetString(CHANNEL_CREATED_KEY, "y");
            PlayerPrefs.Save();
        }
    }

    private void OnApplicationPause(bool paused)
    {
        if (paused)
            ScheduleTenMinuteNotification();
    }

    private IEnumerator RequestPermission()
    {
        var request = new PermissionRequest();
        while (request.Status == PermissionStatus.RequestPending)
            yield return null;
    }

    private void ScheduleTenMinuteNotification()
    {
        AndroidNotificationCenter.CancelAllScheduledNotifications();

        var notif = new AndroidNotification()
        {
            Title    = "Ariana Noelia Chaves - Image Campus",
            Text     = "",
            FireTime = DateTime.Now.AddMinutes(10)
        };

        AndroidNotificationCenter.SendNotification(notif, CHANNEL_ID);
    }
}
