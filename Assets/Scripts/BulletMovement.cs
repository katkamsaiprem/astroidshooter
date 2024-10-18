using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [HideInInspector]
    public Vector2 speed; // Speed of the bullet

    // Update is called once per frame
    void Update()
    {
        // Move the bullet based on its speed
        transform.position += (Vector3)speed * Time.deltaTime;
        
        // Check if the bullet is out of bounds and destroy it if necessary
        DestroyOnBoundsCollision();
        
        // Check for collisions between the bullet and asteroids
        CollisionBtwBlt_Ast();
    }

    // Destroy the bullet if it goes out of the screen bounds
    void DestroyOnBoundsCollision()
    {
        if (transform.position.y > CameraBounds.screenBounds.y + 0.5f)
        {
            Destroy(this.gameObject); // Destroy the bullet
        }
    }

    // Check for collisions between the bullet and asteroids
    void CollisionBtwBlt_Ast()
    {
        foreach (GameObject asteroid in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            // If the bullet is close enough to an asteroid, destroy both
            if (Vector2.Distance(asteroid.transform.position, transform.position) <= 0.5f)
            {
                Destroy(asteroid); // Destroy the asteroid
                Destroy(this.gameObject); // Destroy the bullet
            }
        }
    }
}
