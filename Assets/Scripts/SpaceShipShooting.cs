using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipShooting : MonoBehaviour
{
    // Reference to the bullet prefab to be instantiated
    [SerializeField] GameObject bullPrefab;
    // Speed at which the bullet will travel
    public float bulSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        // Check if the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Instantiate the bullet at the spaceship's position, slightly offset forward
            // Set the bullet's speed in the direction the spaceship is facing
            Instantiate(bullPrefab, transform.position + transform.up * 0.5f, Quaternion.identity).GetComponent<BulletMovement>().speed = transform.up * bulSpeed;
        }   
    }
}
