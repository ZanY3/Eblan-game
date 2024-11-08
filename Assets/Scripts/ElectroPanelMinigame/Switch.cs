using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    public Sprite onSprite;
    public Sprite offSprite;
    public AudioClip clickSound;

    private bool isOn = true;
    private AudioSource source;

    private bool canPlusSwitch = true;
    private SwitchMiniGame switchMiniGame;

    private Image image;

    private void Start()
    {
        switchMiniGame = FindAnyObjectByType<SwitchMiniGame>();
        isOn = Random.value > 0.7f; //больше 0,7 то тогда false
        source = GetComponent<AudioSource>();
        image = GetComponent<Image>();
    }
    private void Update()
    {
        if(isOn)
        {
            if (canPlusSwitch)
            {
                switchMiniGame.switchesOn++;
                canPlusSwitch = false;
            }
            canPlusSwitch = false;
            image.sprite = onSprite;
        }
        else
        {
            if (!canPlusSwitch)
            {
                switchMiniGame.switchesOn--;
                canPlusSwitch = true;
            }
            image.sprite = offSprite;
        }
    }
    public void Click()
    {
        isOn = !isOn;
        source.PlayOneShot(clickSound);
    }
}
