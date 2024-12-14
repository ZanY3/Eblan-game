using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement; // Для управления сценами
using Discord;

public class DiscordPresenceManager : MonoBehaviour
{
    [Header("Настройки приложения Discord")]
    public long applicationId = 123456789012345678; // Замените на ваш Discord Application ID

    [Header("Изображения по умолчанию")]
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
            discord = new Discord.Discord(applicationId, (UInt64)Discord.CreateFlags.Default);
            Debug.Log("Объект Discord SDK создан успешно.");

            if (discord != null)
            {
                activityManager = discord.GetActivityManager();
                isInitialized = activityManager != null;

                if (isInitialized)
                {
                    Debug.Log("Discord SDK успешно инициализирован.");
                    UpdatePresence(SceneManager.GetActiveScene().name); // Устанавливаем статус для текущей сцены
                    SceneManager.sceneLoaded += OnSceneLoaded; // Подписываемся на событие смены сцены
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

    void UpdatePresence(string sceneName)
    {
        if (!isInitialized)
        {
            Debug.LogWarning("Discord SDK не был инициализирован. UpdatePresence() отменен.");
            return;
        }

        // Настраиваем Rich Presence в зависимости от названия сцены
        string state = $"Уничтожает ебланов";
        string details = sceneName switch
        {
            "Trailer" => "Пробирается в подвал ебланов",
            "Menu" => "Возле подвала ебланов",
            "Level1" => "На этаже T2X2",
            "Level2" => "На этаже Дрейка",
            "Level3" => "На этаже Стинта",
            "LastFight" => "Сражается с ебланами",
            "EndTrailer" => "Победил ебланов",
            _ => "Под столом ебланов"
        };

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

        activity.Timestamps = new Discord.ActivityTimestamps
        {
            Start = DateTimeOffset.Now.ToUnixTimeMilliseconds()
        };

        activityManager.UpdateActivity(activity, result =>
        {
            if (result == Discord.Result.Ok)
                Debug.Log($"Rich Presence обновлен для сцены: {sceneName}");
            else
                Debug.LogError("Ошибка обновления Rich Presence.");
        });
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Сцена загружена: {scene.name}");
        UpdatePresence(scene.name);
    }

    void Update()
    {
        if (isInitialized && discord != null)
        {
            discord.RunCallbacks();
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