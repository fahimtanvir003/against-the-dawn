using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public AudioManager audioManager;
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void Start()
    {
        audioManager.Play("GameOver");
    }
    public void Restart()
    {
        SceneManager.LoadScene("MainLevel");
        //audioManager.Stop("GameOver");
        audioManager.Play("Theme");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
        audioManager.Play("MainMenuTheme");
       // audioManager.Stop("GameOver");
    }
    
}
