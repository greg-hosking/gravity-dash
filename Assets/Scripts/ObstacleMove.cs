using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    private GameManager gameManager;

    // Spawn variables
    public float spawnY;

    // Vertical movement variables
    public bool doesMoveVertically;
    [HideInInspector] public int verticalDirection;
    [SerializeField] private float minY, maxY;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // If the obstacle is one that moves up and down...
        if (doesMoveVertically)
        {
            // Randomly determine its starting direction; a value of 1 denotes upward direction
            // and a value of -1 denotes downward direction
            int[] verticalDirections = { -1, 1 };
            verticalDirection = verticalDirections[Random.Range(0, 2)];
        }
        else
            // A value of 0 here denotes that the obstacle does not move up nor down
            verticalDirection = 0;
    }

    void Update()
    {
        // Update the horizontal position of the obstacle
        transform.position += Vector3.left * gameManager.horizontalObjectSpeed * Time.deltaTime;

        // Update the vertical position of the obstacle
        transform.position += Vector3.up * gameManager.verticalObjectSpeed * verticalDirection * Time.deltaTime;
        // If the obstacle is about to touch either the bottom or top platform, invert its vertical direction
        if (transform.position.y < minY || transform.position.y > maxY)
            verticalDirection *= -1;

        // Once the obstacle travels out of bounds, it is destroyed
        if (transform.position.x < gameManager.objectDestroyX)
        {
            // If the obstacle is a complex obstacle, the parent must also be destroyed
            if (transform.parent != null)
                Destroy(transform.parent.gameObject);
            else
                Destroy(this.gameObject);
        }
    }
}
