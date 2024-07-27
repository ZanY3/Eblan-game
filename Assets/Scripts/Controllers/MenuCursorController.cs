using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCursorController : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
