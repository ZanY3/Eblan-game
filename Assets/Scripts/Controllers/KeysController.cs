using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeysController : MonoBehaviour
{
    public TMP_Text countText;

    public int count;
    
    public void TakeKey()
    {
        count++;
        countText.text = "������: " + count.ToString() + "/10";
    }
}
