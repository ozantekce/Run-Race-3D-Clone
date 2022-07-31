using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Vector3 move;
    public float speed, jumpForce, gravity, verticalVelocity;

    private CharacterController charController;


    void Start()
    {
        charController = GetComponent<CharacterController>();    
    }

    void Update()
    {

        move = Vector3.zero;
        move = transform.forward;

        if (charController.isGrounded)
        {
            //jump
        }

        move.Normalize();
        move *= speed;
        charController.Move(move * Time.deltaTime);

    }


}
