using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
    //[RequireComponent(typeof(CarController))]
    public class NewCarMod1 : MonoBehaviour
    {
        public GameObject lTailLight;
        public GameObject rTailLight;
        
        public AudioManager audioManager;
        public float damage;
        public Transform tRightFrontWheel, tLeftFrontWheel, tRightBackWheel, tLeftBackWheel;
        public WheelCollider wRightFrontWheel, wLeftFrontWheel, wRightBackWheel, wLeftBackWheel;
        public float carSpeed;
        public float reverseSpeed;
        public float hAxis, vAxis;
        public Rigidbody rb;
        public float accel;
        public float steer;
        public float brakeForce;
        // CarController car;
        public bool is_Accelerating;
        public bool is_Reversing;
        public float CurrentSpeed { get { return rb.velocity.magnitude * 2.23693629f; } }
        //variables for speed meter
        public GameObject needle;
        public float startPos, endPos;
        private float desiredPos;
        public float carSpeeeed;
        // speed meter

        //Player Health
        public HealthBar healthbar;
        public float playerMaxHealth;
        public float currentHealth;

        /*public void AccelInput(float input)
        {
            accel = input;
        }
        public void SteerInput(float input)
        {
            steer = input;
        }
        public void BrakeInput(float input)
        {
            brake = input;
        }*/

        private void Awake()
        {
            audioManager = FindObjectOfType<AudioManager>();
           
           // audioManager.Stop("MainMenuTheme");
        }
        // Start is called before the first frame update
        void Start()
        {

            audioManager.Play("CarAccelerationLow");
            audioManager.Play("Theme");
            rb = GetComponent<Rigidbody>();
            currentHealth = playerMaxHealth;
            lTailLight.SetActive(false);
            rTailLight.SetActive(false);
            //tyreSmoke.Play();
        }
        private void FixedUpdate()
        {
            //UpdateWheelPos();
            //car.Move(steer, accel, accel, brake);
        }
        // Update is called once per frame
        void Update()
        {
            hAxis = Input.GetAxis("Horizontal") * 16f;
            //vAxis = Input.GetAxis("Vertical");
            
            wRightFrontWheel.steerAngle = hAxis;
            wLeftFrontWheel.steerAngle = hAxis;
            //Game restarts if Health goes down to 0
            if (currentHealth <= 0)
            {
                SceneManager.LoadScene("Game Over");
                //audioManager.Stop("Theme");
               // audioManager.Stop("CarAccelerationLow");
            }
            // accel = Input.GetAxis("Vertical");
            /*brake = Input.GetAxis("Jump");*/
            //steer = SimpleInput.GetAxis("Horizontal");
            //CarInput();
            //Accel();
            //Reverse();
            if (Input.GetKeyDown(KeyCode.W))
            {
                Accel();
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                UpAccel();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Reverse();
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                UpRev();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Brake();
            }
            else if(Input.GetKeyUp(KeyCode.Space))
            {
                UpBrake();
            }
            /*if (Input.GetKeyDown(KeyCode.A) && hAxis == -1)
            {
                wRightFrontWheel.steerAngle =  -100;
                wLeftFrontWheel.steerAngle = -100;
            }
            if (Input.GetKeyDown(KeyCode.D) && hAxis ==1)
            {
                wRightFrontWheel.steerAngle = 100;
                wLeftFrontWheel.steerAngle = 100;
            }*/
            Steer();
            /*if (Input.GetKeyDown(KeyCode.W) || InputA.accel == 1)
            {
                Accel();
            }

            if (Input.GetKeyDown(KeyCode.S) || InputB.reverse == 1)
            {
               
                Reverse();
            }*/
            UpdateNeedle();
            carSpeeeed = CurrentSpeed;
           
        }
        void UpdateWheelPos()
        {
            UpdateWheelPos(wRightFrontWheel, tRightFrontWheel);
            UpdateWheelPos(wLeftFrontWheel, tLeftFrontWheel);
            UpdateWheelPos(wRightBackWheel, tRightBackWheel);
            UpdateWheelPos(wLeftBackWheel, tLeftBackWheel);

        }
        void UpdateWheelPos(WheelCollider _collider, Transform _transform)
        {
            Vector3 _pos = _transform.position;
            Quaternion _quat = _transform.rotation;

            _collider.GetWorldPose(out _pos, out _quat);
            _transform.position = _pos;
            _transform.rotation = _quat;
        }

        public void Accel()
        {
            audioManager.Play("CarAccelerationHigh");
            wRightFrontWheel.motorTorque = carSpeed;
            wLeftFrontWheel.motorTorque = carSpeed;
            is_Accelerating = true;
            is_Reversing = false;
            lTailLight.SetActive(false);
            rTailLight.SetActive(false);
            /*transRearLeft.rotation = carSpeed;
            tLeftBackWheel.rotation = carSpeed; */

            //transform.position = Vector3.right * 50;
            // rb.AddForce(Vector3.forward * carSpeed * vAxis);

        }
        public void UpAccel()
        {
           // audioManager.Stop("CarAccelerationHigh");
            wRightFrontWheel.motorTorque = 0;
            wLeftFrontWheel.motorTorque = 0;
            is_Reversing = false;
            is_Accelerating = false;
            lTailLight.SetActive(false);
            rTailLight.SetActive(false);
        }
        public void Reverse()
        {
            audioManager.Play("Reverse");
            wRightFrontWheel.motorTorque = reverseSpeed;
            wLeftFrontWheel.motorTorque = reverseSpeed;
            is_Reversing = true;
            is_Accelerating = false;
            lTailLight.SetActive(true);
            rTailLight.SetActive(true);
            /*transRearLeft.rotation = carSpeed;
            tLeftBackWheel.rotation = carSpeed; */
            // rb.AddForce(Vector3.back * carSpeed * -vAxis);
        }
        public void UpRev()
        {
            //audioManager.Stop("Reverse");
            wRightFrontWheel.motorTorque =0;
            wLeftFrontWheel.motorTorque = 0;
            is_Reversing = false;
            is_Accelerating = false;
            lTailLight.SetActive(false);
            rTailLight.SetActive(false);
        }
        public void Steer()
        {
            //steerAngle = hAxis * maxSteerAngle;
            wRightFrontWheel.steerAngle = hAxis;
            wLeftFrontWheel.steerAngle = hAxis;
            //wRightFrontWheel.motorTorque = hAxis;
            //leftFrontWheel.motorTorque = hAxis;
        }
        // car speed meter
        void UpdateNeedle()
        {
            desiredPos = startPos; //- endPos;
            float temp = carSpeeeed / 100;
            needle.transform.eulerAngles = new Vector3(0, 0, (startPos - temp * desiredPos));
        }
        // car speed meter

        public void Brake()
        {
            audioManager.Play("Brake");
            wRightFrontWheel.brakeTorque = brakeForce;
            wLeftFrontWheel.brakeTorque = brakeForce;
            lTailLight.SetActive(true);
            rTailLight.SetActive(true);
        }
        public void UpBrake()
        {
            //audioManager.Stop("Brake");
            wRightFrontWheel.brakeTorque = 0;
            wLeftFrontWheel.brakeTorque = 0;
            lTailLight.SetActive(false);
            rTailLight.SetActive(false);
        }
        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            healthbar.SetHealth(currentHealth);
        }
        private void OnCollisionEnter(Collision other)
        {
           
            if (other.gameObject.CompareTag("Cars")) //other.gameObject.CompareTag("Zombie"))
            {
                audioManager.Play("CarCrash");
                TakeDamage(2);
            }

        }
        private void OnTriggerEnter(Collider other)
        {

            if (other.gameObject.CompareTag("End"))
            {
                SceneManager.LoadScene("Game Win");
               // audioManager.Stop("Theme");
               // audioManager.Stop("CarAccelerationLow");
            }

        }
        private void OnCollisionExit(Collision collision)
        {
            //TakeDamage(0);
        }
       
    }
}
