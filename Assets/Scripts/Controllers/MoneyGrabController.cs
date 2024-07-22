using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyGrabController : MonoBehaviour
{
    public Transform grabPos;
    public Transform dropPos;
    public AudioClip grabSound;
    public AudioClip dropSound;

    private GameObject objToTake;
    private bool taked = false;
    private AudioSource source;
    private bool usable = false;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && usable && !taked)
        {
            Take();
        }
        else if (Input.GetKeyDown(KeyCode.G) && taked)
        {
            Drop();
        }
    }

    public void Take()
    {
        if (objToTake != null && !taked)
        {
            objToTake.GetComponent<Rigidbody>().isKinematic = true;
            objToTake.GetComponent<Collider>().isTrigger = true;
            source.PlayOneShot(grabSound);
            taked = true;
            objToTake.transform.parent = grabPos;
            objToTake.transform.localPosition = Vector3.zero;
            objToTake.transform.localRotation = Quaternion.identity;
        }
    }

    public void Drop()
    {
        if (objToTake != null && taked)
        {
            source.PlayOneShot(dropSound);
            objToTake.transform.parent = null;
            objToTake.transform.position = dropPos.position;

            Rigidbody rb = objToTake.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(dropPos.forward * 1f, ForceMode.Impulse);
            }
            objToTake.GetComponent<Rigidbody>().isKinematic = false;
            objToTake.GetComponent<Collider>().isTrigger = false;
            taked = false;
            objToTake = null;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.CompareTag("Money"))
        {
            objToTake = collision.gameObject;
            usable = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Money"))
        {
            objToTake = null;
            usable = false;
        }
    }
}
