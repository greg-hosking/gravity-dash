using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;

    // Camera shake variables
    public CameraShake cameraShake;
    public float platformCollisionShakeDuration;
    public float platformCollisionShakeAmount;
    public float obstacleCollisionShakeDuration;
    public float obstacleCollisionShakeAmount;    

    // Sound variables
    public AudioSource audioSource;
    public AudioClip[] gravityReverseSounds;
    public AudioClip[] bitCollectSounds;
    public AudioClip[] platformCollisionSounds;
    public AudioClip[] obstacleCollisionSounds;

    // Player control variables
    Rigidbody rb;
    bool isGravityNormal;

    // Player material variables
    public Material normalGravityMaterial;
    public Material reversedGravityMaterial;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();

        // Start the game with normal gravity
        isGravityNormal = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            ReverseGravity();
    }

    void FixedUpdate()
    {
        // Apply the proper gravitational force
        if (isGravityNormal)
            rb.AddForce(Physics.gravity);
        else
            rb.AddForce(Physics.gravity * -2.0f);
    }

    void OnCollisionEnter(Collision other)
    {
        // If the player collides with a platform...
        if (other.gameObject.CompareTag("Platform"))
            OnPlatformCollide();
        // If the player collides with an obstalce...
        else if (other.gameObject.CompareTag("Obstacle"))
            OnObstacleCollide();
    }

    void OnTriggerEnter(Collider other)
    {
        // If the player "collides" with a bit (collects),
        // play a random bit collect sound and increment bitsCollected
        PlayRandomSound(bitCollectSounds);
        gameManager.bitsCollected++;
    }

    void OnPlatformCollide()
    {
        // Slightly shake the camera upon colliding with a platform
        // in order to emphasize the impact
        cameraShake.ShakeCamera(platformCollisionShakeDuration, platformCollisionShakeAmount);

        // Play a random platform collision sound
        PlayRandomSound(platformCollisionSounds);
    }

    void OnObstacleCollide()
    {
        // Greatly shake the camera upon colliding with an obstacle
        // in order to greatly emphasize the impact
        cameraShake.ShakeCamera(obstacleCollisionShakeDuration, obstacleCollisionShakeAmount); 

        // Play a random obstacle collision sound
        PlayRandomSound(obstacleCollisionSounds);
    }

    void ReverseGravity()
    {
        // If gravity is normal...
        if (isGravityNormal)
        {
            // Activate reversed gravity and change material
            rb.useGravity = false;
            GetComponent<MeshRenderer>().material = reversedGravityMaterial;
        }
        // If gravity is reversed...
        else
        {
            // Activate normal gravity and change material
            rb.useGravity = true;
            GetComponent<MeshRenderer>().material = normalGravityMaterial;
        }
        isGravityNormal = !isGravityNormal;
        
        // Torque the player slightly and play a random gravity reverse sound
        rb.AddTorque(Vector3.back, ForceMode.Impulse);
        PlayRandomSound(gravityReverseSounds);
    }

    void PlayRandomSound(AudioClip[] sounds)
    {
        // Pick a random sound from the array
        int randomIndex = Random.Range(0, sounds.Length);
        AudioClip randomSound = sounds[randomIndex];
        // Play the sound
        audioSource.PlayOneShot(randomSound);
    }
}
