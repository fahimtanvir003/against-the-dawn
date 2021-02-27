using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CarNewMove : MonoBehaviour
{
    float xInput;
    public float motorSpeed;
    public float currentSteerAngle;
    public float maxSteerAngle;
    public float carForwardSpeed;
    Rigidbody rb;
    [SerializeField] private WheelCollider frontLeftWheel;
    [SerializeField] private WheelCollider frontRightWheel;
    [SerializeField] private WheelCollider backLeftWheel;
    [SerializeField] private WheelCollider backRightWheel;

    [SerializeField] private Transform frontLeftWheelTrans;
    [SerializeField] private Transform frontRightWheelTrans;
    [SerializeField] private Transform backLeftWheelTrans;
    [SerializeField] private Transform backRightWheeltrans;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, carForwardSpeed) * Time.deltaTime;
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheel();
    }
    void HandleMotor()
    {
        frontLeftWheel.motorTorque = motorSpeed;
        frontRightWheel.motorTorque = motorSpeed;
    }
    void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * xInput;
        frontLeftWheel.steerAngle = currentSteerAngle;
        frontRightWheel.steerAngle = currentSteerAngle;
    }
    void UpdateWheel()
    {
        UpdateSingleWheel(frontLeftWheel, frontLeftWheelTrans);
        UpdateSingleWheel(frontRightWheel, frontRightWheelTrans);
        UpdateSingleWheel(backRightWheel, backRightWheeltrans);
        UpdateSingleWheel(backLeftWheel, backLeftWheelTrans);
    }
    void UpdateSingleWheel(WheelCollider Wheel, Transform WheelTrans)
    {
        Vector3 pos;
        Quaternion rot;
        Wheel.GetWorldPose(out pos, out rot);
        WheelTrans.position = pos;
        WheelTrans.rotation = rot;
    }
    void GetInput()
    {
        xInput = Input.GetAxis("Horizontal");
    }
}
