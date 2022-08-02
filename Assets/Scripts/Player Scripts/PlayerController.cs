using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Vector3 move;
    public float speed, jumpForce, gravity, verticalVelocity;

    private bool wallSlide, turn;

    private bool doubleJump;
    private CharacterController charController;

    private Animator animator;


    void Start()
    {
        charController = GetComponent<CharacterController>();    
        animator = GetComponentInChildren<Animator>();
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
                Jump(jumpForce);
            }
            if (turn)
            {
                turn = false;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x
                    ,transform.eulerAngles.y+180
                    ,transform.eulerAngles.z
                    );
            }

        }
        else if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && doubleJump)
        {
            Debug.Log("hi");
            Jump(jumpForce*0.75f);
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

        animator.SetBool("WallSlide",wallSlide);
        animator.SetBool("Grounded",charController.isGrounded);


        move.Normalize();
        move *= speed;
        move.y = verticalVelocity;
        charController.Move(move * Time.deltaTime);
        //print(wallSlide);

    }

    private void Jump(float force)
    {

        animator.SetTrigger("Jump");
        verticalVelocity = force;
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
                    Jump(jumpForce);

                    doubleJump = false;

                    transform.eulerAngles = new Vector3(
                        transform.eulerAngles.x
                        , transform.eulerAngles.y + 180
                        , transform.eulerAngles.z);

                    wallSlide = false;
                }
            }

        }
        else
        {
            if(transform.forward != hit.collider.transform.up && hit.collider.tag == "Ground" && !turn)
            {
                turn = true;
            }

        }

    }


}
