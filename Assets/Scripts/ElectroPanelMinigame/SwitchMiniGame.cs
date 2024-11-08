using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchMiniGame : MonoBehaviour
{
    // hide in сделать потом
    public int switchesOn = 0;

    public int switchesToOn = 6;
    public AudioClip passSound;

    [Space]
    public ElectroPanel electroPanel;

    private ElectroPanelKeyController keyController;

    private void Start()
    {
        keyController = FindAnyObjectByType<ElectroPanelKeyController>();
    }

    private void Update()
    {
        if (switchesOn >= switchesToOn)
        {
            keyController.PassElectroPanel();
            electroPanel.EndMiniGame();
        }
    }
}
