using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class carController : MonoBehaviour
{
    private float mHorizantalInput, mVerticalInput, brakeInput, mSteeringAngle;
    public int lowestSpeed = 1;
    public int highestSpeed = 50;
    public float lowSteerAngle = 1;
    public float highSteerAngle = 50;
    public int speed = 5;
    public WheelCollider fDriverW, fPassengerW, rDriverW, rPassengerW;
    public Transform fDriverT, fPassengerT, rDriverT, rPassengerT;

    public float maxSteeringAngle = 30;
    public float motorForce = 50;
    Rigidbody rb;

    public Text textField;
    public void getInput()
    {
        mHorizantalInput = Input.GetAxis("Horizontal");
        mVerticalInput = (Input.GetAxis("Vertical") + 1) / 2.0f;
        brakeInput = Mathf.Clamp01(Input.GetAxis("Brake") + 1);
        //Debug.Log(mVerticalInput);
    }

    public void steer()
    {
        //mSteeringAngle = maxSteeringAngle * mHorizantalInput;
        float speedFactor = rb.velocity.magnitude / highestSpeed;
        var currentSteerAngle = 30f;// Mathf.Lerp(lowSteerAngle, highSteerAngle, speedFactor);
        currentSteerAngle *= Input.GetAxis("Horizontal");
        //mSteeringAngle = Input.GetAxis("Horizontal");
        //fDriverW.steerAngle = mSteeringAngle;
        fDriverW.steerAngle = currentSteerAngle;
        //fPassengerW.steerAngle = mSteeringAngle;
        fPassengerW.steerAngle = currentSteerAngle;
        //currentSteerAngle = maxSteeringAngle;
        Debug.Log("Steering: " + (currentSteerAngle).ToString("F2"));

    }

    public void Accelerate()
    {
        fDriverW.motorTorque = mVerticalInput * motorForce * speed;
        fPassengerW.motorTorque = mVerticalInput * motorForce * speed;
    }

    public void Brake()
    {
        fDriverW.brakeTorque = brakeInput * motorForce * speed * 2;
        fPassengerW.brakeTorque = brakeInput * motorForce * speed * 2;


        //rb.velocity *= 1.0f - brakeInput;
        //fDriverW.motorTorque *= 1.0f - brakeInput;
        //fPassengerW.motorTorque *= 1.0f - brakeInput;
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
        getInput();
        steer();
        Accelerate();
        updateWheelPosses();
        //Debug.Log(speed);
        //textField.text = "Motor: " + (mVerticalInput * motorForce * speed * 2).ToString("F2");
        // textField.text += "\nBrake: " + (brakeInput * motorForce * speed * 2).ToString("F2");
        //f textField.text += "\nRigidbody: " + transform.InverseTransformDirection(rb.velocity).z.ToString("F2");
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }




}
