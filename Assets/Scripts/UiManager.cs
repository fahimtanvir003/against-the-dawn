using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UiManager : MonoBehaviour
{
    private AudioManager audioManager;
    public GameObject panel;
    public bool state;
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    public void SwitchShowHide()
    {
        state = !state;
        if (state==true)
        {
            audioManager.Play("ButtonClick");
            panel.SetActive(true);
            Time.timeScale = 0;
            state = true;
        }
        else
        {
            audioManager.Play("ButtonClick");
            panel.SetActive(false);
            Time.timeScale = 1;
            state = false;
        }
    }

    public void restart()
    {
        audioManager.Play("ButtonClick");
        SceneManager.LoadScene("MainLevel");
        Time.timeScale = 1;
    }

    public void menu()
    {
        audioManager.Play("ButtonClick");
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
