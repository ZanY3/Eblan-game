using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bin : MonoBehaviour
{
    public float speed;
    public AudioClip hitSound;
    public TMP_Text countText;

    private CatchMiniGame catchMiniGame;
    private Rigidbody2D rb;
    private float moveInput;
    private AudioSource source;

    private void Start()
    {
        catchMiniGame = FindAnyObjectByType<CatchMiniGame>();
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("CatchObj"))
        {
            catchMiniGame.catchLeft--;
            countText.text = "Слови " + catchMiniGame.catchLeft + " элементов электричества";
            source.PlayOneShot(hitSound);
            Destroy(collision.gameObject);
        }
    }
}
