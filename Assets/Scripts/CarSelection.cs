using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    public static int currentCar;
    public Button leftButton;
    public Button rightButton;
    public Text carPrice;
    
    private void Awake()
    {
        currentCar = PlayerPrefs.GetInt("Car");
        SelectCar(currentCar);
    }
    public void SelectCar(int index)
    {
        leftButton.interactable = (index != 0);
        rightButton.interactable = (index != transform.childCount - 1);
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == index);
           
        }
    }
    public void Update()
    {
        if(currentCar == 0)
        {
            carPrice.text = "Sedan : $5,000";
            PlayerPrefs.SetInt("Car", 0);
        }
        if(currentCar == 1)
        {
            carPrice.text = "Hammer : $10,000";
            PlayerPrefs.SetInt("Car", 1);
        }
        if (currentCar == 2)
        {
            carPrice.text = "Bourak : $40,000";
            PlayerPrefs.SetInt("Car", 2);
        }
        if(currentCar == 3)
        {
            carPrice.text = "Dodge : $60,000";
            PlayerPrefs.SetInt("Car", 3);
        }
    }
    public void ChangeCar(int change)
    {
        currentCar += change;
        
        SelectCar(currentCar);
    }
    
}
