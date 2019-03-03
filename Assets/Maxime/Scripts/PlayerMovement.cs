using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    float horizontalMove = 0f;
    public float runSpeed = 40f;
    bool jump = false;
    public bool action = false;

    // Update is called once per frame
    void Update()
    {
        if (!action)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            if (Mathf.Abs(horizontalMove) > 0.1)
                animator.SetBool("running", true);
            else
                animator.SetBool("running", false);
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("jumping", true);
            }
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove*Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public void OnLanding()
    {
        animator.SetBool("jumping", false);
    }
}
