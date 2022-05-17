using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class carController : MonoBehaviour
{
    private float mHorizantalInput, mVerticalInput, brakeInput, mSteeringAngle, revInput;
    float[] gears = new float[4] { 0.3f, 0.5f, 0.7f, 0.9f };
    [SerializeField] int gearIndex = 0;
    [SerializeField] float mySpeed = 0;
    private const float speedMultiplier = 0.1f;
    public float speed = 1;
    public WheelCollider fDriverW, fPassengerW, rDriverW, rPassengerW;
    public Transform fDriverT, fPassengerT, rDriverT, rPassengerT;

    public float maxSteeringAngle = 30;
    public float motorForce = 5000;
    Rigidbody rb;
    public Transform centerObject;

    public Text textField;
    public void getInput()
    {
        mHorizantalInput = Input.GetAxis("Horizontal");
        mVerticalInput = (Input.GetAxis("Vertical") + 1) / 2.0f;
        brakeInput = Mathf.Clamp01(Input.GetAxis("Brake") + 1);
        revInput = (Input.GetAxis("Reverse") + 1) / 2.0f;
        //Debug.Log(mVerticalInput);
    }
    private void Update()
    {
        if (rb.velocity.magnitude < 1)
        {
            gearIndex = 0;
        }
        //Debug.Log(fDriverW.motorTorque);
        if (mVerticalInput > 0)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton4))
            {


                if (gearIndex == 0 && rb.velocity.magnitude > 15)
                {
                    gearIndex++;
                }
                else if (gearIndex == 1 && rb.velocity.magnitude > 30)
                {
                    gearIndex++;
                }
                else if (gearIndex == 2 && rb.velocity.magnitude > 45)
                {
                    gearIndex++;
                }
                else if (gearIndex == 3 && rb.velocity.magnitude > 60)
                {
                    gearIndex++;
                }
                if (gearIndex > 3)
                {
                    gearIndex = 3;
                }

                motorForce = gears[gearIndex];
            }
            else if (Input.GetKeyDown(KeyCode.JoystickButton5))
            {
                gearIndex--;
                if (gearIndex < 0)
                {
                    gearIndex = 0;
                }

                motorForce = gears[gearIndex];

            }
        }
        
        mySpeed = rb.velocity.magnitude;
        controlSound();
        // Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);
        // Debug.Log(gearIndex);


    }
    public void steer()
    {
        // mSteeringAngle = maxSteeringAngle * mHorizantalInput;
        mSteeringAngle = maxSteeringAngle * Input.GetAxis("Horizontal");
        fDriverW.steerAngle = mSteeringAngle;
        fPassengerW.steerAngle = mSteeringAngle;
        //Debug.Log(mSteeringAngle);

    }

    public void Accelerate()
    {
        fDriverW.motorTorque = mVerticalInput * motorForce * speed;
        fPassengerW.motorTorque = mVerticalInput * motorForce * speed;
        //Debug.Log(fPassengerW.motorTorque);

        //Debug.Log(motorForce);

    }

    public void Brake()
    {
        fDriverW.brakeTorque = brakeInput * motorForce * speed * 2;
        fPassengerW.brakeTorque = brakeInput * motorForce * speed * 2;


        //rb.velocity *= 1.0f - brakeInput;
        //fDriverW.motorTorque *= 1.0f - brakeInput;
        //fPassengerW.motorTorque *= 1.0f - brakeInput;
    }
    public void reverse()
    {
        fDriverW.motorTorque = revInput * -motorForce * speed;
        fPassengerW.motorTorque = revInput * -motorForce * speed;
    }

    private void updateWheelPosses()
    {
        getWheels(fDriverW, fDriverT);
        getWheels(fPassengerW, fPassengerT);
        getWheels(rDriverW, rDriverT);
        getWheels(rPassengerW, rPassengerT);

    }

    private void getWheels(WheelCollider _wheelCollider, Transform _transform)
    {
        Vector3 _pos = _transform.position * speed;


        Quaternion quaternion = transform.rotation;

        _wheelCollider.GetWorldPose(out _pos, out quaternion);
        _transform.position = _pos;
        _transform.rotation = quaternion;
        //Debug.Log(transform.rotation);
    }
    private void FixedUpdate()
    {
        Brake();
        if (mVerticalInput > 0)
        {
            Accelerate();
        }
        else
        {
            reverse();
        }
        getInput();
        steer();
        updateWheelPosses();
        rb.AddForce(Vector3.down * 10, ForceMode.Acceleration);
        //Debug.Log(mSteeringAngle);
        //Debug.Log(speed);


        //textField.text = "Motor: " + (mVerticalInput * motorForce * speed * 2).ToString("F2");
        // textField.text += "\nBrake: " + (brakeInput * motorForce * speed * 2).ToString("F2");
        //f textField.text += "\nRigidbody: " + transform.InverseTransformDirection(rb.velocity).z.ToString("F2");
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.centerOfMass = centerObject.position;
        //rb.centerOfMass -= Vector3.down;
    }
    public void controlSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (gearIndex == 0 && rb.velocity.magnitude < 37)
        {
            audioSource.pitch = 0.8f + (rb.velocity.magnitude / 37.0f) * 0.6f; // 0, 37
        }
        else if (gearIndex == 1 && rb.velocity.magnitude < 40 && rb.velocity.magnitude > 20)
        {
            audioSource.pitch = 0.9f + (Mathf.Max(0, (rb.velocity.magnitude - 37.0f)) / 14.0f) * 0.6f; // 37, 51
        }
        else if (gearIndex == 2 && rb.velocity.magnitude < 60 && rb.velocity.magnitude > 40)
        {
            audioSource.pitch = 1.0f + (Mathf.Max(0, (rb.velocity.magnitude - 51)) / 8.0f) * 0.6f; // 51, 59
        }
        else if (gearIndex == 3 && rb.velocity.magnitude < 80 && rb.velocity.magnitude > 60)
        {
            audioSource.pitch = 1.1f + (Mathf.Max(0, (rb.velocity.magnitude - 59)) / 4.0f) * 0.6f;
        }

    }
}
