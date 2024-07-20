using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuMusicController : MonoBehaviour
{
    private GameObject anotherMenuMusic;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        anotherMenuMusic = GameObject.FindGameObjectWithTag("DestroySource");
        if (anotherMenuMusic != null && anotherMenuMusic != gameObject)
        {
            Destroy(anotherMenuMusic);
        }
    }
}
