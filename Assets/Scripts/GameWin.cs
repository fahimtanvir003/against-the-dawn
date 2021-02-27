using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWin : MonoBehaviour
{
    public AudioManager audioManager;
    private void Start()
    {
        audioManager.Play("Game Win");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
        audioManager.Play("MainMenuTheme");
       // audioManager.Stop("Game Win");
    }
}
