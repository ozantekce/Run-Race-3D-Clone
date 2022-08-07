using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private Vector3 move;
    public float speed, jumpForce, gravity, verticalVelocity;

    private bool wallSlide, turn , jump, superJump;

    private bool doubleJump;
    private CharacterController charController;

    private Animator animator;

    void Awake()
    {
        charController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

    }


    void Update()
    {

        if (GameManager.instance.finish)
        {
            move = Vector3.zero;

            if (!charController.isGrounded)
            {
                verticalVelocity -= gravity * Time.deltaTime;
            }
            else
            {
                verticalVelocity = 0;
            }

            move.y = verticalVelocity;
            charController.Move(new Vector3(0, move.y * Time.deltaTime, 0));
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Dance"))
            {
                animator.SetTrigger("Dance");
                transform.eulerAngles = Vector3.up * 180;
            }

            return;
        }

        if (!GameManager.instance.start)
            return;

        move = Vector3.zero;
        move = transform.forward;


        if (charController.isGrounded)
        {
            jump = true;
            verticalVelocity = 0;
            RayCasting();




        }

        if (superJump)
        {
            superJump = false;
            verticalVelocity = jumpForce * 1.75f;
            animator.SetTrigger("Jump");
        }


        if (!wallSlide)
        {
            gravity = 30;
            verticalVelocity -=gravity*Time.deltaTime;
        }
        else
        {
            gravity = 15;
            verticalVelocity -= gravity * Time.deltaTime;
        }

        animator.SetBool("Grounded", charController.isGrounded);
        animator.SetBool("WallSlide", wallSlide);

        move.Normalize();
        move *= speed;
        move.y = verticalVelocity;
        charController.Move(move * Time.deltaTime);

    }



    private void RayCasting()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position ,transform.forward, out hit,2f))
        {

            Debug.DrawLine(transform.position , hit.point,Color.red);
            if (hit.collider.CompareTag("Wall"))
            {
                verticalVelocity = jumpForce;
                animator.SetTrigger("Jump");
                
            }
        }


    }


    private IEnumerator LateJump(float time)
    {
        jump = false;
        wallSlide = true;
        yield return new WaitForSeconds(time);

        if (!charController.isGrounded)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x
                , transform.eulerAngles.y + 180
                , transform.eulerAngles.z);
            verticalVelocity = jumpForce;
            animator.SetTrigger("Jump");

        }
        jump = true;
        wallSlide = false;
    }



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Wall"))
        {

            if(jump)
                StartCoroutine(LateJump(Random.Range(0.2f,0.5f)));
            if(verticalVelocity<0)
                wallSlide = true;
        }

        if(hit.collider.tag == "Slide" && charController.isGrounded)
        {

        }
        else if(hit.collider.tag == "Slide") {
            wallSlide = true;
        }

        if (hit.collider.CompareTag("Trampoline") && charController.isGrounded)
        {
            superJump = true;
        }
    }



}
