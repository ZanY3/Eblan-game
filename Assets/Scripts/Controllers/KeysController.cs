using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeysController : MonoBehaviour
{
    public TMP_Text countText;

    private int count;
    
    public void TakeKey()
    {
        count++;
        countText.text = "Ключей: " + count.ToString() + "/10";
    }
}
