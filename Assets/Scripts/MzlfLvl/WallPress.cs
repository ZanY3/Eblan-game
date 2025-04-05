using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WallPress : MonoBehaviour
{
    public float PressCd;
    public float AnimPlayTime;
    public AudioClip PressSound;

    private MzlfLvlController _lvlController;
    private float _startPressCd;
    private Animator _anim;
    private bool _isActive = false;
    private AudioSource _source;

    private void Start()
    {
        _startPressCd = PressCd;
        _lvlController = FindAnyObjectByType<MzlfLvlController>();
        _anim = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
    }
    private async void Update()
    {
        PressCd -= Time.deltaTime;
        if(PressCd <= 0 && !_isActive)
        {
            _source.PlayOneShot(PressSound);
            _isActive = true;
            _anim.SetTrigger("Hit");
            await new WaitForSeconds(AnimPlayTime);
            _isActive = false;
            PressCd = _startPressCd;

        }
    }
    public void OnChildCollisionEnter()
    {
        if (_isActive)
        {
            _lvlController.BoxDestroy();
        }
    }

}
