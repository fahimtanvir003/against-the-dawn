using System;

using UnityEngine;
using UnityEngine.UI;

using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
        public bool accel;
        public bool revBool;
        public int GasInput;
        public int BreakInput;
        //public Rigidbody rb;
        //public float reverseSpeed;
        public float h;
        public Transform transRightFrontWheel, transLeftFrontWheel;
        public WheelCollider rightFrontWheel, leftFrontWheel;
        public float carSpeed;
        //public float hAxis, vAxis;
        //public float steerAngle;
        public float maxSteerAngle;
        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }

        public void GasPressed()
        {
            GasInput = 1;
            accel = true;
            revBool = false;
        }

        public void GasReleased()
        {
            GasInput = 0;
            accel = false;
        }
        public void Reverse()
        {
            GasInput = -1;
            accel = false;
            revBool = true;
        }
        public void NotReverse()
        {
            GasInput = 1;
            accel = false;
            revBool = false;
            rightFrontWheel.motorTorque = 0f;
            leftFrontWheel.motorTorque = 0f;
        }

        public void BreakPressed()
        {
            BreakInput = 1;
        }

        public void BreakReleased()
        {
            BreakInput = 0;
        }

        private void FixedUpdate()
        {
            // pass the input to the car!
            //float h = CrossPlatformInputManager.GetAxis("Horizontal");
            //float 
            h = Input.GetAxis("Horizontal");
            //float v = SimpleInput.GetAxis("Vertical");

            //float v = GasInput;

#if !MOBILE_INPUT
            //float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            float handbrake = BreakInput;
            //m_Car.Move(h, v, v, handbrake);

#else
            if (GasInput == 0)
            {
                accel = false;
                revBool = false;
                GasInput = 0;
                m_Car.Move(h, 0, 0, 0);
                /* rightFrontWheel.motorTorque = 0;
                 leftFrontWheel.motorTorque = 0;*/
            }
            m_Car.Move(h, GasInput, GasInput, 0);
            /*if (GasInput == 1)
            {

            }
            if (GasInput == -1)
            {
                m_Car.Move(h, GasInput, GasInput, 0);
            }*/
            // hAxis = SimpleInput.GetAxis("Horizontal");
            // vAxis = Input.GetAxis("Vertical");
            //Reverse();
#endif
        }
        private void Update()
        {
            // Accel();
            //Steer();
            //CarInput();
            /*if (GasInput == 1)
            {
                accel = true;
                revBool = false;
            }*/
        }

        /*public void Reverse()
        {
            GasInput = -1;
            accel = false;
            revBool = true;
            //leftFrontWheel.motorTorque = -carSpeed * Time.deltaTime;
            //rightFrontWheel.motorTorque = -carSpeed * Time.deltaTime;
        }*/
        public void UnReverse()
        {
            GasInput = 0;
            rightFrontWheel.motorTorque = 1;
            leftFrontWheel.motorTorque = 1;
        }

        /*public void CarInput()
        {
            if (rightFrontWheel.motorTorque == 1)
            {
                Accel();
            }
            if (rightFrontWheel.motorTorque == -1)
            {
                Reverse();
            }
        }*/
        /*public void Accel()
        {
            rightFrontWheel.motorTorque = vAxis * carSpeed;
            leftFrontWheel.motorTorque = vAxis * carSpeed;
        }*/
        /*public void Reverse()
        {
            rightFrontWheel.motorTorque = vAxis * carSpeed;
            leftFrontWheel.motorTorque = vAxis * carSpeed;
        }*/
        public void Steer()
        {
            //steerAngle = hAxis * maxSteerAngle;
            rightFrontWheel.steerAngle = h * maxSteerAngle;
            leftFrontWheel.steerAngle = h * maxSteerAngle;

        }
    }

}
