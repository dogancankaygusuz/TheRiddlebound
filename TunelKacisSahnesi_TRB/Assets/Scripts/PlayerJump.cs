using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f; // zýplama hýzý
    [SerializeField] private AudioClip jumpSound; // ziplama sesi

    private AudioSource audioSource;
    private Rigidbody rb;
    private Animator animator; 
    private bool isGrounded; // zemin kontrolu
    private int jumpCount = 0; // Ziplama sayisi
    private int maxJumpCount = 2; // Maksimum zýplama sayýsý

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Zemin kontrolu
        isGrounded = Mathf.Abs(rb.velocity.y) < 0.01f;

        // Eger karakter yerdeyse, ziplama sayisini sifirla
        if (isGrounded)
        {
            jumpCount = 0;
        }

        // Space basilinca ziplama hakki varsa zipla
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            Jump();
            jumpCount++;
        }

        // Animasyonlari calistir
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("Jump", !isGrounded);
    }

    // Ziplama metotu
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);

        if (jumpSound != null && audioSource != null)
        {
            StartCoroutine(PlayJumpSoundForDuration(1f));
        }
    }

    // Ziplama sesinin 1 saniye calmasini saglama metotu
    private IEnumerator PlayJumpSoundForDuration(float duration)
    {
        audioSource.clip = jumpSound;
        audioSource.Play();
        yield return new WaitForSeconds(duration);
        audioSource.Stop();
    }
}
