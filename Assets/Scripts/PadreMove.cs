using UnityEngine;
using UnityEngine.InputSystem;

public class PadreMove : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;

    void Update()
    {
        // Get WASD / Arrow Key input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate direction relative to the player's orientation
        Vector3 move = transform.right * x + transform.forward * z;

        // Apply movement
        controller.Move(move * speed * Time.deltaTime);
    }

}
