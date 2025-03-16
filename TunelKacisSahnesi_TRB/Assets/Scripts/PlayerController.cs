using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float movementRange = 1.2f;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        // Yer çekimini oyun baþlarken ekledik
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Ýleriye doðru sürekli haraket
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);

        // Saða ve Sola Hareket
        float horizontalInput = Input.GetAxis("Horizontal");
        float newX = Mathf.Clamp(transform.position.x + horizontalInput * horizontalSpeed * Time.fixedDeltaTime, -movementRange, movementRange);
        rb.MovePosition(new Vector3(newX, transform.position.y, transform.position.z));
    }

    void Update()
    {
        // Zýplama
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            isGrounded = false;
        }
    }

    // Karakterin zemine deðip-deðmeme kontrolu
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
