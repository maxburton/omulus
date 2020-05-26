using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    // Movement parameters
    public float baseSpeed = 10f;
    public float maxSpeedFactor = 3f;
    private float maxSpeed;
    private float speed;
    public float speedScalingFactor = 1.3f;
    public float gravity = -9.81f * 3;
    public float jumpHeight = 1f * 3;
    private Vector3 lastMovement;
    private bool sprinting = false;

    private Vector3 velocity;
    private float trailingGravity = -2f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = baseSpeed * maxSpeedFactor;
        speed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Note: We multiply by delta time at final step to avoid repeating it for each movement modification

        // Sprint
        if(Input.GetButtonDown("Sprint")){
            sprinting = true;
        }
        if(Input.GetButtonUp("Sprint")){
            sprinting = false;
        }

        // Scale down to increase speed by scalingFactor every second.
        float localSpeedScalingFactor = 1 + (speedScalingFactor * Time.deltaTime);
        // If sprint key is held down, and player is on the ground and maxSpeed not exceeded
        if(sprinting && isGrounded && speed <= maxSpeed){
            speed = speed * localSpeedScalingFactor;
        }
        // If sprint key is not held down, and player is on the ground and speed is at least base speed
        if(!sprinting && isGrounded && speed >= baseSpeed){
            speed = speed * (2 - localSpeedScalingFactor);
        }

        if(speed < baseSpeed){
            speed = baseSpeed;
        }

        // Left-Right Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z) * speed;

        /*
        if(!isGrounded){
            move = lastMovement;
        }
        */

        controller.Move(move * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0){
            velocity.y = trailingGravity;
        }

        // Jump
        if(Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            lastMovement = move;
        }

        // Gravity (change in velocity = g*t^2)
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
