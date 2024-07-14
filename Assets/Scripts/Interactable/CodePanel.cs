using TMPro;
using UnityEngine;

public class CodePanel : MonoBehaviour
{
    public GameObject codePanelObj;
    public GameObject uiCanvas;
    public string code;
    public TMP_Text inputText;

    [Header("Door")]

    public Animator doorAnimator;
    public AudioSource doorSource;
    public AudioClip doorOpenClip;
    
    private FirstPersonLook playerCamera;
    private string inputCode;
    private bool usable;
    private bool opened = false;

    private void Start()
    {
        playerCamera = FindAnyObjectByType<FirstPersonLook>();
        inputText.text = null;
    }
    private void Update()
    {
        if(usable && !opened && Input.GetKeyDown(KeyCode.E))
        {
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = CursorLockMode.None;
            playerCamera.canFollow = false;
            codePanelObj.SetActive(true);
            uiCanvas.SetActive(false);
            Time.timeScale = 0;
        }
    }
    public void GetNumber(int number)
    {
        print(number);
        inputCode += number.ToString();
        inputText.text = inputCode;
    }
    public void EnterCode()
    {
        if (inputCode == code)
        {
            Time.timeScale = 1f;
            opened = true;
            doorAnimator.SetTrigger("Open");
            doorSource.PlayOneShot(doorOpenClip);
            Debug.Log("Correct");
            codePanelObj.SetActive(false);
            uiCanvas.SetActive(true);
            playerCamera.canFollow = true;
        }
        else
        {
            inputCode = null;
            inputText.text = null;
            Debug.Log("Incorrect");
        }

    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
