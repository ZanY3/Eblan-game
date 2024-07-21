using UnityEngine;

public class InteractableItemClue : MonoBehaviour
{
    public Camera playerCamera;
    public LayerMask interactableLayer;
    public GameObject clueObject;
    public float maxDistance = 10f;

    void Start()
    {
        if (clueObject != null)
        {
            clueObject.SetActive(false);
        }
    }

    void Update()
    {
        if (clueObject == null)
        {
            return;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, interactableLayer))
        {
            clueObject.SetActive(true);
        }
        else
        {
            clueObject.SetActive(false);
        }
    }
}
