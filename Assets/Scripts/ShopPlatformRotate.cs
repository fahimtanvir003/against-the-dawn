using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPlatformRotate : MonoBehaviour
{
    public Transform platform;
    
    public float rotationSpeed;
    // Update is called once per frame
    void Update()
    {
        platform.transform.Rotate( new Vector3(0f,rotationSpeed, 0f));
        
    }
}
