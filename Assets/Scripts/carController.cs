using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class carController : MonoBehaviour
{
    private float mHorizantalInput, mVerticalInput, brakeInput, mSteeringAngle, revInput;


    public float speed = 1;
    public WheelCollider fDriverW, fPassengerW, rDriverW, rPassengerW;
    public Transform fDriverT, fPassengerT, rDriverT, rPassengerT;

    public float maxSteeringAngle = 30;
    public float motorForce = 50;
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


        if (Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            Debug.Log("Pressed");
        }

    }
    public void steer()
    {
        mSteeringAngle = maxSteeringAngle * mHorizantalInput;
        mSteeringAngle = maxSteeringAngle * Input.GetAxis("Horizontal");
        
        fDriverW.steerAngle = mSteeringAngle;
        fPassengerW.steerAngle = mSteeringAngle;
    }
    public void Accelerate()
    {
        fDriverW.motorTorque = mVerticalInput * motorForce * speed;
        fPassengerW.motorTorque = mVerticalInput * motorForce * speed;
        //Debug.Log(fPassengerW.motorTorque);
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




}
