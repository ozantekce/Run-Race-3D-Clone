using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Vector3 move;
    public float speed, jumpForce, gravity, verticalVelocity;

    private bool wallSlide;

    private bool doubleJump;
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
            wallSlide = false;
            verticalVelocity = 0;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                verticalVelocity = jumpForce;
                doubleJump = true;
            }
        }
        else if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && doubleJump)
        {
            verticalVelocity += jumpForce * 0.5f;
            doubleJump = false;
        }

        if (!wallSlide)
        {
            gravity = 30;
            verticalVelocity -= gravity * Time.deltaTime;
        }
        else
        {
            gravity = 15;
            verticalVelocity -= gravity * Time.deltaTime;
        }


        move.Normalize();
        move *= speed;
        move.y = verticalVelocity;
        charController.Move(move * Time.deltaTime);
        print(wallSlide);

    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (!charController.isGrounded)
        {
            if(hit.collider.tag == "Wall")
            {
                if(verticalVelocity < -0.6f)
                    wallSlide = true;
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    verticalVelocity = jumpForce;

                    doubleJump = false;

                    transform.eulerAngles = new Vector3(
                        transform.eulerAngles.x
                        , transform.eulerAngles.y + 180
                        , transform.eulerAngles.z);

                    wallSlide = false;
                }
            }

        }

    }


}
