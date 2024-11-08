using TMPro;
using UnityEngine;

public class CodePanel : MonoBehaviour
{
    public GameObject codePanelObj;
    public GameObject uiCanvas;
    public TMP_Text inputText;
    public TMP_Text codePaperText;

    [Header("Door sounds")]
    public Animator doorAnimator;
    public AudioSource doorSource;
    public AudioClip doorOpenClip;
    [Header("Code door Sounds")]
    public AudioClip typeSound;
    public AudioClip errorSound;

    private Pause pause;
    private string code;
    private AudioSource source;
    private FirstPersonLook playerCamera;
    private string inputCode;
    private bool usable;
    private bool inPanel = false;
    private bool opened = false;


    private void Start()
    {
        code = GenerateRandomCode(4);
        codePaperText.text = code;
        Debug.Log(code);
        source = GetComponent<AudioSource>();
        playerCamera = FindAnyObjectByType<FirstPersonLook>();
        inputText.text = null;
        pause = FindAnyObjectByType<Pause>();
    }
    private void Update()
    {
        if(usable && !opened && Input.GetKeyDown(KeyCode.E))
        {
            pause.canPause = false;
            inPanel = true;
            Cursor.visible = Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerCamera.canFollow = false;
            codePanelObj.SetActive(true);
            uiCanvas.SetActive(false);
            Time.timeScale = 0;
        }
        if(inPanel && Input.GetKeyDown(KeyCode.Backspace))
        {
            pause.canPause = true;
            inPanel = false;
            Time.timeScale = 1f;
            Cursor.visible = Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            codePanelObj.SetActive(false);
            uiCanvas.SetActive(true);
            playerCamera.canFollow = true;
        }
    }
    string GenerateRandomCode(int length)
    {
        string result = "";
        for (int i = 0; i < length; i++)
        {
            result += Random.Range(0, 10).ToString();
        }
        return result;
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
            pause.canPause = true;
            Time.timeScale = 1f;
            opened = true;
            Cursor.visible = Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
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
