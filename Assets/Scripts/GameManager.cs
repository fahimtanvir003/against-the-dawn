using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   // public CarSelection carSelection;
    public GameObject hammer;
    public GameObject bourak;
    public GameObject sedan;
    public GameManager gmInstance;

    private void Awake()
    {
        /*if (!gmInstance)
        {
            gmInstance = this;
        }
        else if (gmInstance != null)
        {
            Destroy(gameObject);
            return;
        }*/

        if(gmInstance == null)
        {
            gmInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(gmInstance != this)
        {
            Destroy(gameObject);
        }
             
    }
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (CarSelection.currentCar == 0)
        {
            sedan.SetActive(true);
            hammer.SetActive(false);
            bourak.SetActive(false);
        }
        else if (CarSelection.currentCar == 1)
        {
            sedan.SetActive(false);
            hammer.SetActive(true);
            bourak.SetActive(false);
        }
        else if (CarSelection.currentCar == 2)
        {
            sedan.SetActive(false);
            hammer.SetActive(false);
            bourak.SetActive(true);
        }

    }
    public void PlayButton()
    {
        SceneManager.LoadScene("Level01");

       /* if (CarSelection.currentCar == 0)
        {
            sedan.SetActive(true);
            hammer.SetActive(false);
            bourak.SetActive(false);
        }
        else if (CarSelection.currentCar == 1)
        {
            sedan.SetActive(false);
            hammer.SetActive(true);
            bourak.SetActive(false);
        }
        else if (CarSelection.currentCar == 2)
        {
            sedan.SetActive(false);
            hammer.SetActive(false);
            bourak.SetActive(true);
        }*/
    }
}
