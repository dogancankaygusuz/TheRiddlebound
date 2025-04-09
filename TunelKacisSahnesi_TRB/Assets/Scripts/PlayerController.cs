using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;            // Z ekseni (Ýleri hareket) hýzý
    [SerializeField] private float horizontalSpeed = 3f;   // X ekseni (Yatay hareket) hýzý
    [SerializeField] private float movementRange = 0.8f;   // Yatay hareket sýnýrý
    [SerializeField] private AudioClip collisionSound;     // Carpma sesi
    [SerializeField] private GameObject winPanel; // Bitis Panel
    [SerializeField] private float winScore = 500f; // Skor sýnýrý
    [SerializeField] private GameObject scorePanel; // Skor panel

    private Rigidbody rb;
    private AudioSource audioSource;
    private CameraShack cameraShake;
    private Animator animator;
    public Text txt;
    public float score;

    void Start()
    {
        // Oyun basladiginda kullanilan referanslarý aldýk
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        cameraShake = Camera.main.GetComponent<CameraShack>();
        animator = GetComponentInChildren<Animator>();

        // Karakterin hýzýnýn kontrolunu saglamak icin, suruklenme surtunme degerleri 
        rb.drag = 3f;
        rb.angularDrag = 3f;

        // Baslangicta panel degerleri
        scorePanel.SetActive(true);
        winPanel.SetActive(false);
    }

    void FixedUpdate()
    {
        // Yalnýzca yatay hareket ve z ekseni (ileri hareket) kontrolu
        float horizontalInput = Input.GetAxis("Horizontal");
        float newX = Mathf.Clamp(transform.position.x + horizontalInput * horizontalSpeed * Time.fixedDeltaTime, -movementRange, movementRange);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z + speed * Time.fixedDeltaTime);
    }

    void Update()
    {
        // Skor guncelleme
        score += 0.1f;
        txt.text = "SKOR: " + Mathf.FloorToInt(score).ToString();

        // Hedeflenen skora ulasildiginda panel gösterimi ve oyunu durdurma
        if (score >= winScore)
        {
            winPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        
    }

    // Karakterin engele carpma kontrolu
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) // Engel ile carpýsma
        {
            scorePanel.SetActive(false);
            if (collisionSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(collisionSound);
            }
            if (cameraShake != null)
            {
                cameraShake.StartShake(10f, 0.3f);
            }
            Time.timeScale = 0f;
        }
        cameraShake.StopShake();
    }

    // Yeni sahneye gecis fonksiyonu
    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); YENI SAHNE ISMI BURADA OLACAK
    }

    // Oyunu tekrar baslatma fonksiyonu
    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
