using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameOver : MonoBehaviour
{

    public GameObject gameOverPanel;
    public AudioSource gameOverSound;
    [SerializeField] private AudioSource ambientMusic;
    public void ShowGameOver()
    {
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (ambientMusic != null)
        {
            ambientMusic.Stop();
        }

        if(gameOverSound != null)
        {
            gameOverSound.Play();
        }

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuStart");
    }
}
