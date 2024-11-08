using TMPro;
using UnityEngine;

public class ElectroPanelKeyController : MonoBehaviour
{
    public int count = 0;
    public TMP_Text countText;

    private void Start()
    {
        countText.text = "Счетчиков включено: " + count + "/6";
    }

    public void PassElectroPanel()
    {
        count++;
        countText.text = "Счетчиков включено: " + count + "/6"; 
    }
}
