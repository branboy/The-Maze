using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
Vector3 velocity;
public CharacterController controller;

public float speed = 20f;
public float gravity = -10f;
public float jumpHeight = 5f;
public Transform ground;
public float groundDistance = 0.4f;
public LayerMask Land;
bool ifGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        ifGrounded = Physics.CheckSphere(ground.position, groundDistance, Land);
        if(ifGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }
        
        float x =  Input.GetAxis("Horizontal");

        float y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * y;

        controller.Move(move * speed * Time.deltaTime);

        if(ifGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
