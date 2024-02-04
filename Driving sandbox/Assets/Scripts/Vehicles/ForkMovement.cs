using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Unity.Mathematics;
using UnityEngine;

public class ForkMovement : MonoBehaviour
{
    //max lift turn over x = -3.5 <= x <= 8
    //max fork height y = -0.5 <= y <= 3.5
    [SerializeField] private GameObject liftGroup;
    [SerializeField] private GameObject fork;
    [SerializeField] private LiftState liftState = LiftState.Idle;
    [SerializeField] private TiltState tiltState = TiltState.Idle;
    [SerializeField] private float liftSensitivity = .05f;
    [SerializeField] private float tiltSensitivity = 5.0f;

    private const float maxLift = 3.5f;
    private const float minLift = -.5f;
    private const float maxTilt = 4.0f;
    private const float minTilt = -3.5f;

    private enum LiftState
    {
        Up,
        Idle,
        Down
    }

    private enum TiltState
    {
        Forward,
        Idle,
        Backward
    }

    private void Update()
    {
        GetTiltState();
        GetLiftState();
    }

    private void LateUpdate()
    {
        TiltLift();
        MoveFork();
    }

    private void TiltLift()
    {
        liftGroup.transform.GetLocalPositionAndRotation(out var loc, out var rot);

        var xRotation = rot.x * Mathf.Rad2Deg;
        switch (tiltState)
        {
            case TiltState.Forward:
                xRotation += tiltSensitivity * Time.deltaTime;
                break;
            case TiltState.Backward:
                xRotation -= tiltSensitivity * Time.deltaTime;
                break;
        }

        xRotation = Math.Clamp(xRotation, minTilt, maxTilt);
        rot.x = xRotation * Mathf.Deg2Rad;
        
        liftGroup.transform.SetLocalPositionAndRotation(loc, rot);
    }

    private void MoveFork()
    {
        fork.transform.GetLocalPositionAndRotation(out var loc, out var Rot);

        switch (liftState)
        {
            case (LiftState.Up):
                loc.y += liftSensitivity * Time.deltaTime;
                break;
            case (LiftState.Down):
                loc.y -= liftSensitivity * Time.deltaTime;
                break;
        }

        loc.y = Math.Clamp(loc.y, minLift, maxLift);
        
        fork.transform.SetLocalPositionAndRotation(loc, Rot);
    }

    private void GetTiltState()
    {
        if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.Q))
            return;
        if (!Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q))
            tiltState = TiltState.Idle;
        else if (Input.GetKey(KeyCode.E))
            tiltState = TiltState.Forward;
        else if (Input.GetKey(KeyCode.Q))
            tiltState = TiltState.Backward;
    }

    private void GetLiftState()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl))
            return;
        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
            liftState = LiftState.Idle;
        else if (Input.GetKey(KeyCode.LeftShift))
            liftState = LiftState.Up;
        else if (Input.GetKey(KeyCode.LeftControl))
            liftState = LiftState.Down;
    }
}