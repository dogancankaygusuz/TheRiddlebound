using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;  // Bitis Panel
    [SerializeField] private AudioClip crashSound; // Engele carp�l�nca calan ses
    [SerializeField] private TextMeshProUGUI finalScoreText; // Skor texti

    PlayerController playerController;
    private AudioSource audioSource;
    private bool isGameOver = false;

    void Start()
    {
        // Gerekli referanslar� alma
        audioSource = GetComponent<AudioSource>();
        playerController = FindObjectOfType<PlayerController>();
        gameOverPanel.SetActive(false);
    }

    // Engel metotu ve panel g�sterme
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

    // Oyundan c�k�s metotu
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();

        // Editor'de cal�st�g� icin c�k�s� g�rebilmek icin
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
