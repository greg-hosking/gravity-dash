using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake
    // Grab the gameObject's transform if it is null
    Transform cameraTransform;

    // How long the camera should shake for
    [HideInInspector] public float originalShakeDuration;
    public float shakeDuration;

    // Amplitude of the shake (a larger value shakes the camera harder)
    public float shakeAmount;
    public bool doShake;
    Vector3 originalPos;

    void Start()
    {
        cameraTransform = GetComponent<Transform>();
        originalPos = cameraTransform.position;
    }

    void Update()
    {
        if (doShake)
        {
            if (shakeDuration > 0)
            {
                cameraTransform.position = originalPos + Random.insideUnitSphere * shakeAmount;
                shakeDuration -= Time.deltaTime;
            }
            else
            {
                doShake = false;
                cameraTransform.position = originalPos;
                shakeDuration = originalShakeDuration;
            }
        }
    }

    public void ShakeCamera(float _shakeDuration, float _shakeAmount)
    {
        doShake = true;
        originalShakeDuration = _shakeDuration;
        shakeAmount = _shakeAmount;
    }
}
