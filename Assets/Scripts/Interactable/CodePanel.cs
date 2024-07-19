using TMPro;
using UnityEngine;

public class CodePanel : MonoBehaviour
{
    public GameObject codePanelObj;
    public GameObject uiCanvas;
    public string code;
    public TMP_Text inputText;

    [Header("Door sounds")]
    public Animator doorAnimator;
    public AudioSource doorSource;
    public AudioClip doorOpenClip;
    [Header("Code door Sounds")]
    public AudioClip typeSound;
    public AudioClip errorSound;

    private AudioSource source;
    private FirstPersonLook playerCamera;
    private string inputCode;
    private bool usable;
    private bool inPanel = false;
    private bool opened = false;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        playerCamera = FindAnyObjectByType<FirstPersonLook>();
        inputText.text = null;
    }
    private void Update()
    {
        if(usable && !opened && Input.GetKeyDown(KeyCode.E))
        {
            inPanel = true;
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = CursorLockMode.None;
            playerCamera.canFollow = false;
            codePanelObj.SetActive(true);
            uiCanvas.SetActive(false);
            Time.timeScale = 0;
        }
        if(inPanel && Input.GetKeyDown(KeyCode.Escape))
        {
            inPanel = false;
            Time.timeScale = 1f;
            codePanelObj.SetActive(false);
            uiCanvas.SetActive(true);
            playerCamera.canFollow = true;
        }
    }
    public void GetNumber(int number)
    {
        source.PlayOneShot(typeSound);
        print(number);
        inputCode += number.ToString();
        inputText.text = inputCode;
    }
    public async void EnterCode()
    {
        if (inputCode == code)
        {
            Time.timeScale = 1f;
            opened = true;
            doorAnimator.SetTrigger("Open");
            doorSource.PlayOneShot(doorOpenClip);
            codePanelObj.SetActive(false);
            uiCanvas.SetActive(true);
            playerCamera.canFollow = true;
        }
        else 
        {
            Time.timeScale = 1f;
            inputText.text = "Incorect";
            source.PlayOneShot(errorSound);
            inputText.color = Color.red;
            await new WaitForSeconds(0.5f);
            inputText.color = Color.green;
            inputText.text = "";
            inputCode = "";
            Time.timeScale = 0f;
            
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
