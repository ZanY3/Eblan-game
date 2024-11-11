using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CatchMiniGame : MonoBehaviour
{
    public int catchLeft;

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
        if(catchLeft <= 0)
        {
            keyController.PassElectroPanel();
            electroPanel.EndMiniGame();
        }
    }
}
