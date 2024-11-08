using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ElevatorDoor : MonoBehaviour
{
    public TMP_Text cantEnterText;
    public GameObject elevatorBg;
    public AudioClip elevatorSound;
    public AudioClip errorSound;
    public float timeBtwLoad = 2f;
    public string sceneToLoad;
    public bool isElectroPanels = false;

    [Space]
    [Header("ONLY FOR 3 LVL")]
    public GameObject lightObj;
    
    private AudioSource source;
    private bool usable = false;
    public bool canEnter = false;
    private KeysController keyController;
    private ScreenLoader screenLoader;
    private ElectroPanelKeyController elPanelKeyController;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        keyController = FindAnyObjectByType<KeysController>();
        screenLoader = FindAnyObjectByType<ScreenLoader>();
        if (isElectroPanels)
        { 
            elPanelKeyController = FindAnyObjectByType<ElectroPanelKeyController>();
        }
    }

 
    private async void Update()
    {
        if(!isElectroPanels)
        {
            if(keyController.count >= 10)
                canEnter = true;
        }
        else
        {
            if(elPanelKeyController.count >= 2)
                lightObj.SetActive(true);

            if (elPanelKeyController.count >= 6)
                canEnter = true;
        }

        if(usable && canEnter && Input.GetKeyDown(KeyCode.E))
        {
            StartLoading();
        }
        else if(usable && Input.GetKeyDown(KeyCode.E))
        {
            source.PlayOneShot(errorSound);
            cantEnterText.gameObject.SetActive(true);
            await new WaitForSeconds(1.5f);
            cantEnterText.gameObject.SetActive(false);
        }

    }

    public async void StartLoading()
    {
        source.PlayOneShot(elevatorSound);
        elevatorBg.SetActive(true);
        await new WaitForSeconds(timeBtwLoad);
        screenLoader.LoadScene(sceneToLoad);
    }

    private void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            usable = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            usable = false;
        }
    }
}
