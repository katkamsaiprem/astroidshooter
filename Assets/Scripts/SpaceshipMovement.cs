
using UnityEngine;
using UnityEngine.SceneManagement;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class SpaceshipMovement : MonoBehaviour
{
    // Public variables for spaceship movement parameters
    public float acceleration = 6.0f; // Acceleration rate of the spaceship
    public float maxSpeed = 6.0f; // Maximum speed the spaceship can reach
    public float drag = 0.6f; // Drag factor to slow down the spaceship when no input is given

    // Private variable to store the current velocity of the spaceship
    Vector2 currVelocity;

    // Update is called once per frame
    void Update()
    {
        // Handle the spaceship's movement based on player input
        HandleMovement();
        // Handle the spaceship's rotation based on its velocity
        HandleRotation();
        // Ensure the spaceship wraps around the screen edges
        WrapObjInScreenBounds();
        // Check for collisions between the spaceship and asteroids
        CollisionBtwShip_Ast();
    }

    // Method to handle the spaceship's movement
    void HandleMovement()
    {
        // Get the input direction from the player (WASD or arrow keys)
        Vector2 inputDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        // Update the current velocity based on the input direction and acceleration
        currVelocity += inputDir * acceleration * Time.deltaTime;
        // Clamp the velocity to ensure it doesn't exceed the maximum speed
        currVelocity = Vector2.ClampMagnitude(currVelocity, maxSpeed);

        // Apply drag to slow down the spaceship when no input is given
        if (inputDir == Vector2.zero)
        {
            currVelocity *= drag;
        }

        // Update the spaceship's position based on the current velocity
        transform.position += (Vector3)currVelocity * Time.deltaTime;
    }

    // Method to handle the spaceship's rotation
    void HandleRotation()
    {
        // Rotate the spaceship to face the direction of its velocity
        if (currVelocity != Vector2.zero)
        {
            Quaternion RawRotation = Quaternion.LookRotation(transform.forward, this.currVelocity);
            Quaternion PlayerRotation = Quaternion.RotateTowards(transform.rotation, RawRotation, 360 * Time.deltaTime);
            transform.rotation = PlayerRotation;
        }
    }

    // Method to wrap the spaceship around the screen edges
    void WrapObjInScreenBounds()
    {
        // Check if the spaceship has moved beyond the right edge of the screen
        if (transform.position.x > CameraBounds.screenBounds.x)
        {
            Vector3 prePos = transform.position;
            prePos.x = -CameraBounds.screenBounds.x; // Move the spaceship to the left edge
            transform.position = prePos;
        }
        // Check if the spaceship has moved beyond the left edge of the screen
        else if (transform.position.x < -CameraBounds.screenBounds.x)
        {
            Vector3 prePos = transform.position;
            prePos.x = CameraBounds.screenBounds.x; // Move the spaceship to the right edge
            transform.position = prePos;
        }

        // Check if the spaceship has moved beyond the top edge of the screen
        if (transform.position.y > CameraBounds.screenBounds.y)
        {
            Vector3 prePos = transform.position;
            prePos.y = -CameraBounds.screenBounds.y; // Move the spaceship to the bottom edge
            transform.position = prePos;
        }
        // Check if the spaceship has moved beyond the bottom edge of the screen
        else if (transform.position.y < -CameraBounds.screenBounds.y)
        {
            Vector3 prePos = transform.position;
            prePos.y = CameraBounds.screenBounds.y; // Move the spaceship to the top edge
            transform.position = prePos;
        }
    }

    // Method to check for collisions between the spaceship and asteroids
    void CollisionBtwShip_Ast()
    {
        // Iterate through all game objects tagged as "Asteroid"
        foreach (GameObject asteroid in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            // Check if the distance between the spaceship and the asteroid is less than or equal to 0.5 units
            if (Vector2.Distance(asteroid.transform.position, transform.position) <= 0.5f)
            {
                Destroy(asteroid); // Destroy the asteroid
                GameOver(); // Trigger the game over sequence
                Destroy(this.gameObject); // Destroy the spaceship
            }
        }
    }

    // Method to handle the game over sequence
    void GameOver()
    {
        SceneManager.LoadScene("Gameplay"); // Reload the gameplay scene
        Debug.Log("GAME OVER!!"); // Log the game over message
    }
}
