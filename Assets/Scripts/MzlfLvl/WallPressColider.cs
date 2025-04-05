using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPressColider : MonoBehaviour
{
    private WallPress parentPress;

    private void Start()
    {
        parentPress = GetComponentInParent<WallPress>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Box"))
        {
            parentPress.OnChildCollisionEnter();
        }
    }
}
