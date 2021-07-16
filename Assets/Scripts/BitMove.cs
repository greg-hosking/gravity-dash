using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitMove : MonoBehaviour
{
    GameManager gameManager;

    // Spawn variables
    public float minSpawnY, maxSpawnY;

    // Movement variables
    public Vector3 rotationSpeed;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();        
    }

    void Update()
    {
        // Update the horizontal position of the bit
        transform.position += Vector3.left * gameManager.horizontalObjectSpeed * Time.deltaTime;        

        // Once the bit travels out of bounds, it is destroyed
        if (transform.position.x < gameManager.objectDestroyX)
            Destroy(this.gameObject);
    }

    void FixedUpdate()
    {
        // Rotate the bit
        transform.Rotate(rotationSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
