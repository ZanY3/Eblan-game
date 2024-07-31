using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public string gameDifficulty;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void SetDifficulty(string difficulty)
    {
        gameDifficulty = difficulty;
        Debug.Log(gameDifficulty);
    }
}
