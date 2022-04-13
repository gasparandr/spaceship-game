using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
  [Header("General Setup Settings")]
  [Tooltip("How fast ship moves up and down based upon player input")]
  [SerializeField] float controlSpeed = 30f;

  [Header("Laser Gun Array")]
  [Tooltip("Add all player lasers here")]
  [SerializeField] GameObject[] lasers;

  [Tooltip("How fast player moves horizontally")]
  [SerializeField] float xRange = 10f;
  [Tooltip("How fast player moves vertically")]
  [SerializeField] float yRange = 7f;

  [Header("Screen Position Based Tuning")]
  [SerializeField] float positionPitchFactor = -2f;
  [SerializeField] float positionYawFactor = 2f;

  [Header("Player Input Based Tuning")]
  [SerializeField] float controlPitchFactor = -10f;
  [SerializeField] float controlRollFactor = -20f;

  float yThrow, xThrow;


  // Update is called once per frame
  void Update()
  {
    ProcessTranslation();
    ProcessRotation();
    ProcessFiring();
  }


  void SetThrowValues()
  {
    xThrow = Input.GetAxis("Horizontal");
    yThrow = Input.GetAxis("Vertical");
  }

  void ProcessTranslation()
  {
    SetThrowValues();

    float xOffset = xThrow * Time.deltaTime * controlSpeed;
    float rawXPos = transform.localPosition.x + xOffset;
    float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

    float yOffset = yThrow * Time.deltaTime * controlSpeed;
    float rawYPos = transform.localPosition.y + yOffset;
    float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

    transform.localPosition = new Vector3(
        clampedXPos,
        clampedYPos,
        transform.localPosition.z
    );
  }

  void ProcessRotation()
  {
    float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
    float pitchDueToControlThrow = yThrow * controlPitchFactor;

    float pitch = pitchDueToPosition + pitchDueToControlThrow;
    float yaw = transform.localPosition.x * positionYawFactor;
    float roll = xThrow * controlRollFactor;

    transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
  }

  void ProcessFiring()
  {
    // If pushing fire button
    // then print "shooting"
    // else don't print "shooting".

    if (Input.GetButton("Fire1"))
    {
      SetLasersActive(true);
    }
    else
    {
      SetLasersActive(false);
    }
  }

  void SetLasersActive(bool isActive)
  {
    foreach (GameObject laser in lasers)
    {
      //   laser.SetActive(true);
      var emissionModule = laser.GetComponent<ParticleSystem>().emission;
      emissionModule.enabled = isActive;
    }
  }


}
