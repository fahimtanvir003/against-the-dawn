using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ILastEntered : MonoBehaviour
{
    public Collider iLastEntered;
    public Collider iLastExited;

    private void OnTriggerEnter(Collider other)
    {
        iLastEntered = other;
    }
    private void OnTriggerExit(Collider other)
    {
        iLastExited = other;
    }
}
