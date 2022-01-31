using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;

    [SerializeField] float controlSpeed = 30f;
    [SerializeField] float xRange = 13f;
    [SerializeField] float yRange = 8f;

    [SerializeField] float positionPitchFactor = -3f;
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float positionYawFactor = 2.3f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;

    void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }
    void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float yawDueToPosition = transform.localPosition.x * positionYawFactor;

        float rollDueToControlThrow = xThrow * controlRollFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = yawDueToPosition;
        float roll = rollDueToControlThrow;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        xThrow = movement.ReadValue<Vector2>().x;
        yThrow = movement.ReadValue<Vector2>().y;

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clammpedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clammpedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clammpedXPos, clammpedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (fire.ReadValue<float>() > 0.5)
        {
            Debug.Log("Button is Pressed");
        }
        else
        {
            Debug.Log("Button not pressed");
        }
        //if pushing fire button
        //Then print shoooting
        //or don't print shooting
    }
}
