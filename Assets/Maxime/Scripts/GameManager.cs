using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Animator animator;
    public GameObject player, planete;
    public float speed = 5.0f;
    public bool playerCollideRight, playerCollideLeft;
    public bool grounded=true;
    public Direction direction;
    public float vi, vf, yi, yf, a, ti,tf;
    public bool action;
    public float timerAction;

    // Start is called before the first frame update
    void Start()
    {
        a=-9.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!action)
        {
            if (Input.GetKey(KeyCode.A) && !playerCollideRight)
            {
                planete.transform.eulerAngles = new Vector3(0, 0, planete.transform.eulerAngles.z - speed);
                direction = Direction.right;
                player.GetComponent<SpriteRenderer>().flipX = true;
                animator.SetBool("running", true);
            }

            else if (Input.GetKey(KeyCode.D) && !playerCollideLeft)
            {
                planete.transform.eulerAngles = new Vector3(0, 0, planete.transform.eulerAngles.z + speed);
                direction = Direction.left;
                player.GetComponent<SpriteRenderer>().flipX = false;
                animator.SetBool("running", true);
            }
  
            else
                animator.SetBool("running", false);

            if (Input.GetKey(KeyCode.Space) && grounded)
            {
                initFall(4);
                animator.SetBool("jumping", true);
            }

            if (!grounded)
            {
                vf = vi + a * (Time.time - ti);
                player.transform.position = new Vector3(0, yi + (vi + vf) * (Time.time - ti) / 2, 0);
            }
        }

        if (Input.GetKey(KeyCode.E))
        {
            timerAction = Time.time;
            action = true;
            animator.SetBool("action", true);
        }
        if (action&&Time.time-timerAction>3)
        {
            action = false;
            animator.SetBool("action", false);
        }
    }

    public void initFall(float speed)
    {
        grounded = false;
        vi = speed;
        ti = Time.time;
        yi = player.transform.position.y;
    }

    public enum Direction
    {
        right,
        left
    }
}
