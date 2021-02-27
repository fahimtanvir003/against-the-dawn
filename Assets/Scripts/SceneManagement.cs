using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
    public AudioManager audioManager;
    public GameObject loadingScreen;
    public Slider loadingSlider;
    public GameObject panel;
    public bool state;

    private void Start()
    {
        audioManager.Play("MainMenuTheme");
    }
    public void loadlevel(int sceneIndex)
    {
        //SceneManager.LoadScene("Level01");
        StartCoroutine(LoadAsynchronously(sceneIndex));
        audioManager.Play("ButtonClick");
        //audioManager.Stop("MainMenuTheme");
        //audioManager.Play("Theme");
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;
            yield return null;
        }
    }

    public void quit()
    {
        audioManager.Play("ButtonClick");
        Application.Quit();
    }
    public void SwitchShowHide()
    {
        state = !state;
        if (state == true)
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
}
