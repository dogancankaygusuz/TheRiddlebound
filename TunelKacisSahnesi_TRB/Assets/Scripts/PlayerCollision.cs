using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;  // Bitis Panel
    [SerializeField] private AudioClip crashSound; // Engele carpýlýnca calan ses
    [SerializeField] private TextMeshProUGUI finalScoreText; // Skor texti

    PlayerController playerController;
    private AudioSource audioSource;
    private bool isGameOver = false;

    void Start()
    {
        // Gerekli referanslarý alma
        audioSource = GetComponent<AudioSource>();
        playerController = FindObjectOfType<PlayerController>();
        gameOverPanel.SetActive(false);
    }

    // Engel metotu ve panel gösterme
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && !isGameOver)
        {
            isGameOver = true;

            if (crashSound != null)
                audioSource.PlayOneShot(crashSound);

            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);

            finalScoreText.text = "SKOR: " + Mathf.FloorToInt(playerController.score).ToString();
        }
    }

    // Oyunu tekrar baslatma metotu
    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Oyundan cýkýs metotu
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();

        // Editor'de calýstýgý icin cýkýsý görebilmek icin
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
