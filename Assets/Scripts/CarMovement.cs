using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float carForwardSpeed;
    public float steeringSpeed;
    float xInput;
    Rigidbody rb;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector3(xInput * steeringSpeed, rb.velocity.y, carForwardSpeed) * Time.deltaTime;
    }
}
