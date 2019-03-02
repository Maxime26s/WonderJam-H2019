using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Animator animator;
    public GameObject player, planete, right, left;
    public float speed = 5.0f;
    public bool playerCollideRight, playerCollideLeft;
    public bool grounded = true;
    public Direction direction;
    public float vi, vf, yi, yf, a, ti, tf;
    public bool action;
    public float timerAction;
    public GameObject[] inventory = new GameObject[5];
    public GameObject interactable;

    // Start is called before the first frame update
    void Start()
    {
        a = -9.8f;
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
                for (int i = 0; i < inventory.Length; i++)
                    if (inventory[i] != null)
                        inventory[i].transform.position = Vector3.MoveTowards(inventory[0].transform.position, right.transform.position, 0.0225f);

            }

            else if (Input.GetKey(KeyCode.D) && !playerCollideLeft)
            {
                planete.transform.eulerAngles = new Vector3(0, 0, planete.transform.eulerAngles.z + speed);
                direction = Direction.left;
                player.GetComponent<SpriteRenderer>().flipX = false;
                animator.SetBool("running", true);
                for (int i = 0; i < inventory.Length; i++)
                    if (inventory[i] != null)
                        inventory[i].transform.position = Vector3.MoveTowards(inventory[0].transform.position, left.transform.position, 0.0225f);
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
            if (Input.GetKey(KeyCode.E) && grounded)
            {
                timerAction = Time.time;
                action = true;
                animator.SetBool("action", true);
                if (interactable != false)
                    switch (interactable.tag)
                    {
                        case "air":
                            use(Type.air);
                            interactable.GetComponent<Items>().used = true;
                            interactable.GetComponent<Items>().time = Time.time;
                            interactable.GetComponent<CircleCollider2D>().enabled = false;
                            break;
                    }
            }
        }

        if (action && Time.time - timerAction > 3)
        {
            action = false;
            animator.SetBool("action", false);
            grounded = true;
        }
        for (int i = 0; i < inventory.Length; i++)
            if (inventory[i] != null)
            {
                if (i == 0)
                    inventory[0].transform.position = Vector3.MoveTowards(inventory[0].transform.position, player.transform.position, 0.02f);
                else
                    inventory[i].transform.position = Vector3.MoveTowards(inventory[i].transform.position, inventory[i - 1].transform.position, 0.02f - 0.0025f * i);
            }
    }

    public void initFall(float speed)
    {
        grounded = false;
        vi = speed;
        ti = Time.time;
        yi = player.transform.position.y;
    }

    public void use(Type type)
    {
        switch (type)
        {
            case Type.air:
                break;
            case Type.talk:
                break;
        }

    }

    public enum Direction
    {
        right,
        left
    }

    public enum Type
    {
        air,
        talk,
    }
}
