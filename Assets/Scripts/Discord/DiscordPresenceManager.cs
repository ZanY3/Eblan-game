using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Discord;

public class DiscordPresenceManager : MonoBehaviour
{
    [Header("Настройки приложения Discord")]
    public long applicationId = 123456789012345678; // Замените на ваш Discord Application ID

    [Header("Основной статус")]
    public string state = "В меню";
    public string details = "Проходит 1-й уровень";

    [Header("Время активности")]
    public bool useTime = true;
    
    [Header("Изображения")]
    public string largeImageKey = "main_icon";
    public string largeImageText = "Название игры";
    public string smallImageKey = "small_icon";
    public string smallImageText = "Заряжено на победу";

    private Discord.Discord discord;
    private ActivityManager activityManager;

    private bool isInitialized = false;

    void Start()
    {
        Debug.Log("Попытка инициализации Discord SDK...");

        try
        {
            // Попытка создать объект Discord SDK
            discord = new Discord.Discord(applicationId, (UInt64)Discord.CreateFlags.Default);
            Debug.Log("Объект Discord SDK создан успешно.");

            if (discord != null)
            {
                // Попытка получить Activity Manager
                activityManager = discord.GetActivityManager();
                Debug.Log("Activity Manager получен успешно.");

                isInitialized = activityManager != null;
                if (isInitialized)
                {
                    Debug.Log("Discord SDK успешно инициализирован.");
                    UpdatePresence(); // Обновляем статус после инициализации
                }
                else
                {
                    Debug.LogError("Activity Manager не был инициализирован.");
                }
            }
            else
            {
                Debug.LogError("Discord SDK объект не был создан.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Не удалось инициализировать Discord SDK: {ex.Message}");
        }
    }

    void UpdatePresence()
    {
        if (!isInitialized)
        {
            Debug.LogWarning("Discord SDK не был инициализирован. UpdatePresence() отменен.");
            return;
        }

        var activity = new Discord.Activity
        {
            State = state,
            Details = details,
            Assets =
            {
                LargeImage = largeImageKey,
                LargeText = largeImageText,
                SmallImage = smallImageKey,
                SmallText = smallImageText
            }
        };

        if (useTime)
        {
            activity.Timestamps = new Discord.ActivityTimestamps
            {
                Start = DateTimeOffset.Now.ToUnixTimeMilliseconds()
            };
        }

        activityManager.UpdateActivity(activity, result =>
        {
            if (result == Discord.Result.Ok)
                Debug.Log("Rich Presence успешно обновлен!");
            else
                Debug.LogError("Ошибка обновления Rich Presence.");
        });
    }

    void Update()
    {
        if (isInitialized && discord != null)
        {
            discord.RunCallbacks();
        }
        else
        {
            Debug.LogWarning("Discord SDK не инициализирован.");
        }
    }

    void OnApplicationQuit()
    {
        if (discord != null)
        {
            discord.Dispose();
            Debug.Log("Discord SDK завершил работу.");
        }
    }
}

