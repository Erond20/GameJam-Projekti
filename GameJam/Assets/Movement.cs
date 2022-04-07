using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;

    public Transform player;

    public float speed = 12f;

    
    public float walkSpeed;
    public float RunSpeed;
    public float jumpHeight = 2.4f;

    public float gravity = -9.81f;
    private float x;
    private float z;
    public MoveState state;
    public enum MoveState
    {
        walking,
        sprinting,
        air,
    }

    
    Vector3 move = new Vector3();
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        StateHandler();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

         x = Input.GetAxis("Horizontal");
         z = Input.GetAxis("Vertical");

        
        
        move = transform.right * x + transform.forward * z  ;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {

            velocity.y = jumpHeight;

        }
        

        
        
       


    }
    private void StateHandler()
    {
        if (controller.isGrounded && Input.GetKey(KeyCode.LeftShift))
        {
            state = MoveState.sprinting;
            speed = RunSpeed;
            
        }
        else if (controller.isGrounded)
        {
            state = MoveState.walking;
            speed = walkSpeed;

        }
        else
        {
            state = MoveState.air;
        }
    }
}
