using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoController : MonoBehaviour
{
    public float timeBfrStart;
    public string loadSceneName;
    public bool canSkip = true;

    private bool usedLoad = false;
    private ScreenLoader screenLoader;

    private void Start()
    {
        screenLoader = FindAnyObjectByType<ScreenLoader>();
    }

    private void Update()
    {
        timeBfrStart -= Time.deltaTime;

        if(timeBfrStart <= 0 && !usedLoad)
        {
            screenLoader.LoadScene(loadSceneName);
            usedLoad = true;
        }

        if (canSkip && Input.GetKeyDown(KeyCode.Space) && !usedLoad)
        {
            screenLoader.LoadScene(loadSceneName);
            usedLoad = true;
        }
    }
}
