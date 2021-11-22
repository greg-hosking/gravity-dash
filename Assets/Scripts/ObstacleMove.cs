using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    GameManager gameManager;

    // Spawn variables
    public float spawnY;

    // Vertical movement variables
    public bool doesMoveVertically;
    public int initialVerticalDirection;
    int verticalDirection = 0;
    public float minY, maxY;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        if (doesMoveVertically)
            verticalDirection = initialVerticalDirection;
    }

    void Update()
    {
        if (gameManager.state == 0)
        {
            Destroy(this.gameObject);
        }
        else if (gameManager.state == 1)
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
}
