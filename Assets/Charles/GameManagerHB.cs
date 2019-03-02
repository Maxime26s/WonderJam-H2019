using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerHB : MonoBehaviour
{
    public GameObject player;
    public GameObject planete;
    public float speed = 5.0f;
    public bool playerCollideRight;
    public bool playerCollideLeft;
    public bool grounded = true;
    public Direction direction;
    public float vi, vf, yi, yf, a, ti, tf;

    // Start is called before the first frame update
    void Start()
    {
        a = -9.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && !playerCollideRight)
        {
            planete.transform.eulerAngles = new Vector3(0, 0, planete.transform.eulerAngles.z - speed);
            direction = Direction.right;
            player.GetComponent<SpriteRenderer>().flipX = true;
        }

        else if (Input.GetKey(KeyCode.D) && !playerCollideLeft)
        {
            planete.transform.eulerAngles = new Vector3(0, 0, planete.transform.eulerAngles.z + speed);
            direction = Direction.left;
            player.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            initFall(4);
        }
        if (!grounded)
        {
            vf = vi + a * (Time.time - ti);
            player.transform.position = new Vector3(0, yi + (vi + vf) * (Time.time - ti) / 2, 0);
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
