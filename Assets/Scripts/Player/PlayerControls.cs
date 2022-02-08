using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup")]

    [Tooltip("How fast ship along screen")]
    [SerializeField] float controlSpeed = 30f;
    [Tooltip("Range of movement along vertical axis")]
    [SerializeField] float xRange = 13f;
    [Tooltip("Range of movement along Horizonal axis")]
    [SerializeField] float yRange = 8f;
    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] GameObject[] lasers;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -3f;
    [SerializeField] float positionYawFactor = 2.3f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlRollFactor = -20f;

    [Header("Ship input configurations")]
    [Tooltip("Input options for movement")]
    [SerializeField] InputAction movement;
    [Tooltip("Input options for firing weapons")]
    [SerializeField] InputAction fire;

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
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (fire.ReadValue<float>() > 0.5)
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    private void SetLasersActive(Boolean state)
    {
        foreach (GameObject laser in lasers)
        {
            var emission = laser.GetComponent<ParticleSystem>().emission;
            emission.enabled = state;
        }
    }
}
