using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class CarController : MonoBehaviour
{
    public enum Axle
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject WheelModel;
        public WheelCollider WheelCollider;
        public Axle Axle;
        public bool MotorDriven;
        public bool Steerable;
        public GameObject WheelEffectObj;
    }

    [SerializeField] private float maxAcceleration = 30.0f;
    [SerializeField] private float brakeAcceleration = 50.0f;
    [SerializeField] private float steerSensitivity = 1.0f;
    [SerializeField] private float maxSteerAngle = 45.0f;
    [SerializeField] private List<Wheel> wheels = new();
    [SerializeField] private Rigidbody carBody;
    [SerializeField] private Vector3 com; //CenterOfMass

    private float moveInput;
    private float steerInput;
    private const KeyCode BrakeKey = KeyCode.B;
    private void Start()
    {
        carBody = GetComponent<Rigidbody>();
        carBody.centerOfMass = com;
    }

    private void FixedUpdate()
    {
        getInputs();
        AnimateWheels();
        WheelEffects();
    }

    private void LateUpdate()
    {
        steer();
        move();
        Brake();
    }

    private void getInputs()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }

    private void move()
    {
        foreach (var w in wheels)
            if (w.MotorDriven)
                w.WheelCollider.motorTorque = moveInput * maxAcceleration * Time.fixedDeltaTime * 400;
    }

    private void steer()
    {
        foreach (var w in wheels)
            if (w.Steerable)
            {
                var steerAngle = steerInput * steerSensitivity * maxSteerAngle;
                w.WheelCollider.steerAngle = -steerAngle;
            }
    }
    
    private void Brake()
    {
        if (Input.GetKey(BrakeKey))
            foreach (var w in wheels)
                w.WheelCollider.brakeTorque = 250 * brakeAcceleration * Time.fixedDeltaTime;
        else
            foreach (var w in wheels)
                w.WheelCollider.brakeTorque = 0;
    }

    private void AnimateWheels()
    {
        foreach (var w in wheels)
        {
            w.WheelCollider.GetWorldPose(out var pos, out var rot);
            w.WheelModel.transform.SetPositionAndRotation(pos, rot);
        }
    }
    
    private void WheelEffects()
    {
        foreach (var w in wheels)
        {
            if (Input.GetKey(BrakeKey))
                w.WheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = true;
            else
                w.WheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = false;
        }
    }
}