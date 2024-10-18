using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    // Public variables to control the asteroid's movement and rotation speed
    public Vector2 velocity;
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public float rotSpeed = 15f;

    // Called when the script instance is being loaded
    private void Start()
    {
        AstMovement(); // Initialize the asteroid's movement
    }

    // Update is called once per frame
    void Update()
    {
        // Move the asteroid based on its velocity
        transform.position += (Vector3)velocity * Time.deltaTime;
        // Rotate the asteroid
        transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);

        // Check if the asteroid is out of bounds and reset its position if necessary
        ResetPositionAtBounds();
    }

    // Check if the asteroid is out of the screen bounds and reset its position to the opposite side
    void ResetPositionAtBounds()
    {
        // Check horizontal bounds
        if (transform.position.x > CameraBounds.screenBounds.x)
        {
            Vector3 prePos = transform.position;
            prePos.x = -CameraBounds.screenBounds.x;
            transform.position = prePos;
        }
        else if (transform.position.x < -CameraBounds.screenBounds.x)
        {
            Vector3 prePos = transform.position;
            prePos.x = CameraBounds.screenBounds.x;
            transform.position = prePos;
        }

        // Check vertical bounds
        if (transform.position.y > CameraBounds.screenBounds.y)
        {
            Vector3 prePos = transform.position;
            prePos.y = -CameraBounds.screenBounds.y;
            transform.position = prePos;
        }
        else if (transform.position.y < -CameraBounds.screenBounds.y)
        {
            Vector3 prePos = transform.position;
            prePos.y = CameraBounds.screenBounds.y;
            transform.position = prePos;
        }
    }

    // Initialize the asteroid's movement with random direction and speed
    void AstMovement()
    {
        // Randomize the rotation speed and direction
        rotSpeed *= (Random.Range(0, 2) == 0) ? -1 : 1;
        rotSpeed *= Random.Range(0.5f, 1.5f);

        // Set a random direction and speed for the asteroid
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        float randSpeed = Random.Range(minSpeed, maxSpeed);
        velocity = randomDir * randSpeed;
    }
}